using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ActiveSystem : IReactiveSystem
{
    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.ACTIVE);
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
        foreach (Entity item in entitys)
        {
            bool isActive = item.GetValue<bool>(ActiveComponentVariable.active);
            if (item.GetComponentFlag().HasFlag(ComponentIds.GAME_OBJECT))
            {
                GameObject obj = item.GetValue<GameObject>(GameObjectComponentVariable.value);
                if (obj != null)
                {
                    obj.SetActive(isActive);
                }
            }
            Observer.Instance.SetActive(item, isActive);
        }
    }
}

