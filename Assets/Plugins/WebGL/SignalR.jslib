// Assets/Plugins/WebGL/SignalR.jslib

mergeInto(LibraryManager.library, {
    SignalR_Free: function (ptr) {
        Module._free(ptr);
    },

    SignalR_Init: function (url, onMessageCallback, onConnectedCallback, onDisconnectedCallback,onBinaryMessageCallback) {
        console.dir("js: SignalR init");

        console.dir("R:" + signalR)
        console.dir("protocols:" + signalR.protocols)
        
        var urlStr = UTF8ToString(url);
        var onMessage = onMessageCallback;
        var onConnected = onConnectedCallback;
        var onDisconnected = onDisconnectedCallback;
        var onBinaryMessage = onBinaryMessageCallback

        // 创建连接（假设 signalR 已全局可用）
        var connection = new signalR.HubConnectionBuilder()
            .withUrl(urlStr)
            // .withAutomaticReconnect()  // lyuan-signalR 可能不支持，注释掉，手动重连
            .configureLogging(signalR.LogLevel.Information)
            .withHubProtocol(new signalR.protocols.msgpack.MessagePackHubProtocol())
            .build();

        // 注册消息接收器（根据服务端实际方法名调整）
        // 假设服务端调用 Clients.All.SendAsync("ReceiveMessage", user, message)
        //connection.on("ReceiveMessage", function (user, message) {
            //if (onMessage) {
                // 将 user 和 message 打包成一个 JSON 字符串传给 C#
               // var argsStr = JSON.stringify({ user: user, message: message });
                //dynCall_vii(onMessage, "ReceiveMessage",argsStr);

                //window.Module.mono_bind_static_method(
                    //"SignalRBridge.HandleMessageReceived",
                    //"ReceiveMessage",  // 方法名
                    //argsStr
                //);
            //}
        //});

        // 可选：接收二进制消息（如果服务端发送二进制）
        connection.on("ReceiveBinary", function (data) {
            console.log("data constructor:", data.constructor.name);
            console.log("Is ArrayBuffer?", data instanceof ArrayBuffer);
            console.log("Is TypedArray?", ArrayBuffer.isView(data));

            // 兼容 ArrayBuffer 和 TypedArray
            if (data && (data instanceof ArrayBuffer || ArrayBuffer.isView(data))) {
                var bytes = new Uint8Array(data);
                var ptr = Module._malloc(bytes.length);
                HEAPU8.set(bytes, ptr);
                dynCall_vii(onBinaryMessage, ptr, bytes.length);
                // 注意：C# 中收到后需调用 SignalR_Free(ptr) 释放
                // 注意：C# 中收到后需调用 SignalR_Free(ptr) 释放
            }
        });

        // 连接成功回调
        connection.start()
            .then(function () {
                console.log("SignalR connected");
                dynCall_vi(onConnected, connection.connectionId);
            })
            .catch(function (err) {
                console.error("SignalR connection failed: " + err);
                if (onDisconnected) {
                    
                    dynCall_vi(onDisconnected, err.toString());

                    //window.Module.mono_bind_static_method(
                        //"SignalRBridge.HandleDisconnected",
                        //err.toString()
                    //);
                }
                // 可选：手动重连逻辑（若需要，可在此处实现）
            });

        // 保存连接供后续调用
        window.signalRConnection = connection;

        // 断开连接处理
        connection.onclose(function (error) {
            console.log("SignalR closed: " + error);
            if (onDisconnected) {

                dynCall_vi(onDisconnected, error ? error.toString() : "normal close");

                //window.Module.mono_bind_static_method(
                    //"SignalRBridge.HandleDisconnected",
                    //error ? error.toString() : "normal close"
                //);
            }
            // 若需自动重连，可在此处调用 connection.start()，但注意避免死循环
        });
    },

    SignalR_Invoke: function (methodName, argsJson) {
        var methodNameStr = UTF8ToString(methodName);
        var argsJsonStr = UTF8ToString(argsJson);
        if (!window.signalRConnection) {
            console.error("SignalR not initialized");
            return;
        }
        var args;
        try {
            args = JSON.parse(argsJsonStr);
        } catch (e) {
            args = {};
        }
        window.signalRConnection.invoke(methodNameStr, args)
            .catch(function (err) {
                console.error("Invoke failed: " + err);
            });
    },

    SignalR_InvokeBinary: function (methodName, dataPtr, dataLen) {
        var methodNameStr = UTF8ToString(methodName);
        if (!window.signalRConnection) {
            console.error("SignalR not initialized");
            return;
        }
        var byteArray = new Uint8Array(HEAPU8.buffer, dataPtr, dataLen);
        window.signalRConnection.invoke(methodNameStr, byteArray.buffer)
            .catch(function (err) {
                console.error("Binary invoke failed: " + err);
            });
    },

    SignalR_Stop: function () {
        if (window.signalRConnection) {
            window.signalRConnection.stop();
            window.signalRConnection = null;
        }
    },

    SignalR_GetState: function () {
        if (!window.signalRConnection) return 0;
        var state = window.signalRConnection.state;
        switch (state) {
            case "Connected": return 2;
            case "Connecting": return 1;
            default: return 0;
        }
    }
});