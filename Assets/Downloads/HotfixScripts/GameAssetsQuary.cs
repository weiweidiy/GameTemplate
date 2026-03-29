using Cysharp.Threading.Tasks;
using JFramework;
using JFramework.Game;
using JFramework.Unity;
using UnityEngine;




namespace Game
{
    public class GameAssetsQuary : IGameAssetsQuary
    {
        /// <summary>
        /// 配置表管理器，使用方法： var cfgData = configManager.Get<TexturesCfgData>(uid);
        /// </summary>
        IJConfigManager configManager;

        /// <summary>
        /// model管理器，使用方法： var model = modelManager.Get<PlayerModel>();
        /// </summary>
        IModelManager modelManager;

        /// <summary>
        /// 精灵纹理管理器，使用方法： 预加载过的可以 var sp = spriteManager.GetSprite(spName);  没有预加载过的可以 var sp = await spriteManager.LoadSpriteAsync(spName); 
        /// </summary>
        ISpriteManager spriteManager;


        public void SetFacade(IJFacade facade)
        {
            configManager = facade.GetConfigManager();
            modelManager = facade.GetModelManager();
            spriteManager = facade.GetSpriteManager();
            
        }

        public UniTask<Sprite> GetBackgroundSpriteAsync()
        {
            if(spriteManager == null)
            {
                Debug.LogError("SpriteManager is null 请调用SetFacade 设置facade");
                return UniTask.FromResult<Sprite>(null);
            }
            //to do: 通过configManager获取名字，这里只是演示 ：
            //var uid = "1";
            //var spName = configManager.Get<TexturesCfgData>(uid).Path;
            var spName = "4pd_img_background_lobby_1";
            var sp = spriteManager.GetSprite(spName); //这里必须是配置表中有预加载的资源，否则使用LoadSpriteAsync动态加载

            return UniTask.FromResult(sp);
        }
    }
}

//container.Bind<SceneSM>().ToSingleton<SceneSM>();
//container.Bind<SceneController>().ToSingleton<SceneController>();
//container.Bind<SceneTransitionController>().ToSingleton<SceneTransitionController>();
//container.Bind<BackgroundController>().ToSingleton<BackgroundController>();
//container.Bind<SlotsController>().ToSingleton<SlotsController>();
//container.Bind<WarriorControllerManager>().ToSingleton<WarriorControllerManager>();
//container.Bind<PlayerControllerManager>().ToSingleton<PlayerControllerManager>();

//container.Bind<LevelsManager>().ToSingleton();
//container.Bind<ITypeRegister>().ToSingleton<TiktokServerNetMessageRegister>().As("Server");
//container.Bind<IMessageTypeResolver>().ToSingleton<CommonServerJNetMessageTypeResolver>().As("Server");
//container.Bind<INetworkMessageProcessStrate>().ToSingleton<CommonServerJNetworkMessageProcessStrate>().As("Server");
//container.Bind<FakeServer>().ToSingleton();         
//container.Bind<FakeNotifier>().ToSingleton();


//container.Bind<BattleCalculator>().ToSingleton<BattleCalculator>();
//container.Bind<BattleSystem>().ToSingleton<BattleSystem>();

//container.RegisterCommand<CommandStartupGame>();
//container.RegisterCommand<CommandSwitchToBattleScene>();
//container.RegisterCommand<CommandNextBattleState>();




