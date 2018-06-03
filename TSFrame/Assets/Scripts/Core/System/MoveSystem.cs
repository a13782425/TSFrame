using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MoveSystem : IInitSystem, IExecuteSystem
{
    private Group _currentGroup;
    public ComponentFlag ExecuteCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.GAME_OBJECT);
        }
    }

    public void Execute()
    {
        Debug.LogError(_currentGroup.Count);
    }

    public void Init()
    {
        _currentGroup = Observer.Instance.MatchGetGroup(ExecuteCondition);
    }
}

