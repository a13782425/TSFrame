using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Observer : MonoBehaviour
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
