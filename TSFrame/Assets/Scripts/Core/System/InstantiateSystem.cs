using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSystem : IInitSystem, IReactiveSystem
{
    private Group _currentGroup;

    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.INSTANTIATE, ComponentIds.GAME_OBJECT);
        }
    }

    public void Execute(List<Entity> entitys)
    {
        Debug.LogError("InstantiateSystem");
    }

    public void Init()
    {
        _currentGroup = Observer.Instance.MatchGetGroup(ReactiveCondition);
    }
}
