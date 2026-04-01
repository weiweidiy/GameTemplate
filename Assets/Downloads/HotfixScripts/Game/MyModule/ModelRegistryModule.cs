using JFramework.Unity;

namespace Game.MyModule
{
    public sealed class ModelRegistryModule : IModuleInstaller
    {
        public void Install(IServiceRegistry services)
        {
            if (!services.TryResolve<IModelRegistry>(out _))
                services.AddSingleton<IModelRegistry>(new ModelRegistry());


            var models = services.Resolve<IModelRegistry>();
            if (!models.TryGet<MyModuleStatusModel>(out _))
                models.Register(new MyModuleStatusModel());
        }
    }
}
