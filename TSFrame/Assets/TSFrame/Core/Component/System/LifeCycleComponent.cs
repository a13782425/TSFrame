using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TSFrame.ECS
{
    public class LifeCycleComponent : IComponent, IReactiveComponent
    {
        public Int64 OperatorId
        {
            get
            {
                return OperatorIds.LIFE_CYCLE;
            }
        }
        /// <summary>
        /// 生命周期
        /// </summary>
        [DataDriven]
        private LifeCycleEnum lifeCycle;
        /// <summary>
        /// 延时时间
        /// </summary>
        private float dealyTime;
    }
}

