using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed partial class Observer
{
    #region Public

    public Entity CreateEntity()
    {
        return CreateEntity(null);
    }

    public Entity CreateEntity(Entity parent)
    {
        Entity entity = new Entity();
        entity.SetChangeComponent(MatchEntity);
        _entityDic.Add(entity.GetId(), entity);
        MatchEntity(entity);
        if (parent != null)
        {
            parent.ChildList.Add(entity);
            entity.Parent = parent;
        }
        return entity;
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

