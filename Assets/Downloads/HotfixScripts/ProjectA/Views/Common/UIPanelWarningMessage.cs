using System;
using UnityEngine;
using JFramework.Unity;
using deVoid.UIFramework;

namespace Game
{

    public class UIPanelWarningMessage : UIPanelBase<UIPanelWarningMessageProperties>
    {
        public event Action onComplete;

        [SerializeField] private TipsComponent textMessage;


        protected override void OnPanelShow()
        {
            base.OnPanelShow();
            textMessage.Show(Properties.message);
            textMessage.onComplete += TextMessage_onComplete;
        }


        protected override void OnPanelHide()
        {
            base.OnPanelHide();
            textMessage.onComplete -= TextMessage_onComplete;

        }


        private void TextMessage_onComplete()
        {
            onComplete?.Invoke();
        }
    }

    public class UIPanelWarningMessageProperties : PanelProperties
    {
        public string message;
    }
}
