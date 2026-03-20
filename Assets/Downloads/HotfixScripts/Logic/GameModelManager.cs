using JFramework.Unity;
using System;
using System.Collections.Generic;

namespace Game
{
    public class GameModelManager : BaseModelManager
    {
        public override void RegisterModels()
        {
            // 例如注册一个 UserModel
            // models["user"] = new UserModel(...);
            Func<PlayerDTO,string> func = (dto) => dto.Uid;
            models.Add(nameof(PlayerModel), new PlayerModel(func, Facade.GetEventManager() ));
        }


    }

}