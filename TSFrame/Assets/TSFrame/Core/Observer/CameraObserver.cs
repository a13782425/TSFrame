using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed partial class Observer
{
    partial void CameraLoad()
    {
        _camareGameObject = new GameObject("CamareGameObject");
        _camareGameObject.transform.SetParent(this.transform);
    }

    partial void CameraUpdate()
    {

    }
}
