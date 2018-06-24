using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSFrame.ECS
{
    public class PoolComponent : IComponent, IReactiveComponent
    {
        public Int64 OperatorId
        {
            get
            {
                return OperatorIds.POOL;
            }
        }
        public string poolName;
        [DataDriven]
        public bool recover;
    }
}