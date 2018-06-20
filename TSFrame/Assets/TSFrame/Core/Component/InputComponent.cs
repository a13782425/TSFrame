using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class InputComponent : IComponent
{
    public long CurrentId
    {
        get
        {
            return OperatorIds.INPUT;
        }
    }
    private float x;
    private float y;
}

