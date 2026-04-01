using Cysharp.Threading.Tasks;
using JFramework.Unity;
using System;

namespace Game.Demo
{
    public class SceneInitState : BaseGameSceneState
    {


        protected override UniTask OnEnter(object arg)
        {
            return UniTask.CompletedTask;
        }
        public override UniTask OnExit()
        {
            return UniTask.CompletedTask;
        }

        protected override string GetBGMClipName()
        {
            return "";
        }

        //protected override View[] GetViews()
        //{
        //    return null;
        //}

        protected override GameSceneType GetSceneType()
        {
            return GameSceneType.Init;
        }

        protected override string GetUISettingsName()
        {
            return "";
        }

    }
}
