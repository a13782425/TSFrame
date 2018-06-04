using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InstantiateComponent : IComponent, IReactiveComponent
{
    public Int64 CurrentId
    {
        get
        {
            return ComponentIds.INSTANTIATE;
        }
    }
    [DataDriven]
    private string prefabName = "";
    private Transform parent = null;
    private Vector3 pos;
    private Quaternion rot;
    private HideFlags hideFlags = HideFlags.None;
}

