using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed partial class Observer
{

    #region Public
    public Group MatchGetGroup(ComponentFlag flag)
    {
        if (_matchGroupDic.ContainsKey(flag))
        {
            return _matchGroupDic[flag];
        }
        Group group = new Group(flag);
        _matchGroupDic.Add(flag, group);
        return group;
    }
    public Group MatchGetGroup(params Int64[] componentIds)
    {
        if (componentIds == null)
        {
            throw new Exception("ComponentIds is null");
        }
        ComponentFlag flag = new ComponentFlag();
        for (int i = 0; i < componentIds.Length; i++)
        {
            if (flag.HasFlag(componentIds[i]))
            {
                continue;
            }
            flag.SetFlag(componentIds[i]);
        }
        return MatchGetGroup(flag);
    }

    public ComponentFlag GetFlag(params Int64[] args)
    {
        ComponentFlag flag = new ComponentFlag();
        if (args != null)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (!flag.HasFlag(args[i]))
                {
                    flag.SetFlag(args[i]);
                }
            }
        }
        return flag;
    }
    [Obsolete("外界不要调用")]
    public void DataDrivenMethod(Entity entity, IComponent com)
    {
        if (entity == null)
        {
            return;
        }
        foreach (KeyValuePair<ISystem, List<Entity>> item in _systemReactiveDic)
        {
            ComponentFlag reactiveCondition = (item.Key as IReactiveSystem).ReactiveCondition;
            ComponentFlag reactiveIgnoreCondition = (item.Key as IReactiveSystem).ReactiveIgnoreCondition;
            if (entity.GetComponentFlag().HasFlag(reactiveIgnoreCondition))
            {
                Debug.LogError(com.GetType().Name);
                continue;
            }
            if (reactiveCondition.HasFlag(com.CurrentId) && entity.GetComponentFlag().HasFlag(reactiveCondition))
            {
                if (!item.Value.Contains(entity))
                {
                    item.Value.Add(entity);
                }
            }
        }
    }
    #endregion


    partial void MatchLoad()
    {
        _matchGameObject = new GameObject("MatchGameObject");
        _matchGameObject.transform.SetParent(this.transform);
        ILHelper.SetChangeCallBack("DataDrivenMethod");
    }

    partial void MatchUpdate()
    {

    }

    partial void MatchEntity(Entity entity, bool isActive)
    {
        foreach (KeyValuePair<ComponentFlag, Group> item in _matchGroupDic)
        {
            if (isActive)
            {
                if (entity.GetComponentFlag().HasFlag(item.Key))
                {
                    item.Value.AddEntity(entity);
                }
            }
            else
            {
                item.Value.RemoveEntity(entity);
            }
        }
    }

    partial void MatchEntity(Entity entity, IComponent component)
    {

        if (!entity.GetComponentFlag().HasFlag(component.CurrentId))
        {
            foreach (KeyValuePair<ComponentFlag, Group> item in _matchGroupDic)
            {
                if (item.Key.HasFlag(component.CurrentId))
                {
                    item.Value.RemoveEntity(entity);
                }
            }
            RecoverComponent(component);
        }
        else
        {
            foreach (KeyValuePair<ComponentFlag, Group> item in _matchGroupDic)
            {
                if (entity.GetComponentFlag().HasFlag(item.Key))
                {
                    item.Value.AddEntity(entity);
                }
            }
        }
    }

}

