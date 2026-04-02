using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JFramework.Unity;
using System;
using JFramework;
namespace Game
{
    public class PlayerModel : Model<PlayerDTO>
    {
        public PlayerModel(Func<PlayerDTO, string> keySelector, EventManager eventManager) : base(keySelector, eventManager)
        {
        }
    }
}

