using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using JFramework;
using JFramework.Game;
using JFramework.Unity;
using UnityEngine;

namespace Game
{
    public sealed class SceneLoginState : BaseGameSceneState
    {

        public override async UniTask EnterAsync(ISceneContext context, object arg)
        {

            await base.EnterAsync(context, arg);

            Debug.Log("LoginState");
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var loginView = context.Views.GetViewForScene<SceneLoginState,UIPanelLoginView>();
            loginView.Open(new ViewData { prefabName = "UIPanelLogin" });
            loginView.onLoginClicked += LoginView_onLoginClicked;

            var backgroundView = context.Views.GetViewForScene<SceneLoginState, LoginBackgroundView>();
            backgroundView.Open(new SingleGameObjectViewData { prefabName = "LoginEnvironment", parent = goRoot });

        }

        private async void LoginView_onLoginClicked(string account)
        {
            var accountToUse = string.IsNullOrEmpty(account) ? GameLauncher.Account : account;
            var url = GameLauncher.ServerUrl + "Account/FastLogin";
            var req = new AccountDTO() { Uid = accountToUse };
            var urlEnter = GameLauncher.ServerUrl + "api/Game/EnterGame";
            var reqEnter = new ReqEnterGame() { };
            var socketUrl = GameLauncher.ServerUrl;


            var controller = sceneContext.Controllers.Get<LoginController>();
            await controller.Do(gameContext, url, req, urlEnter, reqEnter, socketUrl);
        }

        public override UniTask ExitAsync()
        {
            var loginView = sceneContext.Views.GetViewForScene<SceneLoginState, UIPanelLoginView>();
            loginView.onLoginClicked -= LoginView_onLoginClicked;
            loginView.Close();

            var backgroundView = sceneContext.Views.GetViewForScene<SceneLoginState, LoginBackgroundView>();
            backgroundView.Close();

            return base.ExitAsync();
        }

        protected override string GetBGMClipName()
        {
            return "";
        }

        protected override GameSceneType GetSceneType()
        {
            return GameSceneType.SceneLogin;
        }
        protected override string GetUISettingsName()
        {
            return "UISceneLoginSettings";
        }


    }
}
