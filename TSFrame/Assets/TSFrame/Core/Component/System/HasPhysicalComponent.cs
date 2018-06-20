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
            return OperatorIds.HAS_PHYSICAL;
        }
    }
    [DataDriven]
    private bool isHas;
}

