using deVoid.UIFramework;
using JFramework;
using JFramework.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UIPanelCastleProperties : PanelProperties
    {
        public Color color;
        public TestCfgData cfgData;

        public UIPanelCastleProperties()
        {
            Debug.Log("UIPanelCastleProperties");
        }
    }

    public class UIPanelCastle : UIPanelBase<UIPanelCastleProperties>
    {
        [SerializeField] private Image imgTest;
        [SerializeField] private TextMeshProUGUI txtTest;

        protected override void OnPanelEnable()
        {
            base.OnPanelEnable();
            Debug.Log("UIPanelCastle OnPanelEnable");

            txtTest.text = Properties?.cfgData != null ? Properties.cfgData.StringValue : string.Empty;
        }

        protected override void OnPanelRefresh(UIPanelCastleProperties properties)
        {
            base.OnPanelRefresh(properties);
            Debug.Log("UIPanelCastle OnPanelRefresh");

            imgTest.color = properties.color;
        }

        protected override void OnPanelShow()
        {
            base.OnPanelShow();
            Debug.Log("UIPanelCastle OnPanelShow");

            // txtTest.text = Properties.cfgData.StringValue;
        }

        protected override void OnPanelHide()
        {
            base.OnPanelHide();
            Debug.Log("UIPanelCastle OnPanelHide");
        }
    }
}