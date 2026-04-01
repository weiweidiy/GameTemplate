using Adic;
using Adic.Container;
using JFramework.Unity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class GameViewManager : BaseViewManager
    {
        IInjectionContainer container;

        [Inject]
        public GameViewManager(IInjectionContainer container) 
        {
            this.container = container;
        }

        public override void RegisterViewControllers()
        {
            if (!viewControllers.ContainsKey(GameSceneType.SceneLogin.ToString()))
            {
                viewControllers.Add(GameSceneType.SceneLogin.ToString(), new List<View>());
            }

            if (!viewControllers.ContainsKey(GameSceneType.SceneMain.ToString()))
            {
                viewControllers.Add(GameSceneType.SceneMain.ToString(), new List<View>());
            }


            var sceneLoginViews = container.ResolveAll<View>(GameSceneType.SceneLogin.ToString());
            foreach (var view in sceneLoginViews)
            {
                viewControllers[GameSceneType.SceneLogin.ToString()].Add(view);
            }

            var sceneCastleViews = container.ResolveAll<View>(GameSceneType.SceneMain.ToString());
            foreach (var view in sceneCastleViews)
            {
                viewControllers[GameSceneType.SceneMain.ToString()].Add(view);
            }

        }

        void Init()
        {

        }
    }

}