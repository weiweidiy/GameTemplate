using JFramework.Unity;

namespace Game.MyModule
{
    public sealed class SceneRegistryModule : IModuleInstaller
    {
        public void Install(IServiceRegistry services)
        {

            //注册场景状态
            if (!services.TryResolve<ISceneStateRegistry>(out _))
                services.AddSingleton<ISceneStateRegistry>(new SceneStateRegistry());

            var states = services.Resolve<ISceneStateRegistry>();
            if (!states.TryGet<SceneLoginState>(out _))
                states.Register(new SceneLoginState());

            if (!states.TryGet<SceneMainState>(out _))
                states.Register(new SceneMainState());


            //注册场景上下文和游戏上下文
            if (!services.TryResolve<GameContext>(out var context))
            {
                context = new GameContext();
                services.AddSingleton(context);
            }

            if (!services.TryResolve<ISceneContext>(out _))
            {
                var sceneContext = new SceneContext(services);
                services.AddSingleton<ISceneContext>(sceneContext);
                services.AddSingleton<IViewContext>(sceneContext);
            }

            //注册场景流程
            if (!services.TryResolve<ISceneFlow>(out _))
            {
                services.AddSingleton<ISceneFlow>(
                    new SceneFlowService(
                        services.Resolve<ISceneStateRegistry>(),
                        services.Resolve<ISceneContext>(),
                        typeof(SceneLoginState)));
            }


        }
    }
}
