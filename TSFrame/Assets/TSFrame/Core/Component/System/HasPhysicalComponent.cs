using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class HasPhysicalComponent : IComponent, IReactiveComponent
{
    public Int64 CurrentId
    {
        get
        {
            return ComponentIds.HAS_PHYSICAL;
        }
    }
    [DataDriven]
    private bool isHas;
}

