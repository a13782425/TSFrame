using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameObjectComponent : IComponent
{
    public Int64 CurrentId
    {
        get
        {
            return ComponentIds.GAME_OBJECT;
        }
    }

    public GameObject Value
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

    private GameObject _value = null;

}

