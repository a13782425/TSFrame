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
    public ComponentFlag ReactiveIgnoreCondition
    {
        get
        {
            return Observer.Instance.GetFlag(0);
        }
    }

    public void Execute(List<Entity> entitys)
    {
        foreach (var item in entitys)
        {
            GameObject obj = item.GetValue<GameObject>(GameObjectComponentVariable.value);
            string name = item.GetValue<string>(GameObjectNameComponentVariable.name);
            if (obj != null)
            {
                obj.name = name;
            }
            else
            {
                item.SetValue(GameObjectNameComponentVariable.name, name);
            }
        }
    }
}

