using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TestSystem : IReactiveSystem
{
    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.TEST);
        }
    }

    public ComponentFlag ReactiveIgnoreCondition { get { return ComponentFlag.None; } }

    public void Execute(List<Entity> entitys)
    {
        foreach (Entity item in entitys)
        {
            Debug.LogError(item.GetValue<string>(TestComponentVariable.Test1));
            Debug.LogError(item.GetId());
        }
    }
}

