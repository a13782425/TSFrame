using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InputSystem : IExecuteSystem, IInitSystem
{
    private Group _currentGroup;
    private ComponentFlag _executeCondition = null;
    public ComponentFlag ExecuteCondition
    {
        get
        {
            if (_executeCondition == null)
            {
                _executeCondition = Observer.Instance.GetFlag(OperatorIds.INPUT);
            }
            return _executeCondition;
        }
    }

    public void Execute()
    {
        foreach (Entity item in _currentGroup.EntityHashSet)
        {
            item.SetValue(InputComponentVariable.x, Input.GetAxis("Horizontal"));
            item.SetValue(InputComponentVariable.y, Input.GetAxis("Vertical"));
        }
    }

    public void Init()
    {
        _currentGroup = Observer.Instance.MatchGetGroup(ExecuteCondition);
    }
}

