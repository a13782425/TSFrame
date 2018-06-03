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
        foreach (IInitSystem item in _systemInitList)
        {
            item.Init();
        }
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
                (item.Key as IReactiveSystem).Execute(item.Value);
                item.Value.Clear();
            }
        }
    }
}

