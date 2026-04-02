using JFramework.Unity;
using System;
using UnityEngine;

namespace Game
{


    public class SingleSpriteViewData : SingleGameObjectViewData
    {
        //public Transform parent; //父物体
        public Sprite sp;
    }

    public abstract class SingleSpriteView : SingleGameObjectView
    {
        //GameObject goBackground;

        public override void Open<TArg>(TArg args)
        {
            //var prefabName = (args as SingleSpriteViewData)?.prefabName;
            //var parent = (args as SingleSpriteViewData)?.parent;
            //var goManager = GetGameObjectManager();
            //goBackground = goManager.Rent(prefabName, parent);
            //goBackground.GetComponent<SingleSpriteComponent>().SetSrprite((args as SingleSpriteViewData)?.sp);

            base.Open<TArg>(args);

            var sp = (args as SingleSpriteViewData)?.sp;
            go.GetComponent<SingleSpriteComponent>().SetSrprite(sp);
        }

        //public override void Close()
        //{
        //    var goManager = GetGameObjectManager();
        //    goManager.Return(goBackground);
        //}

        //public override void Refresh<TArg>(TArg args)
        //{
        //    throw new NotImplementedException();
        //}
    }

}