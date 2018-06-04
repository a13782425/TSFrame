using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CollisionSystem : IReactiveSystem
{
    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.TEST);
        }
    }

    public void Execute(List<Entity> entitys)
    {
    }
}

