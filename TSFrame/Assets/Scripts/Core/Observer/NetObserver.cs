using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed partial class Observer
{
    partial void NetLoad()
    {
        _netGameObject = new GameObject("NetGameObject");
        _netGameObject.transform.SetParent(this.transform);
    }

    partial void NetUpdate()
    {

    }
}
