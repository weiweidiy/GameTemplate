using System;
using System.Threading.Tasks;
using JFramework;
using JFramework.Unity;
namespace Game
{
    public class UIPanelNetworkHoldingView : View, IRunable
    {
        UIPanelNetworkHolding panel;

        public RunableExtraData ExtraData { get; set; }
        public bool IsRunning { get ; set; }

        public event Action<IRunable> onComplete;

        public override void Close()
        {
            GetUIManager().HidePanel(panel.ScreenId);
        }

        public override void Open<TArg>(TArg args)
        {
            panel = GetUIManager().ShowPanel(args.prefabName, new UIPanelNetworkHoldingProperties()) as UIPanelNetworkHolding;
        }

        public override void Refresh<TArg>(TArg args)
        {
           // throw new System.NotImplementedException();
        }

        Task IRunable.Start(RunableExtraData extraData, TaskCompletionSource<bool> tcs = null)
        {
            panel = GetUIManager().ShowPanel(nameof(UIPanelNetworkHolding), new UIPanelNetworkHoldingProperties()) as UIPanelNetworkHolding;
            return Task.CompletedTask;
        }

       void IRunable.Stop()
        {
            GetUIManager().HidePanel(panel.ScreenId);
            onComplete?.Invoke(this);
        }

        public void Update(RunableExtraData extraData)
        {
            throw new NotImplementedException();
        }
    }
}
