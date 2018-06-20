using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class NormalComponent
{
    public IComponent CurrentComponent { get; private set; }

    private TSProperty[] _propertyArray;
    public TSProperty[] PropertyArray { get { return _propertyArray; } set { _propertyArray = value; } }

    /// <summary>
    /// 实例Id
    /// </summary>
    private int _id;
    /// <summary>
    /// 实例Id
    /// </summary>
    public int Id { get { return _id; }set { _id = value; } }
    /// <summary>
    /// 共享Id
    /// </summary>
    private int _sharedId;
    /// <summary>
    /// 共享Id
    /// </summary>
    public int SharedId { get { return _sharedId; } set { _sharedId = value; } }

    private Int64 _currentId = 0L;
    /// <summary>
    /// 组件Id
    /// </summary>
    public Int64 CurrentId { get { return _currentId; } }

    public NormalComponent(IComponent component)
    {
        if (component == null)
        {
            throw new Exception("组件实例为Null");
        }
        CurrentComponent = component;
        _currentId = CurrentComponent.CurrentId;
        _sharedId = -1;
    }

    public override int GetHashCode()
    {
        return _id;
    }
}

