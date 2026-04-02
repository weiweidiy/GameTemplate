using Cysharp.Threading.Tasks;
using JFramework;
using JFramework.Game;
using JFramework.Unity;
using System.Linq;

namespace Game.ProjectA
{
    public sealed class InitializerModule : IModuleInstaller
    {
        public void Install(IServiceRegistry services)
        {
            if (!services.TryResolve<IInitializable>(out _))
            {

                services.AddSingleton<IInitializable>(new DemoRuntimeInitializer(services));

                //同一个类型，会覆盖前一个
                //services.AddSingleton<IInitializable>(new DemoCustomInitializer());
            }
        }
    }

    /// <summary>
    /// 运行时初始化器，负责在应用程序运行时进行必要的初始化操作，例如预加载配置数据等
    /// </summary>
    public sealed class DemoRuntimeInitializer : IInitializable
    {
        private readonly IJConfigManager configManager;
        IGameObjectManager gameObjectManager;
        ISpriteManager spriteManager;

        public DemoRuntimeInitializer(IServiceRegistry services)
        {
            this.configManager = services.Resolve<IJConfigManager>();
            this.gameObjectManager = services.Resolve<IGameObjectManager>();
            this.spriteManager = services.Resolve<ISpriteManager>();
        }

        public async UniTask InitializeAsync()
        {
            if (configManager != null)
                await configManager.PreloadAllAsync().AsUniTask();

            var prefabDataList = configManager.GetAll<PrefabsCfgData>();
            var prefabNames = prefabDataList.Select(x => x.PrefabName).ToList();
            var taskPreloadGo = gameObjectManager.PreloadGameObjects(prefabNames);

            var spriteDataList = configManager.GetAll<TexturesCfgData>();
            var spriteNames = spriteDataList.Select(x => x.Path).ToList();
            var taskPreloadSp = spriteManager.PreloadSprites(spriteNames);

            // 等待所有预加载任务完成
            await UniTask.WhenAll(taskPreloadGo, taskPreloadSp);
        }
    }
}
