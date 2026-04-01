using System;
using Cysharp.Threading.Tasks;
using JFramework;
using JFramework.Game;
using JFramework.Unity;
using UnityEngine;

namespace Game.MyModule
{
    public sealed class LoginState : BaseSceneState
    {

        public override async UniTask EnterAsync(ISceneContext context, object arg)
        {

            await base.EnterAsync(context, arg);

            Debug.Log("LoginState");
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var gameContext = new GameContext
            {
                Services = context.Services
            };

            var controller = context.Controllers.Get<MyModuleStartController>();
            await controller.Do(gameContext);

            var model = context.Models.Get<MyModuleStatusModel>();
            var views = context.Views.GetViewsForScene(typeof(LoginState));

            foreach (var view in views)
            {
                view.Start(gameContext);

                if( view is UIPanelLoginView loginView)
                {
                    loginView.Open(new ViewData { prefabName = model.ViewName });
                    loginView.onLoginClicked += LoginView_onLoginClicked;
                }

                if(view is LoginBackgroundView loginBgView)
                {
                    var cfgData = context.Services.Resolve<IJConfigManager>().Get<PrefabsCfgData>("2");
                    var prefabName = cfgData.PrefabName;
                    loginBgView.Open(new SingleGameObjectViewData { prefabName = prefabName, parent = goRoot });
                }

            }
        }

        private void LoginView_onLoginClicked(string obj)
        {
            context.Services.Resolve<ISceneFlow>().EnterAsync<MainState>();
        }

        public UniTask ExitAsync()
        {
            return UniTask.CompletedTask;
        }

        protected override string GetBGMClipName()
        {
            return "";
        }

        protected override DemoSceneType GetSceneType()
        {
            return DemoSceneType.SceneLogin;
        }
        protected override string GetUISettingsName()
        {
            return "UISceneLoginSettings";
        }

 
    }
}
