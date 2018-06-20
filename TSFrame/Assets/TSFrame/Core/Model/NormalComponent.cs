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
    private int _instanceId;
    /// <summary>
    /// 实例Id
    /// </summary>
    public int InstanceId { get { return _instanceId; } set { _instanceId = value; } }
    /// <summary>
    /// 共享Id
    /// </summary>
    private int _sharedId;
    /// <summary>
    /// 共享Id
    /// </summary>
    public int SharedId { get { return _sharedId; } set { _sharedId = value; } }

    private Int64 _operatorId = 0L;
    /// <summary>
    /// 组件Id
    /// </summary>
    public Int64 OperatorId { get { return _operatorId; } }

    private int _componentId = 0;
    /// <summary>
    /// 组件ID
    /// </summary>
    public int ComponentId { get { return _componentId; } }

    public NormalComponent(IComponent component, int componentId)
    {
        if (component == null || componentId < 0)
        {
            throw new Exception("组件实例为Null");
        }
        _componentId = componentId;
        CurrentComponent = component;
        _operatorId = CurrentComponent.CurrentId;
        _sharedId = -1;
    }

    public override int GetHashCode()
    {
        return _instanceId;
    }
}

