﻿using System;
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
            return OperatorIds.GAME_OBJECT;
        }
    }

    [DontCopy]
    private GameObject value;

}

