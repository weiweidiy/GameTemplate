using Cysharp.Threading.Tasks;
using Game.MyModule;
using JFramework;
using JFramework.Unity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class LoginController : Controller
    {
        public override async Task Do(GameContext context, params object[] parameters)
        {
            var httpRequest = context.Services.Resolve<IHttpRequest>();
            var network = context.Services.Resolve<IJNetwork>();
            var url = parameters[0] as string;
            var req = parameters[1] as AccountDTO;

            var urlEnter = parameters[2] as string;
            var reqEnter = parameters[3] as ReqEnterGame;

            var socketUrl = parameters[4] as string;

            var netholdingView = context.Services.Resolve<IViewRegistry>().GetViewForScene<SceneLoginState, UIPanelNetworkHoldingView>();
            var tipsView = context.Services.Resolve<IViewRegistry>().GetViewForScene<SceneLoginState, UIPanelWarningMessageView>();

            AccountDTO accountDto;

            try
            {
                accountDto = await httpRequest.HttpRequestAsync<AccountDTO, AccountDTO>(url, req, null, netholdingView);
                Debug.Log($"登录成功，Token={accountDto.Token}");
                SetToken(httpRequest, accountDto.Token);
            }
            catch(Exception e)
            {
                netholdingView.Close();
                tipsView.Open(new UIPanelWarningMessageViewData() { prefabName = nameof(UIPanelWarningMessage), message = $"登录失败，错误信息：{e.Message}" });
                throw;
            }
   

            try
            {
                //var netholdingView = sceneContext.Facade.GetViewControllerContainer().GetViewController(nameof(UIPanelNetworkHoldingView)) as UIPanelNetworkHoldingView;
                var enterGame = await httpRequest.HttpRequestAsync<ReqEnterGame, ResEnterGame>(urlEnter, reqEnter, null, netholdingView);
                Debug.Log($"进入游戏成功，SocketUrl={enterGame.PlayerDTO.Username}");

                var model = context.Services.Resolve<IModelRegistry>().Get<PlayerModel>();
                model.Initialize(new List<PlayerDTO>() { enterGame.PlayerDTO });
                Debug.Log($"玩家数据初始化成功，PlayerName={enterGame.PlayerDTO.Username}");
            }
            catch (Exception e)
            {
                Debug.LogError($"进入游戏失败，错误信息：{e.Message}");
                netholdingView.Stop();
                throw;
            }

            try
            {
                string hubUrl = socketUrl.TrimEnd('/') + "/gamehub";
                Debug.Log($"连接游戏服务器，URL={hubUrl} + token : {accountDto.Token}");

                //string testUrl = "wss://localhost:7289/gamehub";
                await network.Connect(hubUrl, accountDto.Token);
            }
            catch (Exception e)
            {
                Debug.LogError($"连接游戏服务器失败，错误信息：{e.Message}");
                throw;
            }




            var provider = context.Services.Resolve<ITransitionProvider>();
            var transition = await provider.InstantiateAsync(TransitionType.SMFadeTransition.ToString());
            await transition.TransitionOut();
            await context.Services.Resolve<ISceneFlow>().EnterAsync<SceneMainState>();
            await transition.TransitionIn();


            //var httpRequest = sceneContext.Facade.GetHttpRequest();
            //var network = sceneContext.Facade.GetNetworkManager();
            ////todo:这里的accountDTO应该改成requestXXX，responseXXX，
            //AccountDTO loginTask = null;
            //var url = parameters[0] as string;
            //var req = parameters[1] as AccountDTO;

            //var urlEnter = parameters[2] as string;
            //var reqEnter = parameters[3] as ReqEnterGame;

            //var socketUrl = parameters[4] as string;

            //Debug.Log($"开始登录，URL={url}，Uid={req.Uid}");
            //var netholdingView = sceneContext.Facade.GetViewControllerContainer().GetViewController(nameof(UIPanelNetworkHoldingView)) as UIPanelNetworkHoldingView;
            //try
            //{

            //    loginTask = await httpRequest.HttpRequestAsync<AccountDTO, AccountDTO>(url, req, null,netholdingView);
            //}
            //catch (Exception e)
            //{
            //    var view = sceneContext.Facade.GetViewControllerContainer().GetViewController(nameof(UIPanelWarningMessageView)) as UIPanelWarningMessageView;
            //    view.Open(new UIPanelWarningMessageViewData() { prefabName = nameof(UIPanelWarningMessage),  message = $"登录失败，错误信息：{e.Message}" });
            //    Debug.LogError($"登录失败，错误信息：{e.Message}");
            //    (netholdingView as IRunable).Stop();
            //    throw;

            //}
            //Debug.Log($"登录成功，Token={loginTask.Token}");
            //SetToken(httpRequest, loginTask.Token);

            //try
            //{
            //    //var netholdingView = sceneContext.Facade.GetViewControllerContainer().GetViewController(nameof(UIPanelNetworkHoldingView)) as UIPanelNetworkHoldingView;
            //    var enterGame = await httpRequest.HttpRequestAsync<ReqEnterGame, ResEnterGame>(urlEnter, reqEnter ,null, netholdingView);
            //    Debug.Log($"进入游戏成功，SocketUrl={enterGame.PlayerDTO.Username}");
            //    var model = sceneContext.Facade.GetModelManager().GetModel<PlayerModel>();
            //    model.Initialize(new List<PlayerDTO>() { enterGame.PlayerDTO });
            //}
            //catch(Exception e)
            //{
            //    Debug.LogError($"进入游戏失败，错误信息：{e.Message}");
            //    netholdingView.Stop();
            //    throw;
            //}

            //try
            //{
            //    string hubUrl = socketUrl.TrimEnd('/') + "/gamehub";
            //    Debug.Log($"连接游戏服务器，URL={hubUrl} + token : {loginTask.Token}");

            //    string testUrl = "wss://localhost:7289/gamehub";
            //    await network.Connect(testUrl, loginTask.Token);
            //}
            //catch(Exception e)
            //{
            //    Debug.LogError($"连接游戏服务器失败，错误信息：{e.Message}");
            //    throw;
            //}



            //var transition = await sceneContext.Facade.TransitonOut(TransitionType.SMFadeTransition.ToString());
            //await sceneContext.Facade.GetSceneStateMachine().SwitchToState(GameSceneType.SceneMain.ToString(), sceneContext).AsTask();
            //await sceneContext.Facade.TransitonIn(transition);
        }

        public void SetToken(IHttpRequest http, string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                http.RemoveHeader("Authorization");
            }
            else
            {
                http.AddHeader("Authorization", $"Bearer {token}");
            }
        }
    }

}