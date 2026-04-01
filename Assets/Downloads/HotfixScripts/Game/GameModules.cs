using JFramework.Unity;

namespace Game.MyModule
{
    public sealed class GameModules : IModuleInstaller
    {
        private static readonly IModuleInstaller[] Modules =
        {
            new ModelRegistryModule(),
            new ViewRegistryModule(),
            new ControllerRegistryModule(),
            new SceneRegistryModule(),
            new InitializerModule(),
        };

        public void Install(IServiceRegistry services)
        {
            foreach (var module in Modules)
            {
                module.Install(services);
            }
        }
    }
}
