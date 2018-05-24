using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class LinkComponent : IComponent
{
    public Int64 CurrentId
    {
        get
        {
            return ComponentIds.LINK;
        }
    }
}

