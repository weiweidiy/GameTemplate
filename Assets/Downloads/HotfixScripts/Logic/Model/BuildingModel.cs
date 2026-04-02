using System;
using JFramework;
using JFramework.Unity;

namespace Game
{
    public class BuildingModel : Model<BuildingDTO>
    {
        public BuildingModel(Func<BuildingDTO, string> keySelector, EventManager eventManager) : base(keySelector, eventManager)
        {
        }
    }
}
