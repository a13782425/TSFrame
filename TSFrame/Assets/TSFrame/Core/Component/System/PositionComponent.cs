using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PositionComponent : IComponent, IReactiveComponent
{
    public Int64 CurrentId
    {
        get
        {
            return ComponentIds.POSITION;
        }
    }
    [DataDriven]
    public Vector3 position;
}

