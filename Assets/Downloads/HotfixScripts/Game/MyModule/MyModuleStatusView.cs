using JFramework.Unity;
using UnityEngine;

namespace Game.MyModule
{
    public sealed class MyModuleStatusView : View
    {
        public override void Open<TArg>(TArg args)
        {
            var model = context.Services.Resolve<IModelRegistry>().Get<MyModuleStatusModel>();
            Debug.Log($"[MyModuleStatusView] Open stage={model.StageName}, power={model.RecommendedPower}, ready={model.Ready}");
        }

        public override void Close()
        {
            Debug.Log("MyModuleStatusView Close");
        }

        public override void Refresh<TArg>(TArg args)
        {
            var model = context.Services.Resolve<IModelRegistry>().Get<MyModuleStatusModel>();
            Debug.Log($"[MyModuleStatusView] Refresh stage={model.StageName}, ready={model.Ready}");
        }
    }
}
