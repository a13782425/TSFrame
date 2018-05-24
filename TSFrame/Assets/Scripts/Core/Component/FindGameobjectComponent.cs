using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class FindGameobjectComponent : IComponent
{
    public Int64 CurrentId
    {
        get
        {
            return ComponentIds.FIND_GAMEOBJECT;
        }
    }

    public string Value
    {
        get
        {
            return _value;
        }

        set
        {
            _value = value;
        }
    }

    private string _value = "";

}

