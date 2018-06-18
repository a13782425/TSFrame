using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed partial class Observer
{
    #region Public
    /// <summary>
    /// 创建一个实体
    /// </summary>
    /// <returns></returns>
    public Entity CreateEntity()
    {
        return CreateEntity(null);
    }
    /// <summary>
    /// 创建一个实体
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public Entity CreateEntity(Entity parent)
    {
        Entity entity = GetEntity();
        if (parent != null)
        {
            parent.ChildList.Add(entity);
            entity.Parent = parent;
        }
        //entity.SetValue(ActiveComponentVariable.active, true);
        return entity;
    }
    /// <summary>
    /// 从对象池创建一个实体
    /// </summary>
    /// <param name="poolName"></param>
    /// <returns></returns>
    public Entity CreateEntityToPool(string poolName)
    {
        if (string.IsNullOrEmpty(poolName))
        {
            return CreateEntity();
        }
        return CreateEntityToPool(poolName, null);
    }
    /// <summary>
    /// 从对象池创建一个实体
    /// </summary>
    /// <param name="poolName"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public Entity CreateEntityToPool(string poolName, Entity parent)
    {
        if (string.IsNullOrEmpty(poolName))
        {
            return CreateEntity();
        }
        Entity entity = GetEntity(poolName);
        if (parent != null)
        {
            if (entity.Parent != null)
            {
                entity.Parent.ChildList.Remove(entity);
            }
            parent.ChildList.Add(entity);
            entity.Parent = parent;
        }
        return entity;
    }

    /// <summary>
    /// 设置一个实体是否可见
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="isActive"></param>
    public void SetActive(Entity entity, bool isActive)
    {
        if (entity.ChildList.Count > 0)
        {
            for (int i = 0; i < entity.ChildList.Count; i++)
            {
                SetActive(entity.ChildList[i], isActive);
            }
        }
        MatchEntity(entity, isActive);
    }


    #endregion

    partial void EntityLoad()
    {
        _entityGameObject = new GameObject("EntityGameObject");
        _entityGameObject.transform.SetParent(this.transform);
    }

    partial void EntityUpdate()
    {

    }
}

