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
    //[Obsolete("外界不要调用")]
    //public void DataDrivenMethod(Entity entity, IComponent com)
    //{
    //    if (entity == null)
    //    {
    //        return;
    //    }
    //    int sharedId = entity.GetSharedId(com.CurrentId);
    //    if (sharedId > 0)
    //    {
    //        if (_sharedComponentDic.ContainsKey(sharedId))
    //        {
    //            foreach (KeyValuePair<Int64, Entity> item in _sharedComponentDic[sharedId].SharedEntityDic)
    //            {
    //                DataDrivenMethod(item.Value, com.CurrentId);
    //            }
    //        }
    //        else
    //        {
    //            DataDrivenMethod(entity, com.CurrentId);
    //        }
    //    }
    //    else
    //    {
    //        DataDrivenMethod(entity, com.CurrentId);
    //    }
    //}
    [Obsolete("外界不要调用")]
    public void DataDrivenMethod(Entity entity, IComponent com)
    {
        if (entity == null)
        {
            return;
        }
        int sharedId = entity.GetSharedId(com.CurrentId);
        SharedComponent sharedComponent = null;
        if (sharedId > 0)
        {
            sharedComponent = _sharedComponentDic[sharedId];
        }

        foreach (KeyValuePair<ISystem, List<Entity>> item in _systemReactiveDic)
        {
            ComponentFlag reactiveCondition = (item.Key as IReactiveSystem).ReactiveCondition;
            ComponentFlag reactiveIgnoreCondition = (item.Key as IReactiveSystem).ReactiveIgnoreCondition;
            if (sharedId > 0)
            {
                foreach (KeyValuePair<Int64, Entity> entityDic in sharedComponent.SharedEntityDic)
                {
                    if (entityDic.Value.GetComponentFlag().HasFlag(reactiveIgnoreCondition))
                    {
                        continue;
                    }
                    if (reactiveCondition.HasFlag(com.CurrentId) && entityDic.Value.GetComponentFlag().HasFlag(reactiveCondition))
                    {
                        if (!item.Value.Contains(entityDic.Value))
                        {
                            item.Value.Add(entityDic.Value);
                        }
                    }
                }
            }
            else
            {
                if (entity.GetComponentFlag().HasFlag(reactiveIgnoreCondition))
                {
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
        //if (sharedId > 0)
        //{
        //    if (_sharedComponentDic.ContainsKey(sharedId))
        //    {
        //        foreach (KeyValuePair<Int64, Entity> item in _sharedComponentDic[sharedId].SharedEntityDic)
        //        {
        //            DataDrivenMethod(item.Value, com.CurrentId);
        //        }
        //    }
        //    else
        //    {
        //        DataDrivenMethod(entity, com.CurrentId);
        //    }
        //}
        //else
        //{
        //    DataDrivenMethod(entity, com.CurrentId);
        //}
    }
    #endregion
    partial void DataDrivenMethod(Entity entity, Int64 componentId)
    {
        foreach (KeyValuePair<ISystem, List<Entity>> item in _systemReactiveDic)
        {
            ComponentFlag reactiveCondition = (item.Key as IReactiveSystem).ReactiveCondition;
            ComponentFlag reactiveIgnoreCondition = (item.Key as IReactiveSystem).ReactiveIgnoreCondition;
            if (entity.GetComponentFlag().HasFlag(reactiveIgnoreCondition))
            {
                continue;
            }
            if (reactiveCondition.HasFlag(componentId) && entity.GetComponentFlag().HasFlag(reactiveCondition))
            {
                if (!item.Value.Contains(entity))
                {
                    item.Value.Add(entity);
                }
            }
        }
    }
    //partial void DataDrivenMethod(Entity entity, Int64 componentId)
    //{
    //    foreach (KeyValuePair<ISystem, List<Entity>> item in _systemReactiveDic)
    //    {
    //        ComponentFlag reactiveCondition = (item.Key as IReactiveSystem).ReactiveCondition;
    //        ComponentFlag reactiveIgnoreCondition = (item.Key as IReactiveSystem).ReactiveIgnoreCondition;
    //        if (entity.GetComponentFlag().HasFlag(reactiveIgnoreCondition))
    //        {
    //            continue;
    //        }
    //        if (reactiveCondition.HasFlag(componentId) && entity.GetComponentFlag().HasFlag(reactiveCondition))
    //        {
    //            if (!item.Value.Contains(entity))
    //            {
    //                item.Value.Add(entity);
    //            }
    //        }
    //    }
    //}


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
        int sharedId = entity.GetSharedId(component.CurrentId);
        if (!entity.GetComponentFlag().HasFlag(component.CurrentId))
        {
            if (sharedId > 0)
            {
                if (_sharedComponentDic.ContainsKey(sharedId))
                {
                    if (_sharedComponentDic[sharedId].SharedEntityDic.ContainsKey(entity.GetId()))
                    {
                        _sharedComponentDic[sharedId].SharedEntityDic.Remove(entity.GetId());
                    }
                }
            }
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
            if (sharedId > 0)
            {
                if (_sharedComponentDic.ContainsKey(sharedId))
                {
                    if (!_sharedComponentDic[sharedId].SharedEntityDic.ContainsKey(entity.GetId()))
                    {
                        _sharedComponentDic[sharedId].SharedEntityDic.Add(entity.GetId(), entity);
                    }
                }
            }
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

