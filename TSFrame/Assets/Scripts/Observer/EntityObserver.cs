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
        Entity entity = new Entity();
        _entityDic.Add(entity.GetId(), entity);
        MatchEntity(entity);
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

