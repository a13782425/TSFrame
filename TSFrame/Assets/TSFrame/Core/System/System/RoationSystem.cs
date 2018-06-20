using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RoationSystem : IReactiveSystem
{

    private ComponentFlag _reactiveCondition = null;

    public ComponentFlag ReactiveCondition
    {
        get
        {
            if (_reactiveCondition == null)
            {
                _reactiveCondition = Observer.Instance.GetFlag(OperatorIds.ROATION, OperatorIds.GAME_OBJECT);
            }
            return _reactiveCondition;
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

