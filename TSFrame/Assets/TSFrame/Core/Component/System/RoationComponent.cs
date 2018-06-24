using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TSFrame.ECS
{
    public class RoationComponent : IComponent, IReactiveComponent
    {
        public Int64 CurrentId
        {
            get
            {
                return OperatorIds.ROATION;
            }
        }
        [DataDriven]
        public Quaternion roation;
    }
}
