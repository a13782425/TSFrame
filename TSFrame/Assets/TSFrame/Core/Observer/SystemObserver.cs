using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed partial class Observer
{
    public Observer AddSystem(ISystem system)
    {
        if (system != null)
        {
            if (system is IInitSystem)
            {
                if (!_systemInitList.Contains(system))
                {
                    _systemInitList.Add(system);
                    (system as IInitSystem).Init();
                }
            }
            if (system is IReactiveSystem)
            {
                if (!_systemReactiveDic.ContainsKey(system))
                {
                    _systemReactiveDic.Add(system, new List<Entity>());
                }
            }
            if (system is IExecuteSystem)
            {
                if (!_systemExecuteList.Contains(system))
                {
                    _systemExecuteList.Add(system);
                }
            }
        }
        return this;
    }



    partial void SystemLoad()
    {
        _systemGameObject = new GameObject("SystemGameObject");
        _systemGameObject.transform.SetParent(this.transform);
        AddSystem(new ActiveSystem());
        AddSystem(new ViewSystem());
        AddSystem(new LifeCycleSystem());
        AddSystem(new HasPhysicalSystem());
        AddSystem(new Collision2DSystem());
        AddSystem(new CollisionSystem());
        AddSystem(new TriggerSystem());
        AddSystem(new Trigger2DSystem());
    }

    partial void SystemUpdate()
    {
        if (_systemExecuteList.Count > 0)
        {
            foreach (IExecuteSystem item in _systemExecuteList)
            {
                item.Execute();
            }
        }
        foreach (KeyValuePair<ISystem, List<Entity>> item in _systemReactiveDic)
        {
            if (item.Value.Count > 0)
            {
                List<Entity> temp = new List<Entity>();
                temp.AddRange(item.Value);
                item.Value.Clear();
                (item.Key as IReactiveSystem).Execute(temp);
            }
        }
    }
}

