using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class NormalComponent
{
    public IComponent CurrentComponent { get; private set; }

    private Dictionary<string, TSProperty> _propertyDic;
    public Dictionary<string, TSProperty> PropertyDic { get { return _propertyDic; } set { _propertyDic = value; } }

    /// <summary>
    /// 实例Id
    /// </summary>
    private int _id;
    /// <summary>
    /// 共享Id
    /// </summary>
    private int _sharedId;
    /// <summary>
    /// 共享Id
    /// </summary>
    public int SharedId { get { return _sharedId; } set { _sharedId = value; } }

    public Int64 CurrentId { get { return CurrentComponent.CurrentId; } }

    public NormalComponent(IComponent component)
    {
        if (component == null)
        {
            throw new Exception("组件实例为Null");
        }
        CurrentComponent = component;
        _id = Utils.GetComponentId();
        _sharedId = -1;
    }

    public override int GetHashCode()
    {
        return _id;
    }
}

