using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed partial class Observer
{


    /// <summary>
    /// 创建一个对象池
    /// </summary>
    /// <param name="poolName"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    public Observer CreatePool(string poolName, Entity origin)
    {
        if (string.IsNullOrEmpty(poolName))
            return this;
        if (_entityPoolDic.ContainsKey(poolName))
        {
            return this;
        }
        EntityPoolDto pool = new EntityPoolDto(poolName, origin);
        _entityPoolDic.Add(poolName, pool);
        return this;
    }

    /// <summary>
    /// 创建共享组件
    /// </summary>
    /// <param name="componentId"></param>Int64 componentId
    /// <returns></returns>
    public SharedComponent CreateSharedComponent(Int64 componentId)
    {
        NormalComponent component = GetComponent(componentId);
        if ((component.CurrentComponent as ISharedComponent) != null)
        {
            SharedComponent shared = new SharedComponent(component, Utils.GetSharedId());
            _sharedComponentDic.Add(shared.SharedId, shared);
            return shared;
        }
        else
        {
            throw new Exception("创建共享组件失败,这不是共享组件!!!");
        }
    }
    /// <summary>
    /// 获取或创建共享组件
    /// </summary>
    /// <param name="sharedId"></param>
    /// <param name="componentId"></param>Int64 componentId
    /// <returns></returns>
    public SharedComponent GetOrCreateSharedComponent(int sharedId, Int64 componentId)
    {
        if (_sharedComponentDic.ContainsKey(sharedId))
        {
            return _sharedComponentDic[sharedId];
        }
        return CreateSharedComponent(componentId);
    }

    /// <summary>
    /// 回收实体
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="poolName"></param>
    public Observer RecoverEntity(Entity entity, string poolName = null)
    {
        if (string.IsNullOrEmpty(poolName) || !_entityPoolDic.ContainsKey(poolName))
        {
            if (_entityDefaultPool.Add(entity))
            {
                entity.RemoveComponentAll();
                return this;
            }
            return this;
        }
        entity.SetValue(ActiveComponentVariable.active, false);
        _entityPoolDic[poolName].Enqueue(entity);
        return this;
    }


    #region Implement Method

    partial void PoolLoad()
    {
        _poolGameObject = new GameObject("PoolGameObject");
        _poolGameObject.transform.SetParent(this.transform);
    }

    partial void PoolUpdate()
    {
    }
    /// <summary>
    /// 回收一个组件
    /// </summary>
    /// <param name="component"></param>
    partial void RecoverComponent(NormalComponent component)
    {
        if (component == null)
        {
            throw new Exception("回收的组件有误!!!");
        }

        _componentPoolDic[component.CurrentId].Enqueue(component);
    }
    /// <summary>
    /// 获取一个组件
    /// </summary>
    /// <param name="currentId"></param>
    /// <returns></returns>
    NormalComponent GetComponent(Int64 componentId)
    {
        if (!_componentPoolDic.ContainsKey(componentId))
        {
            throw new Exception("需要获取的组件不存在");
        }
        return _componentPoolDic[componentId].Dequeue();
    }
    /// <summary>
    /// 获取一个实体
    /// </summary>
    /// <returns></returns>
    Entity GetEntity()
    {
        if (_entityDefaultPool.Count < 1)
        {
            Entity entity = new Entity(MatchEntity, GetComponent);
            entity.AddComponent(ComponentIds.ACTIVE);
            entity.AddComponent(ComponentIds.LIFE_CYCLE);
            entity.AddComponent(ComponentIds.POOL);
            _entityDic.Add(entity.GetId(), entity);
            return entity;
        }
        else
        {
            Entity entity = null;
            foreach (Entity item in _entityDefaultPool)
            {
                entity = item;
            }
            _entityDefaultPool.Remove(entity);
            return entity;
        }
    }
    /// <summary>
    /// 获取一个实体从对象池
    /// </summary>
    /// <param name="poolName"></param>
    /// <returns></returns>
    Entity GetEntity(string poolName)
    {
        if (!_entityPoolDic.ContainsKey(poolName))
        {
            return GetEntity();
        }

        return _entityPoolDic[poolName].Dequeue();
    }
    #endregion

}

