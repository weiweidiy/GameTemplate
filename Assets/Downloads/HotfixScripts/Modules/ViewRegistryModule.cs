using JFramework.Unity;

namespace Game.MyModule
{

    public sealed class ViewRegistryModule : IModuleInstaller
    {
        public void Install(IServiceRegistry services)
        {


            //◊¢≤· ”Õº
            if (!services.TryResolve<IViewRegistry>(out _))
                services.AddSingleton<IViewRegistry>(new ViewRegistry());

            var views = services.Resolve<IViewRegistry>();
            views.RegisterForScene(typeof(SceneLoginState), new UIPanelLoginView());
            views.RegisterForScene(typeof(SceneLoginState), new LoginBackgroundView());
            views.RegisterForScene(typeof(SceneLoginState), new UIPanelNetworkHoldingView());
            views.RegisterForScene(typeof(SceneLoginState), new UIPanelWarningMessageView());
        }
    }
}
