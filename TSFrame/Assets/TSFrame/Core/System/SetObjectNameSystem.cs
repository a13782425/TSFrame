using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SetObjectNameSystem : IReactiveSystem
{
    private ComponentFlag _reactiveCondition = null;

    public ComponentFlag ReactiveCondition
    {
        get
        {
            if (_reactiveCondition == null)
            {
                _reactiveCondition = Observer.Instance.GetFlag(OperatorIds.GAME_OBJECT_NAME, OperatorIds.GAME_OBJECT);
            }
            return _reactiveCondition;
        }
    }
    public ComponentFlag ReactiveIgnoreCondition
    {
        get
        {
            return ComponentFlag.None;
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

