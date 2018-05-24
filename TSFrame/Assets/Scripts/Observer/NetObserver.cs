using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Observer : MonoBehaviour
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
