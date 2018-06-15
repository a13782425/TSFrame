using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InputSystem : IExecuteSystem, IInitSystem
{
    private Group _currentGroup;
    public ComponentFlag ExecuteCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.INPUT);
        }
    }

    public void Execute()
    {
        foreach (KeyValuePair<int, Entity> item in _currentGroup.EntityDic)
        {
            item.Value.SetValue(InputComponentVariable.x, Input.GetAxis("Horizontal"));
            item.Value.SetValue(InputComponentVariable.y, Input.GetAxis("Vertical"));
        }
    }

    public void Init()
    {
        _currentGroup = Observer.Instance.MatchGetGroup(ExecuteCondition);
    }
}

