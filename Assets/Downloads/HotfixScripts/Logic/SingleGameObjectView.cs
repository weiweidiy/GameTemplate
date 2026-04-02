using JFramework.Unity;
using System;
using UnityEngine;

namespace Game
{
    public class SingleGameObjectViewData : ViewData
    {
        public Transform parent; //父物体
    }

    public abstract class SingleGameObjectView : View
    {
        protected GameObject go;

        public override void Close()
        {
            var goManager = GetGameObjectManager();
            goManager.Return(go);
        }

        public override void Open<TArg>(TArg args)
        {
            var prefabName = (args as SingleGameObjectViewData)?.prefabName;
            var parent = (args as SingleGameObjectViewData)?.parent;
            var goManager = GetGameObjectManager();
            go = goManager.Rent(prefabName, parent);
        }

        public override void Refresh<TArg>(TArg args)
        {
            throw new NotImplementedException();
        }
    }

}