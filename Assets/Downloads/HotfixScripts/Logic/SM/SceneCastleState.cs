using Cysharp.Threading.Tasks;
using JFramework;
using JFramework.Unity;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Game.Demo
{
    public class SceneCastleState : BaseSceneState
    {
        protected override async UniTask OnEnter(object arg)
        {
            await base.OnEnter(arg);

            //await OpenBackground();
            await OpenStartBattleMenu();
            await OpenCastleUIPanel();
        }

        public override UniTask OnExit()
        {
            //CloseBackground();
            CloseStartBattleMenu();
            CloseCastleUIPanel();

            return base.OnExit();
        }

        protected override string GetBGMClipName()
        {
            return "";
        }

        protected override DemoSceneType GetSceneType()
        {
            return DemoSceneType.SceneCastle;
        }

        protected override string GetUISettingsName()
        {
            return "UISceneCastleSettings";
        }

        /// <summary>
        /// 打开背景界面
        /// </summary>
        /// <returns></returns>
        async UniTask OpenBackground()
        {
            //var ctrl = GetController<BackgroundView>() ;
            //var assetsQuery = context.Facade.GetGameAssetsQuary() as GameAssetsQuary;
            //var spBackground = await assetsQuery.GetBackgroundSpriteAsync();

            //ctrl.Open(new BackgroundViewData()
            //{
            //    prefabName = "Castle"
            //    ,
            //    parent = goRoot
            //    ,
            //    sp = spBackground
            //});
        }

        void CloseBackground()
        {
            //var ctrl = GetController<BackgroundView>();
            //ctrl.Close();
        }

        /// <summary>
        /// 打开开始战斗菜单界面
        /// </summary>
        /// <returns></returns>
        async UniTask OpenStartBattleMenu()
        {
            var ctrl = GetController<UIPanelStartMenuView>();
            Debug.Assert(ctrl != null, "UIPanelStartMenuView controller is not found! Please check if it's added to the scene and registered in the SceneView.");
            ctrl.onStartFightClicked += Ctrl_onStartFightClicked;
            ctrl.Open(new ViewData() { prefabName = nameof(UIPanelStartMenu) });
            await UniTask.CompletedTask;
        }

        void CloseStartBattleMenu()
        {
            var ctrl = GetController<UIPanelStartMenuView>();
            ctrl.onStartFightClicked -= Ctrl_onStartFightClicked;
            ctrl.Close();
        }

        private async void Ctrl_onStartFightClicked()
        {
            var url = GameLauncher.ServerUrl + "api/Match/Match";
            var req = new ReqMatch() { };
            //await context.Facade.GetControllerManager().GetController(nameof(MatchFightController)).Do(context, url, req);
        }

        private async UniTask OpenCastleUIPanel()
        {
            var ctrl = GetController<UIPanelCastleView>();
            ctrl.Open(new UIPanelCastleViewData() { prefabName = nameof(UIPanelCastle) });
            await UniTask.CompletedTask;
        }

        private void CloseCastleUIPanel()
        {
            var  ctrl = GetController<UIPanelCastleView>();
            ctrl.Close();
        }
    }
}
