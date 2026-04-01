using Cysharp.Threading.Tasks;
using JFramework.Unity;
using UnityEngine;

namespace Game.MyModule
{
    public sealed class MainState : BaseGameSceneState
    {
        public override UniTask EnterAsync(ISceneContext sceneContext, object arg)
        {
            Debug.Log("MainState");

            return base.EnterAsync(sceneContext, arg);

  
        }

        protected override string GetBGMClipName()
        {
            return "";
        }

        protected override GameSceneType GetSceneType()
        {
            return GameSceneType.SceneCastle;
        }
        protected override string GetUISettingsName()
        {
            return "UISceneCastleSettings";
        }
    }
}
