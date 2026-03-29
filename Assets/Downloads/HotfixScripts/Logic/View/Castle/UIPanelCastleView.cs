using JFramework;
using JFramework.Unity;
using UnityEngine;

namespace Game
{
    public class UIPanelCastleViewData : ViewData
    {
        public Color color = Color.red;
    }

    public class UIPanelCastleView : View
    {
        private UIPanelCastle panel;
        private TestCfgData cfgData;

        public override void Open<TArg>(TArg args)
        {
            var data = args as UIPanelCastleViewData;
            var configManager = context.Facade.GetConfigManager();
            cfgData = configManager.Get<TestCfgData>("3");
            var color = data?.color ?? Color.white;

            panel = GetUIManager().ShowPanel(args.prefabName, new UIPanelCastleProperties()
            {
                cfgData = cfgData,
                color = color,
            }) as UIPanelCastle;
        }


        public override void Close()
        {
        }

        public override void Refresh<TArg>(TArg args)
        {
            var data = args as UIPanelCastleViewData;
            if (panel == null) return;

            var color = data?.color ?? Color.white;
            panel.Show(new UIPanelCastleProperties()
            {
                cfgData = cfgData,
                color = color,
            });
        }
    }
}