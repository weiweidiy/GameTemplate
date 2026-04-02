using System.Threading.Tasks;
using JFramework.Unity;

namespace Game
{
    public class WorkingController : Controller
    {
        public override Task Do(GameContext context, params object[] parameters)
        {
            return Task.CompletedTask;
        }
    }
}
