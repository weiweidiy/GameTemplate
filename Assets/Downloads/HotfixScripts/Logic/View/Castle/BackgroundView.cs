using JFramework.Unity;
using System;
using UnityEngine;

namespace Game
{


    public class BackgroundViewData : SingleGameObjectViewData
    {
        //public Transform parent; //父物体
        public Sprite sp;
    }

    public class BackgroundView : SingleGameObjectView
    {
        //GameObject goBackground;

        public override void Open<TArg>(TArg args)
        {
            //var prefabName = (args as BackgroundViewData)?.prefabName;
            //var parent = (args as BackgroundViewData)?.parent;
            //var goManager = GetGameObjectManager();
            //goBackground = goManager.Rent(prefabName, parent);
            //goBackground.GetComponent<Background>().SetBackground((args as BackgroundViewData)?.sp);

            base.Open<TArg>(args);

            var sp = (args as BackgroundViewData)?.sp;
            go.GetComponent<Background>().SetBackground(sp);
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