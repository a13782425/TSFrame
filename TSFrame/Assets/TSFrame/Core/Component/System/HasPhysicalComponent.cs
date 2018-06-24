using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TSFrame.ECS
{
    public class HasPhysicalComponent : IComponent, IReactiveComponent
    {
        public Int64 OperatorId
        {
            get
            {
                return OperatorIds.HAS_PHYSICAL;
            }
        }
        [DataDriven]
        private bool isHas;
    }
}
