using JFramework.Unity;

namespace Game.MyModule
{

    public sealed class PresentationRegistryModule : IModuleInstaller
    {
        public void Install(IServiceRegistry services)
        {


            //◊¢≤· ”Õº
            if (!services.TryResolve<IViewRegistry>(out _))
                services.AddSingleton<IViewRegistry>(new ViewRegistry());

            var views = services.Resolve<IViewRegistry>();
            views.RegisterForScene(typeof(LoginState), new UIPanelLoginView());
            views.RegisterForScene(typeof(LoginState), new LoginBackgroundView());
        }
    }
}
