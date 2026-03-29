using System;
using JFramework.Unity;
using System.Collections.Generic;

namespace Game
{
public class GameModelManager : BaseModelManager
{
    public override void RegisterModels()
    {
        Func<PlayerDTO,string> funcPlayerModel = (dto) => dto.Uid;
        models.Add(nameof(PlayerModel), new PlayerModel(funcPlayerModel, Facade.GetEventManager() ));
    }
}
}
