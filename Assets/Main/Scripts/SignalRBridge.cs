using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public sealed class SignalRBridge
{
    // 静态事件
    public static event Action<string, string> OnMessage;
    public static event Action<string> OnConnected;
    public static event Action<string> OnDisconnected;
    // 可以添加相应的事件供外部订阅
    public static event Action<byte[]> OnBinaryMessage;


    // 实例事件（方便 Inspector 绑定，如果需要的话）
    public UnityEvent<string, string> onMessageReceived = new UnityEvent<string, string>();
    public UnityEvent<string> onConnected = new UnityEvent<string>();
    public UnityEvent<string> onDisconnected = new UnityEvent<string>();
    public UnityEvent<byte[]> onBinaryMessageReceived = new UnityEvent<byte[]>();

    private static SignalRBridge _instance;
    private bool _isInitialized;
    private string _currentConnectionId = "";

    public static SignalRBridge Instance => _instance ??= new SignalRBridge();

    public bool IsInitialized => _isInitialized;
    public string ConnectionId => _currentConnectionId;

    private SignalRBridge() { }

#if UNITY_WEBGL && !UNITY_EDITOR

    [DllImport("__Internal")]
    private static extern void SignalR_Init(string url, 
        Action<string, string> onMessageCallback, 
        Action<string> onConnectedCallback, 
        Action<string> onDisconnectedCallback,
        Action<IntPtr, int> onBinaryMessageReceived);

    [DllImport("__Internal")]
    private static extern void SignalR_Invoke(string methodName, string argsJson);

    [DllImport("__Internal")]
    private static extern void SignalR_Stop();

    [DllImport("__Internal")]
    private static extern int SignalR_GetState();

    // 新增：二进制发送
    [DllImport("__Internal")]
    private static extern void SignalR_InvokeBinary(string methodName, byte[] data, int dataLen);

    [DllImport("__Internal")]
    private static extern void SignalR_Free(IntPtr ptr);

#else
    // 编辑器模拟
    private static void SignalR_Init(string url, Action<string, string> onMsg, Action<string> onCon, Action<string> onDisc)
    {
        Debug.Log($"[SignalR Mock] Init: {url}");
        onCon?.Invoke("mock-id");
    }
    private static void SignalR_Invoke(string methodName, string argsJson) => Debug.Log($"[Mock] Invoke: {methodName}, {argsJson}");
    private static void SignalR_Stop() => Debug.Log("[Mock] Stop");
    private static int SignalR_GetState() => 2;

    // 编辑器模拟二进制发送
    private static void SignalR_InvokeBinary(string methodName, byte[] data, int dataLen)
        => Debug.Log($"[Mock] InvokeBinary: {methodName}, length={dataLen}");

    private static void SignalR_Free(IntPtr ptr)
    {
        // 编辑器模拟：无需真正释放，或使用 Marshal.FreeHGlobal 模拟
        // 因为编辑器下 ptr 通常是模拟的，不一定是真实非托管内存
        Debug.Log("[SignalR Mock] SignalR_Free called (simulated)");
    }

#endif

    [AOT.MonoPInvokeCallback(typeof(Action<string, string>))]
    public static void HandleMessageReceived(string methodName, string argsJson)
    {
        Debug.Log($"[SignalR] Message: {methodName} -> {argsJson}");
        OnMessage?.Invoke(methodName, argsJson);
        var instance = Instance;
        if (instance?.onMessageReceived != null)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                instance.onMessageReceived.Invoke(methodName, argsJson);
            });
        }
    }

    [AOT.MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
    public static void HandleBinaryMessage(IntPtr ptr, int length)
    {
        if (length <= 0 || ptr == IntPtr.Zero)
        {
            Debug.LogWarning("[SignalR] Received empty binary data");
            return;
        }

        // 将非托管内存中的数据复制到托管 byte 数组
        byte[] data = new byte[length];
        Marshal.Copy(ptr, data, 0, length);

#if UNITY_WEBGL && !UNITY_EDITOR
     // 在 WebGL 构建时，调用 JS 提供的 free 函数
        SignalR_Free(ptr);
#else
        // 编辑器模式下，无操作或仅输出日志
        Debug.Log($"[SignalR Mock] Received binary data of length {length}, memory free skipped.");
#endif

        // 现在可以在主线程中处理 data
        // 注意：此回调可能在非主线程，需要调度到主线程执行后续操作
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            // 在这里处理二进制数据，比如触发事件
            OnBinaryMessage?.Invoke(data);
            var instance = Instance;
            instance?.onBinaryMessageReceived?.Invoke(data);
        });
    }

    [AOT.MonoPInvokeCallback(typeof(Action<string>))]
    public static void HandleConnected(string connectionId)
    {
        Debug.Log($"[SignalR] Connected: {connectionId}");
        var instance = Instance;
        instance._currentConnectionId = connectionId;
        instance._isInitialized = true;
        OnConnected?.Invoke(connectionId);
        if (instance?.onConnected != null)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                instance.onConnected.Invoke(connectionId);
            });
        }
    }

    [AOT.MonoPInvokeCallback(typeof(Action<string>))]
    public static void HandleDisconnected(string reason)
    {
        Debug.Log($"[SignalR] Disconnected: {reason}");
        var instance = Instance;
        instance._isInitialized = false;
        OnDisconnected?.Invoke(reason);
        if (instance?.onDisconnected != null)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                instance.onDisconnected.Invoke(reason);
            });
        }
    }


    public void Initialize(string url)
    {
        if (_isInitialized)
        {
            Debug.LogWarning("[SignalR] Already initialized");
            return;
        }
#if UNITY_WEBGL && !UNITY_EDITOR
        SignalR_Init(url, HandleMessageReceived, HandleConnected, HandleDisconnected,HandleBinaryMessage);
#else
        HandleConnected("editor-mock-id");
#endif
    }

    public void Invoke(string methodName, object args)
    {
        if (!_isInitialized)
        {
            Debug.LogError("[SignalR] Not initialized");
            return;
        }
        string argsJson = args != null ? JsonUtility.ToJson(args) : "{}";
#if UNITY_WEBGL && !UNITY_EDITOR
        SignalR_Invoke(methodName, argsJson);
#else
        Debug.Log($"[Mock] Invoke: {methodName}, {argsJson}");
#endif
    }


    // 新增公共方法：发送二进制数据
    /// <summary>
    /// 以二进制形式调用服务端方法
    /// </summary>
    /// <param name="methodName">服务端方法名</param>
    /// <param name="data">要发送的字节数组</param>
    public void InvokeBinary(string methodName, byte[] data)
    {
        if (!_isInitialized)
        {
            Debug.LogError("[SignalR] Not initialized");
            return;
        }
        if (data == null)
        {
            Debug.LogWarning("[SignalR] InvokeBinary: data is null, sending empty array");
            data = new byte[0];
        }
#if UNITY_WEBGL && !UNITY_EDITOR
        SignalR_InvokeBinary(methodName, data, data.Length);
#else
        Debug.Log($"[Mock] InvokeBinary: {methodName}, length={data.Length}");
#endif
    }


    public void Stop()
    {
        if (!_isInitialized) return;
#if UNITY_WEBGL && !UNITY_EDITOR
        SignalR_Stop();
#endif
        _isInitialized = false;
        _currentConnectionId = "";
    }

    public ConnectionState GetState()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        int state = SignalR_GetState();
        return (ConnectionState)state;
#else
        return ConnectionState.Connected;
#endif
    }
}

public enum ConnectionState
{
    Disconnected = 0,
    Connecting = 1,
    Connected = 2
}