using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class LifeCycleSystem : IInitSystem, IExecuteSystem
{
    private Group _currentGroup;
    List<Entity> _removeList = null;
    private string _scnenName = "";
    public ComponentFlag ExecuteCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.LIFE_CYCLE);
        }
    }

    public void Execute()
    {
    }

    public void Init()
    {
        _currentGroup = Observer.Instance.MatchGetGroup(ExecuteCondition);
        _removeList = new List<Entity>();
    }
}

