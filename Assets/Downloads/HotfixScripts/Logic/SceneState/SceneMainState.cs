using Cysharp.Threading.Tasks;
using JFramework.Unity;
using UnityEngine;

namespace Game
{
    public sealed class SceneMainState : BaseGameSceneState
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
            return GameSceneType.SceneMain;
        }
        protected override string GetUISettingsName()
        {
            return "UISceneMainSettings";
        }
    }
}
