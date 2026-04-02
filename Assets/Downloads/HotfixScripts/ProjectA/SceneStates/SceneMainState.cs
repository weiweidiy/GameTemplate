using Cysharp.Threading.Tasks;
using JFramework.Unity;
using UnityEngine;

namespace Game
{
    public sealed class SceneMainState : BaseSceneState
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

        protected override string GetSceneName()
        {
            return "SceneMain";
        }
        protected override string GetUISettingsName()
        {
            return "UISceneMainSettings";
        }
    }
}
