using Cysharp.Threading.Tasks;
using Game.Demo;
using JFramework;
using JFramework.Unity;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class SceneLoginState : BaseGameSceneState
    {
        protected override async UniTask OnEnter(object arg)
        {
            await base.OnEnter(arg);

            //打开登录
            OpenLoginUIPanel();
            OpenLoginBackground();

            Debug.Log("!!!Entered Login State");
        }

        public override UniTask OnExit()
        {
            CloseLoginUIPanel();
            CloseLoginBackground();

            return base.OnExit();
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

        #region 显示/隐藏UI
        void OpenLoginUIPanel()
        {
            var ctrl = GetController<UIPanelLoginView>();
            ctrl.onLoginClicked += OnLoginClicked;
            ctrl.Open(new ViewData() { prefabName = nameof(UIPanelLogin) });
        }

        void CloseLoginUIPanel()
        {
            var ctrl = GetController<UIPanelLoginView>();
            ctrl.onLoginClicked -= OnLoginClicked;
            ctrl.Close();
        }

        private void OpenLoginBackground()
        {
            //var ctrl = GetController<LoginBackgroundView>();
            //var configManager = sceneContext.Facade.GetConfigManager();
            //var cfgData = configManager.Get<PrefabsCfgData>("2");
            //var prefabName = cfgData.PrefabName;
            //ctrl.Open(new SingleGameObjectViewData() { prefabName = prefabName, parent = null });
        }


        private void CloseLoginBackground()
        {
            //var ctrl = GetController<LoginBackgroundView>();

            //ctrl.Close();
        }
        #endregion

        #region 交互事件
        private async void OnLoginClicked(string account)
        {
            //Debug.Log("Login Clicked");
            try
            {
                var accountToUse = string.IsNullOrEmpty(account) ? GameLauncher.Account : account;
                var url = GameLauncher.ServerUrl + "Account/FastLogin";
                var req = new AccountDTO() { Uid = accountToUse };
                var urlEnter = GameLauncher.ServerUrl + "api/Game/EnterGame";
                var reqEnter = new ReqEnterGame() { };
                var socketUrl = GameLauncher.ServerUrl;

                //await sceneContext.Facade.GetControllerManager().GetController(nameof(LoginController)).Do(sceneContext, url, req, urlEnter, reqEnter, socketUrl);
            }
            catch(Exception ex)
            {
                Debug.LogError("Failed to switch to Castle State:" + ex.Message);
                throw;
            }
        }
        #endregion
    }
}
