using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed partial class Observer
{

    public TSPool GetPool(string poolName)
    {
        return new TSPool();
    }

    #region MyRegion

    partial void PoolLoad()
    {
        _poolGameObject = new GameObject("ResourcesGameObject");
        _poolGameObject.transform.SetParent(this.transform);
    }

    partial void PoolUpdate()
    {

    }

    #endregion

}

