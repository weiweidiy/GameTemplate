using JFramework.Unity;
using UnityEngine;

namespace Game.MyModule
{
    [DisallowMultipleComponent]
    public sealed class GameEntry : MonoBehaviour
    {
        private IJApp app;

        private async void Start()
        {
            app = new JAppBuilder()
                .AddModule(new DefaultFoundationModule())
                .AddModule(new GameModules())
                .Build();

            await app.RunAsync();
        }

        private async void OnDestroy()
        {
            if (app != null)
            {
                await app.ShutdownAsync();
            }
        }
    }
}
