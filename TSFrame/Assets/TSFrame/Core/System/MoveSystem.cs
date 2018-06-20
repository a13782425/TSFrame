﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MoveSystem : IInitSystem, IExecuteSystem
{
    private Group _currentGroup;
    private ComponentFlag _executeCondition = null;
    public ComponentFlag ExecuteCondition
    {
        get
        {
            if (_executeCondition == null)
            {
                _executeCondition = Observer.Instance.GetFlag(OperatorIds.GAME_OBJECT, OperatorIds.INPUT);
            }
            return _executeCondition;
        }
    }

    public void Execute()
    {
        foreach (Entity item in _currentGroup.EntityHashSet)
        {
            GameObject obj = item.GetValue<GameObject>(GameObjectComponentVariable.value);
            if (obj != null)
            {
                float x = item.GetValue<float>(InputComponentVariable.x);
                float y = item.GetValue<float>(InputComponentVariable.y);
                obj.transform.Translate(new Vector3(x, y, 0));
            }
        }
    }

    public void Init()
    {
        _currentGroup = Observer.Instance.MatchGetGroup(ExecuteCondition);
    }
}

