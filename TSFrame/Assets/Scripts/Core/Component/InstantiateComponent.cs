using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InstantiateComponent : IComponent
{
    public Int64 CurrentId
    {
        get
        {
            return ComponentIds.INSTANTIATE;
        }
    }

    public string PrefabName
    {
        get
        {
            return _prefabName;
        }

        set
        {
            _prefabName = value;
        }
    }
    private string _prefabName = "";

    public string InstabtiateName
    {
        get
        {
            return _instabtiateName;
        }

        set
        {
            _instabtiateName = value;
        }
    }
    private string _instabtiateName = "";

    public Transform Parent
    {
        get
        {
            return _parent;
        }

        set
        {
            _parent = value;
        }
    }
    private Transform _parent = null;

}

