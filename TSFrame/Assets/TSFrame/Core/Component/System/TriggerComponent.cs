using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TSFrame.ECS
{
    public class TriggerComponent : IComponent, IReactiveComponent
    {
        public Int64 CurrentId
        {
            get
            {
                return OperatorIds.TRIGGER;
            }
        }

        [DataDriven]
        private bool isPhysical;

        private List<TriggerModel> triggerList;

        private TriggerCallBack enterCallBack;

        private TriggerCallBack stayCallBack;

        private TriggerCallBack exitCallBack;
    }
}
