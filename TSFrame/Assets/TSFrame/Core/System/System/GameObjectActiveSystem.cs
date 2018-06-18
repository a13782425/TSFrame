using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameObjectActiveSystem : IReactiveSystem
{
    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.ACTIVE, ComponentIds.GAME_OBJECT);
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
        foreach (Entity item in entitys)
        {
            bool isActive = item.GetValue<bool>(ActiveComponentVariable.active);
            GameObject obj = item.GetValue<GameObject>(GameObjectComponentVariable.value);
            if (obj != null)
            {
                obj.SetActive(isActive);
            }
            else
            {
                item.SetValue(ActiveComponentVariable.active, isActive);
            }
        }
    }
}

