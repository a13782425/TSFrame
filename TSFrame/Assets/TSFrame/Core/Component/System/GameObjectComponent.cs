using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TSFrame.ECS
{
    public class GameObjectComponent : IComponent
    {
        public Int64 OperatorId
        {
            get
            {
                return OperatorIds.GAME_OBJECT;
            }
        }

        [DontCopy]
        private GameObject value;

    }
}

