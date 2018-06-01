using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed partial class Observer
{
    public void AddSystem(ISystem system)
    {
        if (system != null)
        {
            if (system is IInitSystem)
            {
                _systemInitList.Add(system);
            }
            if (system is IReactiveSystem)
            {
                _systemReactiveList.Add(system);
            }
            if (system is IExecuteSystem)
            {
                _systemExecuteList.Add(system);
            }
        }
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
    }
}

