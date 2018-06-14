using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class LifeCycleComponent : IComponent
{
    public Int64 CurrentId
    {
        get
        {
            return ComponentIds.LIFE_CYCLE;
        }
    }
    /// <summary>
    /// 生命周期
    /// </summary>
    private LifeCycleEnum lifeCycle;

}

