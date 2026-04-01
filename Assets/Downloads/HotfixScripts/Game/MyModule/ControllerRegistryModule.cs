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

            RegisterControllers(controllers);
        }

        /// <summary>
        /// 注册所的Controller到ControllerRegistry中，ControllerRegistry会负责Controller的生命周期管理，并且提供Controller的获取接口。
        /// </summary>
        /// <param name="controllers"></param>
        void RegisterControllers(IControllerRegistry controllers)
        {

            if (!controllers.TryGet<LoginController>(out _))
                controllers.Register(new LoginController());

            if (!controllers.TryGet<MatchFightController>(out _))
                controllers.Register(new MatchFightController());
        }
    }
}
