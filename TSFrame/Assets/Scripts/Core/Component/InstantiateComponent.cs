using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InstantiateComponent : IComponent,IReactiveComponent
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
    [DataDriven]
    private string instabtiateName = "";
    [DataDriven]
    private Transform parent = null;

}

