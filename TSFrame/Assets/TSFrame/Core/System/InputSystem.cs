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
        foreach (var item in _currentGroup.EntityList)
        {
            item.SetValue(ComponentIds.INPUT, "x", Input.GetAxis("Horizontal"));
            item.SetValue(ComponentIds.INPUT, "y", Input.GetAxis("Vertical"));
        }
    }

    public void Init()
    {
        _currentGroup = Observer.Instance.MatchGetGroup(ExecuteCondition);
    }
}

