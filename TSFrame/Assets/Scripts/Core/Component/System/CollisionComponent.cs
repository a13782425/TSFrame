using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CollisionComponent : IComponent, IReactiveComponent
{
    public Int64 CurrentId
    {
        get
        {
            return ComponentIds.COLLISION;
        }
    }

    [DataDriven]
    private bool isCollision = false;
}

