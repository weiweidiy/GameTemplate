using Cysharp.Threading.Tasks;
using JFramework;
using JFramework.Unity;
using System;
using System.Threading.Tasks;
using UnityEngine;


namespace Game
{
    public class MessageHandler : BaseNetworkMessageHandler
    {
        public override async void Handle(IJNetMessage message)
        {
            switch(message.TypeId)
            {
                case (int)ProtocolType.BuildingUpdateNtf:
                    {
                        var ntf = message as BuildingUpdateNtf;
                        Debug.Log($"HpPoolUpdateNtf: {ntf.BuildingDTO.BusinessId}");
                    }
                    break;
                case (int)ProtocolType.StartFightNtf:
                    {
                        //var ntf = message as StartFightNtf;
                        //var port = ntf.Port;
                        //Debug.Log($"Received StartFightNtf, connecting to fight server at port {port}...");
                        //var model = Facade.GetModelManager().GetModel<PlayerModel>();
                        //GlobalBoard.Ip = "127.0.0.1";
                        //GlobalBoard.Port = port;
                        //GlobalBoard.PlayerUid = model.GetAll()[0].Uid;
                        //await UniTask.Delay(3000); // 模拟连接服务器的延迟
                        //Facade.GetSceneStateMachine().SwitchToState(DemoSceneType.RoomScene.ToString(), Facade.GetGameContext());
                    }
                    break;
                default:
                    throw new Exception($"Unknown message type: {message.TypeId}");
            }
        }
    }
}

