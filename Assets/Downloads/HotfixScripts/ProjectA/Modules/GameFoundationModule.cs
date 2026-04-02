using JFramework;
using JFramework.Game;
using JFramework.Unity;

namespace Game.ProjectA
{
    public class GameFoundationModule : DefaultFoundationModule
    {
        //public override void Install(IServiceRegistry services)
        //{
        //    base.Install(services);

        //    // 这里用于安装游戏基础设施相关服务。
        //    // 可在此注册日志、网络、配置、资源加载、性能监控等通用能力。
        //    // 建议仅放置跨业务模块共享的基础服务。
        //}

        public override void Install(IServiceRegistry services)
        {
            base.Install(services);

            //这里可以安装一些基础设施相关的服务，比如日志系统、性能监视系统等等

            var network = InstallNetworkManager(services);
        }

        private object InstallNetworkManager(IServiceRegistry services)
        {
            if (!services.TryResolve<IJNetwork>(out var networkManageer))
            {
                var builder = new JNetworkBuilder()
                                        .SetSocket(new SignalRSocket())
                                        .SetProtocolRegister(new AutoNetMessageRegister())
                                        .SetMessageHandler(new MessageHandler());

                networkManageer = builder.Build();

                services.AddSingleton<IJNetwork>(networkManageer);
            }
            return networkManageer;
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
