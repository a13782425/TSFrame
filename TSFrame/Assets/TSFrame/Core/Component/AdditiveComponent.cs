using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class AdditiveComponent : IComponent
{
    public Int64 CurrentId
    {
        get
        {
            return ComponentIds.ADDITIVE;
        }
    }

    public List<Int64> AdditiveComponentIds
    {
        get
        {
            return _additiveComponentIds;
        }
        set
        {
            _additiveComponentIds = value;
        }
    }

    private List<Int64> _additiveComponentIds;
}

