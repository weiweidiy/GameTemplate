using JFramework.Unity;

namespace Game.MyModule
{
    public sealed class ControllerRegistryModule : IModuleInstaller
    {
        public void Install(IServiceRegistry services)
        {
            if (!services.TryResolve<IControllerRegistry>(out _))
                services.AddSingleton<IControllerRegistry>(new ControllerRegistry());

            var controllers = services.Resolve<IControllerRegistry>();
            if (!controllers.TryGet<MyModuleStartController>(out _))
                controllers.Register(new MyModuleStartController());
        }
    }
}
