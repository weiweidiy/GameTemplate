using JFramework;
using JFramework.Unity;

namespace Game.MyModule
{
    public sealed class ModelRegistryModule : IModuleInstaller
    {
        public void Install(IServiceRegistry services)
        {
            if (!services.TryResolve<IModelRegistry>(out _))
                services.AddSingleton<IModelRegistry>(new ModelRegistry());

            var eventManager = services.Resolve<EventManager>();


            var models = services.Resolve<IModelRegistry>();
            if (!models.TryGet<PlayerModel>(out _))
                models.Register(new PlayerModel(((dto) => dto.Uid), eventManager));
        }
    }
}
