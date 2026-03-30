using JFramework.Unity;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class MatchFightController : Controller
    {
        public override async Task Do(GameContext context, params object[] parameters)
        {


            //var ntf = await context.Facade.GetNetworkManager().SendMessage<StartFightNtf>(new StartFightNtf()
            //{
            //    Uid = "test",
            //    TypeId = (int)ProtocolType.StartFightNtf
            //});


            //await context.Facade.GetNetworkManager().RPCVoid("Ping");
            var res = await context.Facade.GetNetworkManager().RPC<BuildingDTO>("TestRPC", new BuildingDTO() { });

            Debug.Log($"res ok building level:" + res.Level);

            //Debug.Log($"匹配成功 IP： 端口：{ntf.Port}");

            //var httpRequest = context.Facade.GetHttpRequest();
            //ResMatch matchTask = null;
            //var url = parameters[0] as string;
            //var req = parameters[1] as ReqMatch;
            //try
            //{
            //    matchTask = await httpRequest.HttpRequestAsync<ReqMatch, ResMatch>(url, req);
            //}
            //catch (Exception e)
            //{
            //    Debug.LogError($"匹配失败，错误信息：{e.Message}");
            //    throw;
            //}
            //Debug.Log($"匹配成功 IP：{matchTask.Ip} 端口：{matchTask.Port}");
        }

    }
}