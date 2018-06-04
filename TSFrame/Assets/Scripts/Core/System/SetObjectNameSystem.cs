using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SetObjectNameSystem : IReactiveSystem
{
    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.GAME_OBJECT_NAME, ComponentIds.GAME_OBJECT);
        }
    }

    public void Execute(List<Entity> entitys)
    {
        foreach (var item in entitys)
        {
            GameObject obj = item.GetValue<GameObject>(ComponentIds.GAME_OBJECT, "value");
            if (obj != null)
            {
                obj.name = item.GetValue<string>(ComponentIds.GAME_OBJECT_NAME, "name");
            }

        }
    }
}

