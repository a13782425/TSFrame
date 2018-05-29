using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEntity : MonoBehaviour
{
    private Entity _currentEntity = null;

    public Entity CurrentEntity { get { return _currentEntity; } }

    public Entity<T> AddEntity<T>(Entity<T> entity) where T : Entity<T>
    {
        if (entity == null)
        {
            throw new System.Exception("Entity is null");
        }
        _currentEntity = entity;
        return entity;
    }

    public Entity AddEntity(Entity entity)
    {
        if (entity == null)
        {
            throw new System.Exception("Entity is null");
        }
        _currentEntity = entity;
        return entity;
    }

    public void Load()
    {
        //CurrentEntity.Load();
    }

    public void UnLoad()
    {
        //CurrentEntity.UnLoad();
    }
}
