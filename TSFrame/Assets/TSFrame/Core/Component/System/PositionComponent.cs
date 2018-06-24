using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TSFrame.ECS
{
    public class PositionComponent : IComponent, IReactiveComponent
    {
        public Int64 OperatorId
        {
            get
            {
                return OperatorIds.POSITION;
            }
        }
        [DataDriven]
        public Vector3 position;
    }
}

