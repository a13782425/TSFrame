using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class StringComponent : IComponent
{
    public Int64 CurrentId
    {
        get
        {
            return ComponentIds.STRING;
        }
    }

    public string Value
    {
        get
        {
            return value;
        }

        set
        {
            this.value = value;
        }
    }

    private string value = "";
}

