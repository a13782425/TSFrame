using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed partial class Observer
{
    private Dictionary<string, EntityPoolDto> _entityPoolDic = new Dictionary<string, EntityPoolDto>();

    private Stack<Entity> _entityDefaultPool = new Stack<Entity>();

    public Observer CreatePool(string poolName, Entity origin)
    {
        if (string.IsNullOrEmpty(poolName))
            return this;
        if (_entityPoolDic.ContainsKey(poolName))
        {
            return this;
        }
        EntityPoolDto pool = new EntityPoolDto(origin);
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
                _entityDefaultPool.Push(entity);
                return this;
            }
            return this;
        }
        if (!_entityPoolDic[poolName].Contains(entity))
        {
            entity.SetValue(ActiveComponentVariable.active, false);
            _entityPoolDic[poolName].Puse(entity);
        }
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
            _componentPoolDic[component.CurrentId].Push(component);
        }

    }

    IComponent GetComponent(Int64 currentId)
    {
        if (_componentPoolDic[currentId].Count > 0)
        {
            return _componentPoolDic[currentId].Pop();
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
            _entityDic.Add(entity.GetId(), entity);
            return entity;
        }
        else
        {
            return _entityDefaultPool.Pop();
        }
    }
    Entity GetEntity(string poolName)
    {
        if (!_entityPoolDic.ContainsKey(poolName))
        {
            return GetEntity();
        }

        return _entityPoolDic[poolName].Pop();
    }
    #endregion

}

