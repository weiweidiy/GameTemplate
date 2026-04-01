using JFramework;
using JFramework.Game;
using JFramework.Unity;




namespace Game
{
    public class GameFoundationModule : DefaultFoundationModule
    {
        public override void Install(IServiceRegistry services)
        {
            base.Install(services);

            //这里可以安装一些基础设施相关的服务，比如日志系统、性能监视系统等等


        }

        protected override IAssetsLoader InstallAssetsLoader(IServiceRegistry services)
        {
            if (!services.TryResolve<IAssetsLoader>(out var assetsLoader))
            {
                assetsLoader = new YooAssetsLoader();
                services.AddSingleton<IAssetsLoader>(assetsLoader);
            }
            return assetsLoader;
        }

        protected override IConfigLoader InstallConfigLoader(IServiceRegistry services)
        {
            if (!services.TryResolve<IConfigLoader>(out var configLoader))
            {
                if (services.TryResolve<IAssetsLoader>(out var assetsLoader))
                {
                    configLoader = assetsLoader as IConfigLoader;
                    services.AddSingleton<IConfigLoader>(configLoader);
                }   
            }
            return configLoader;
        }
        protected override IJConfigManager InstallJConfigManager(IServiceRegistry services, IConfigLoader configLoader, IDataConverter dataConverter)
        {
            if (!services.TryResolve<IJConfigManager>(out var jConfigManager))
            {
                jConfigManager = new GenConfigManager(configLoader, dataConverter);
                services.AddSingleton<IJConfigManager>(jConfigManager);
            }
            return jConfigManager;
        }

    }
}




