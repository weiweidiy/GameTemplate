using JFramework;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityWebSocket;

namespace Game
{
    public class UnityWebSocketImp : JBaseSocket
    {
        private WebSocket webSocket;
        private bool isOpen = false;
        private string url;

        public override bool IsOpen
        {
            get => webSocket != null && webSocket.ReadyState == WebSocketState.Open;
            set => isOpen = value;
        }

        public override void Init(string url, string token = null)
        {
            this.url = url;
            webSocket = new WebSocket(url);

            // 注册事件
            webSocket.OnOpen += (sender, e) =>
            {
                isOpen = true;
                OnOpen(this);
            };
            webSocket.OnClose += (sender, e) =>
            {
                isOpen = false;
                // 这里假设 reason 可通过 e.Reason 获取，若没有可用 string.Empty
                string reason = e is CloseEventArgs closeArgs ? closeArgs.Reason : string.Empty;
                OnClosed(this, SocketStatusCodes.NormalClosure, reason);
            };
            webSocket.OnError += (sender, e) =>
            {
                isOpen = false;
                // 这里假设错误信息可通过 e.Message 获取，若没有可用 e.ToString()
                string msg = e is ErrorEventArgs errorArgs ? errorArgs.Message : e.ToString();
                OnError(this, msg);
            };
            webSocket.OnMessage += (sender, e) =>
            {
                // 这里假设 e 是 MessageEventArgs，包含 Data (byte[]) 和 Text (string)
                if (e is MessageEventArgs msgArgs)
                {
                    if (msgArgs.Data != null)
                    {
                        OnBinary(this, msgArgs.RawData);
                    }
                    else if (!string.IsNullOrEmpty(msgArgs.Data))
                    {
                        OnMessage(this, msgArgs.Data);
                    }
                }
            };

        }



        public override void Open()
        {
            if (webSocket == null)
            {
                OnError(this, "WebSocket not initialized.");
                return;
            }
            webSocket.ConnectAsync();
        }

        public override void Close()
        {
            if (webSocket == null) return;
            webSocket.CloseAsync();
        }

        public override Task<byte[]> Send(byte[] data)
        {
            if (!IsOpen)
            {
                OnError(this, "Connection is not open.");
                return Task.FromResult<byte[]>(null);
            }
            try
            {
                webSocket.SendAsync(data);
            }
            catch (Exception ex)
            {
                OnError(this, ex.Message);
                return Task.FromResult<byte[]>(null);
            }

            return Task.FromResult<byte[]>(null);
        }

        public override Task<string> Send(string message)
        {

            if (!IsOpen)
            {
                OnError(this, "Connection is not open.");
                return Task.FromResult<string>(null);
            }
            try
            {
                webSocket.SendAsync(message);
            }
            catch (Exception ex)
            {
                OnError(this, ex.Message);
                return Task.FromResult<string>(null);
            }

            return Task.FromResult<string>(null);
        }

        public override Task RPCVoid(string method, object param = null, TimeSpan? timeout = null)
        {
            throw new NotImplementedException("RPCVoid is not implemented. Please implement it according to your server protocol.");
            //// 简单实现：将method和param序列化为json发送，实际需结合服务端协议
            //var tcs = new TaskCompletionSource<bool>();
            //if (!IsOpen)
            //{
            //    OnError(this, "Connection is not open.");
            //    tcs.SetResult(false);
            //    return tcs.Task;
            //}
            //try
            //{
            //    var rpcObj = new { method, param };
            //    var json = System.Text.Json.JsonSerializer.Serialize(rpcObj);
            //    webSocket.SendAsync(json);
            //    tcs.SetResult(true);
            //}
            //catch (Exception ex)
            //{
            //    OnError(this, ex.Message);
            //    tcs.SetResult(false);
            //}
            //return tcs.Task;
        }

        public override Task<TResponse> RPC<TResponse>(string method, object param = null, TimeSpan? timeout = null)
        {
            throw new NotImplementedException("RPCVoid is not implemented. Please implement it according to your server protocol.");

            //// 简单实现：将method和param序列化为json发送，实际需结合服务端协议和响应回调
            //var tcs = new TaskCompletionSource<TResponse>();
            //if (!IsOpen)
            //{
            //    OnError(this, "Connection is not open.");
            //    tcs.SetResult(default);
            //    return tcs.Task;
            //}
            //try
            //{
            //    var rpcId = Guid.NewGuid().ToString();
            //    var rpcObj = new { id = rpcId, method, param };
            //    var json = System.Text.Json.JsonSerializer.Serialize(rpcObj);
            //    webSocket.SendAsync(json);

            //    // 这里需要你实现响应回调与rpcId的对应关系
            //    // 示例仅返回default
            //    tcs.SetResult(default);
            //}
            //catch (Exception ex)
            //{
            //    OnError(this, ex.Message);
            //    tcs.SetResult(default);
            //}
            //return tcs.Task;
        }
    }
}