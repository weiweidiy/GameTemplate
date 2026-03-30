using JFramework;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using UnityEngine;
using JFramework.Unity;

namespace Game
{
    public class SignalRSocket : JBaseSocket
    {
        private HubConnection connection;
        private bool isOpen = false;
        private string url;

        public override bool IsOpen { get => isOpen && connection != null && connection.State == HubConnectionState.Connected; set => isOpen = value; }

        public override void Init(string url, string token)
        {
            this.url = url;

            connection = new HubConnectionBuilder()
                            .WithUrl(url, options =>
                            {
                                options.AccessTokenProvider = () => Task.FromResult(token);
                            })
                            .WithAutomaticReconnect()
                            //.AddMessagePackProtocol()
                            .Build();

            // 假设服务器端有 "ReceiveBinary" 方法用于接收二进制数据
            connection.On<byte[]>("ReceiveBinary", (data) =>
            {
                Cysharp.Threading.Tasks.UniTask.Post(() =>
                {
                    OnBinary(this, data);
                });

                //onBinary?.Invoke(this, data);
            });

            connection.Closed += async (error) =>
            {
                isOpen = false;
                OnClosed(this, SocketStatusCodes.NormalClosure, error?.Message ?? "Closed");
                await Task.CompletedTask;
            };

            connection.Reconnected += async (connectionId) =>
            {
                isOpen = true;
                OnOpen(this);
                await Task.CompletedTask;
            };

            connection.Reconnecting += async (error) =>
            {
                isOpen = false;
                OnError(this, error?.Message ?? "Reconnecting");
                await Task.CompletedTask;
            };
        }

        public override async void Open()
        {
            if (connection == null)
            {
                OnError(this, "Connection not initialized.");
                return;
            }
            try
            {
                await connection.StartAsync();
                isOpen = true;
                OnOpen(this);
            }
            catch (Exception ex)
            {
                isOpen = false;
                OnError(this, ex.Message);
            }
        }

        public override async void Close()
        {
            if (connection == null) return;
            try
            {
                await connection.StopAsync();
                isOpen = false;
                OnClosed(this, SocketStatusCodes.NormalClosure, "Closed by client");
            }
            catch (Exception ex)
            {
                OnError(this, ex.Message);
            }
        }

        public override async Task<byte[]> Send(byte[] data)
        {
            if (!IsOpen)
            {
                OnError(this, "Connection is not open.");
                return default;
            }
            try
            {
                // 需要服务器端实现 SendBinaryWithResult
                return await connection.InvokeAsync<byte[]>("SendBinary", data);
            }
            catch (Exception ex)
            {
                OnError(this, ex.Message);
                return default;
            }
        }


        public override async Task<string> Send(string message)
        {
            if (!IsOpen)
            {
                OnError(this, "Connection is not open.");
                return default;
            }
            try
            {
                // 需要服务器端实现 SendBinaryWithResult
                return await connection.InvokeAsync<string>("SendMessage", message);
            }
            catch (Exception ex)
            {
                OnError(this, ex.Message);
                return default;
            }
        }

        public override async Task RPCVoid(string method, object param = null, TimeSpan? timeout = null)
        {
            if (!IsOpen)
            {
                OnError(this, "Connection is not open.");
                return;
            }
            try
            {
                if (param == null)
                {
                    await connection.InvokeAsync(method);
                }
                else
                {
                    await connection.InvokeAsync(method, param);
                }
            }
            catch (Exception ex)
            {
                OnError(this, ex.Message);
            }
        }

        public override async Task<TResponse> RPC<TResponse>(string method, object param = null, TimeSpan? timeout = null)
        {
            if (!IsOpen)
            {
                OnError(this, "Connection is not open.");
                return default;
            }
            try
            {
                if (param == null)
                {
                    return await connection.InvokeAsync<TResponse>(method);
                }
                else
                {
                    return await connection.InvokeAsync<TResponse>(method, param);
                }
            }
            catch (Exception ex)
            {
                OnError(this, ex.Message);
                return default;
            }
        }

    }
}