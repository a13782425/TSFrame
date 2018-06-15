using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed partial class Observer
{
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
    /// 回收实体
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="poolName"></param>
    public Observer RecoverEntity(Entity entity, string poolName)
    {
        if (string.IsNullOrEmpty(poolName) || !_entityPoolDic.ContainsKey(poolName))
        {
            if (!_entityDefaultPool.Contains(entity))
            {
                entity.RemoveComponentAll();
                _entityDefaultPool.Enqueue(entity);
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

    partial void RecoverComponent(IComponent component)
    {
        if (!_componentPoolDic[component.CurrentId].Contains(component))
        {
            _componentPoolDic[component.CurrentId].Enqueue(component);
        }

    }

    IComponent GetComponent(Int64 currentId)
    {
        if (_componentPoolDic[currentId].Count > 0)
        {
            return _componentPoolDic[currentId].Dequeue();
        }
        return Activator.CreateInstance(ComponentIds.ComponentTypeDic[currentId]) as IComponent;
    }

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
            return _entityDefaultPool.Dequeue();
        }
    }
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

