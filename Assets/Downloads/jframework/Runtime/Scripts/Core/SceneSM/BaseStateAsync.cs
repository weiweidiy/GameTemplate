using Cysharp.Threading.Tasks;
using System;

namespace JFramework.Unity
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class BaseStateAsync : ISceneState
    {
        /// <summary>
        /// 上下文
        /// </summary>
        protected GameContext context;

        /// <summary>
        /// 状态机名字
        /// </summary>
        public string Name => GetType().Name;

        /// <summary>
        /// 状态进入时调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual UniTask OnEnter(GameContext context, object arg)
        {
            this.context = context;
            AddListeners();      
            return OnEnter(arg); 
        }

        /// <summary>
        /// 子类实现
        /// </summary>
        /// <returns></returns>
        protected virtual UniTask OnEnter(object arg) => UniTask.CompletedTask;

        /// <summary>
        /// 状态退出时调用
        /// </summary>
        /// <returns></returns>
        public virtual UniTask OnExit()
        {
            RemoveListeners();
            return UniTask.CompletedTask;
        }


        public virtual UniTask EnterAsync(ISceneContext sceneContext, object arg)
        {
            GameContext gameContext = null;

            if (sceneContext?.Services != null)
            {
                sceneContext.Services.TryResolve<GameContext>(out gameContext);
            }

            gameContext ??= new GameContext
            {
                Services = sceneContext?.Services
            };

            return OnEnter(gameContext, arg);
        }

        UniTask ISceneState.ExitAsync()
        {
            return OnExit();
        }

        /// <summary>
        /// 事件监听器，在状态进入时调用，子类重写
        /// </summary>
        protected virtual void AddListeners() { }
        protected virtual void RemoveListeners() { }

        protected IAssetsLoader GetAssetsLoader()
        {
            if (context?.Services != null &&
                context.Services.TryResolve<IAssetsLoader>(out var assetsLoader))
            {
                return assetsLoader;
            }

            throw new InvalidOperationException("IAssetsLoader is not registered in IServiceRegistry.");
        }

        protected IJUIManager GetUIManager()
        {
            if (context?.Services != null &&
                context.Services.TryResolve<IJUIManager>(out var uiManager))
            {
                return uiManager;
            }

            throw new InvalidOperationException("IJUIManager is not registered in IServiceRegistry.");
        }
    }
}
