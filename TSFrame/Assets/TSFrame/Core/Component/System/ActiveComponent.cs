using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ActiveComponent : IComponent, IReactiveComponent
{
    public Int64 CurrentId
    {
        get
        {
            return ComponentIds.ACTIVE;
        }
    }
    [DataDriven]
    public bool active = false;
}

