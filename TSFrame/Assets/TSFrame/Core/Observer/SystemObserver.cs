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
                    _systemReactiveDic.Add(system, new Dictionary<int, Entity>());
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

    /// <summary>
    /// 删除系统
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Observer RemoveSystem<T>()
    {
        IInitSystem initSystem = null;
        foreach (IInitSystem item in _systemInitList)
        {
            if (item.GetType() == typeof(T))
            {
                initSystem = item;
                break;
            }
        }
        if (initSystem != null)
            _systemInitList.Remove(initSystem);
        ISystem reactiveSystem = null;
        foreach (KeyValuePair<ISystem, Dictionary<int, Entity>> item in _systemReactiveDic)
        {
            if (item.Key.GetType() == typeof(T))
            {
                reactiveSystem = item.Key;
                break;
            }
        }
        if (reactiveSystem != null)
            _systemReactiveDic.Remove(reactiveSystem);
        IExecuteSystem executeSystem = null;
        foreach (IExecuteSystem item in _systemExecuteList)
        {
            if (item.GetType() == typeof(T))
            {
                executeSystem = item;
                break;
            }
        }
        if (executeSystem != null)
            _systemExecuteList.Remove(executeSystem);
        return this;
    }


    partial void SystemLoad()
    {
        _systemGameObject = new GameObject("SystemGameObject");
        _systemGameObject.transform.SetParent(this.transform);
        AddSystem(new ActiveSystem());
        AddSystem(new ViewSystem());
        AddSystem(new LifeCycleSystem());
        AddSystem(new GameObjectLifeCycleSystem());
        AddSystem(new GameObjectActiveSystem());
        AddSystem(new PoolSystem());
        AddSystem(new HasPhysicalSystem());
        AddSystem(new Collision2DSystem());
        AddSystem(new CollisionSystem());
        AddSystem(new TriggerSystem());
        AddSystem(new Trigger2DSystem());
        AddSystem(new PositionSystem());
        AddSystem(new RoationSystem());
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
        foreach (KeyValuePair<ISystem, Dictionary<int, Entity>> item in _systemReactiveDic)
        {
            if (item.Value.Count > 0)
            {
                List<Entity> temp = new List<Entity>();
                temp.AddRange(item.Value.Values);
                item.Value.Clear();
                (item.Key as IReactiveSystem).Execute(temp);
            }
        }
    }
}

