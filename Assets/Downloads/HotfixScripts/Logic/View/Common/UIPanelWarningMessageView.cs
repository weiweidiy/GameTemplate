using System;
using JFramework.Unity;
namespace Game
{
    public class UIPanelWarningMessageViewData : ViewData
    {
        public string message;
    }

    public class UIPanelWarningMessageView : View
    {
        UIPanelWarningMessage panel;
        public override void Close()
        {
            panel.onComplete -= Panel_onComplete;
            GetUIManager().HidePanel(panel.ScreenId);
        }

        public override void Open<TArg>(TArg args)
        {
            var data = args as UIPanelWarningMessageViewData;

            panel = GetUIManager().ShowPanel(args.prefabName, new UIPanelWarningMessageProperties() { message = data.message}) as UIPanelWarningMessage;
            panel.onComplete += Panel_onComplete;
        }

        private void Panel_onComplete()
        {
            Close();
        }

        public override void Refresh<TArg>(TArg args)
        {
            throw new System.NotImplementedException();
        }
    }
}
