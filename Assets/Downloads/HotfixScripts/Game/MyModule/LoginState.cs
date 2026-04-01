using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using JFramework;
using JFramework.Game;
using JFramework.Unity;
using UnityEngine;

namespace Game.MyModule
{
    public sealed class LoginState : BaseGameSceneState
    {

        public override async UniTask EnterAsync(ISceneContext context, object arg)
        {

            await base.EnterAsync(context, arg);

            Debug.Log("LoginState");
            if (context == null)
                throw new ArgumentNullException(nameof(context));


            var model = context.Models.Get<MyModuleStatusModel>();
            //var views = context.Views.GetViewsForScene(typeof(LoginState));

            //foreach (var view in views)
            //{
            //    view.Start(gameContext);

            //    if (view is UIPanelLoginView loginView)
            //    {
            //        loginView.Open(new ViewData { prefabName = model.ViewName });
            //        loginView.onLoginClicked += LoginView_onLoginClicked;
            //    }

            //    if (view is LoginBackgroundView loginBgView)
            //    {
            //        var cfgData = context.Services.Resolve<IJConfigManager>().Get<PrefabsCfgData>("2");
            //        var prefabName = cfgData.PrefabName;
            //        loginBgView.Open(new SingleGameObjectViewData { prefabName = prefabName, parent = goRoot });
            //    }

            //}

            var loginView = context.Views.GetViewForScene<LoginState,UIPanelLoginView>();
            loginView.Open(new ViewData { prefabName = model.ViewName });
            loginView.onLoginClicked += LoginView_onLoginClicked;

            var backgroundView = context.Views.GetViewForScene<LoginState, LoginBackgroundView>();
            backgroundView.Open(new SingleGameObjectViewData { prefabName = "LoginEnvironment", parent = goRoot });

        }

        private async void LoginView_onLoginClicked(string account)
        {

            var controller = sceneContext.Controllers.Get<LoginController>();
            await controller.Do(gameContext);
        }

        public override UniTask ExitAsync()
        {
            var loginView = sceneContext.Views.GetViewForScene<LoginState, UIPanelLoginView>();
            loginView.onLoginClicked -= LoginView_onLoginClicked;
            loginView.Close();

            var backgroundView = sceneContext.Views.GetViewForScene<LoginState, LoginBackgroundView>();
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
