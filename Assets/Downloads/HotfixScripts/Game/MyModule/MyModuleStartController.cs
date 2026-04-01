using System.Threading.Tasks;
using JFramework.Unity;
using UnityEngine;

namespace Game.MyModule
{
    public sealed class MyModuleStartController : Controller
    {
        public override Task Do(GameContext context, params object[] parameters)
        {
            var services = context?.Services
                ?? throw new System.InvalidOperationException("GameContext.Services is required.");

            var model = services.Resolve<IModelRegistry>().Get<MyModuleStatusModel>();
            model.Ready = true;

            Debug.Log($"[Game.MyModule] Ready for stage {model.StageName}, recommended power {model.RecommendedPower}.");
            return Task.CompletedTask;
        }
    }
}
