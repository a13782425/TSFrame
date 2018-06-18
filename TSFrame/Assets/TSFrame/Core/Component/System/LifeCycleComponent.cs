using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class LifeCycleComponent : IComponent,IReactiveComponent
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
    [DataDriven]
    private LifeCycleEnum lifeCycle;
    /// <summary>
    /// 延时时间
    /// </summary>
    private float dealyTime;
}

