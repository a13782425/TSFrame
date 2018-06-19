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
            return Observer.Instance.GetFlag(ComponentIds.GAME_OBJECT, ComponentIds.INPUT);
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

