//using Cysharp.Threading.Tasks;
//using Game.Main;
//using JFramework;
//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;

//namespace Game
//{
//    public class SignalRForWX : IJSocket
//    {
//        public bool IsOpen => signalRBridge != null && signalRBridge.GetState() == ConnectionState.Connected;

//        public event Action<IJSocket> onOpen;
//        public event Action<IJSocket, SocketStatusCodes, string> onClosed;
//        public event Action<IJSocket, string> onError;
//        public event Action<IJSocket, byte[]> onBinary;

//        private SignalRBridge signalRBridge;
//        private string url;
//        private string token;
//        private bool isInited;

//        public SignalRForWX()
//        {
//            signalRBridge = SignalRBridge.Instance;
//        }

//        public object Clone()
//        {
//            var clone = new SignalRForWX();
//            //clone.Init(url, token);
//            return clone;
//        }

//        public void Init(string url, string token = null)
//        {
//            this.url = url;
//            this.token = token;
//            isInited = false;

//            // 先解绑，防止重复注册
//            signalRBridge.onConnected.RemoveListener(OnConnected);
//            signalRBridge.onDisconnected.RemoveListener(OnDisconnected);
//            signalRBridge.onBinaryMessageReceived.RemoveListener(OnBinaryReceived);

//            signalRBridge.onConnected.AddListener(OnConnected);
//            signalRBridge.onDisconnected.AddListener(OnDisconnected);
//            signalRBridge.onBinaryMessageReceived.AddListener(OnBinaryReceived);

//            isInited = true;
//        }

//        public void Open()
//        {
//            if (!isInited)
//                throw new InvalidOperationException("Please call Init() before Open().");

//            string connectUrl = url;
//            if (!string.IsNullOrEmpty(token))
//            {
//                // 假设 token 通过 url 传递
//                connectUrl += (url.Contains("?") ? "&" : "?") + "token=" + token;
//            }
//            //signalRBridge.LoadJS();
//            //await UniTask.Delay(500); // 等待 JS 加载完成
//            signalRBridge.Initialize(connectUrl);
//        }

//        public void Close()
//        {
//            signalRBridge.Stop();
//        }

//        public Task<byte[]> Send(byte[] data)
//        {
//            if (!IsOpen)
//            {
//                onError?.Invoke(this, "Connection is not open.");
//                return null;
//            }
//            // 这里假设服务端有 "ReceiveBinary" 方法
//            signalRBridge.InvokeBinary("SendBinary", data);
//        }




//        private void OnConnected(string connectionId)
//        {
//            onOpen?.Invoke(this);
//        }

//        private void OnDisconnected(string reason)
//        {
//            onClosed?.Invoke(this, SocketStatusCodes.NormalClosure, reason);
//        }

//        private void OnBinaryReceived(byte[] data)
//        {
//            UnityEngine.Debug.Log($"unity: Received binary data: {data.Length} bytes");
//            onBinary?.Invoke(this, data);
//        }
//    }
//}

using Game.Main;
using JFramework;
using System;
using System.Threading.Tasks;

namespace Game
{
    public class SignalRForWX : JBaseSocket
    {
        private SignalRBridge signalRBridge;

        public override bool IsOpen { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override void Close()
        {
            throw new System.NotImplementedException();
        }

        public override void Init(string url, string token = null)
        {
            throw new System.NotImplementedException();
        }

        public override void Open()
        {
            throw new System.NotImplementedException();
        }

        public override Task<TResponse> RPC<TResponse>(string method, object param = null, TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }

        public override Task RPCVoid(string method, object param = null, TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }

        public override Task<byte[]> Send(byte[] data)
        {
            throw new System.NotImplementedException();
        }

        public override Task<string> Send(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
