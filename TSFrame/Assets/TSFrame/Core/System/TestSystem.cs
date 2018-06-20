using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TestSystem : IReactiveSystem
{
    private ComponentFlag _reactiveCondition = null;

    public ComponentFlag ReactiveCondition
    {
        get
        {
            if (_reactiveCondition == null)
            {
                _reactiveCondition = Observer.Instance.GetFlag(OperatorIds.TEST);
            }
            return _reactiveCondition;
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

