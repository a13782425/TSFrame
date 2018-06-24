using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TSFrame.ECS
{
    public class Trigger2DComponent : IComponent, IReactiveComponent
    {
        public Int64 OperatorId
        {
            get
            {
                return OperatorIds.TRIGGER2D;
            }
        }

        [DataDriven]
        private bool isPhysical;

        private List<Trigger2DModel> triggerList;

        private Trigger2DCallBack enterCallBack;

        private Trigger2DCallBack stayCallBack;

        private Trigger2DCallBack exitCallBack;
    }
}
