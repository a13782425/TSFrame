using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RoationSystem : IReactiveSystem
{

    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.GAME_OBJECT, ComponentIds.ROATION);
        }
    }

    public ComponentFlag ReactiveIgnoreCondition { get { return ComponentFlag.None; } }

    public void Execute(List<Entity> entitys)
    {
        foreach (Entity item in entitys)
        {
            GameObject obj = item.GetValue<GameObject>(GameObjectComponentVariable.value);
            if (obj != null)
            {
                obj.transform.rotation = item.GetValue<Quaternion>(RoationComponentVariable.roation);
            }
            else
            {
                item.SetValue(PositionComponentVariable.position, item.GetValue<Quaternion>(RoationComponentVariable.roation));
            }
        }
    }
}

