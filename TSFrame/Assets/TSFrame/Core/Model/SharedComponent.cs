using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SharedComponent
{
    private int _sharedId;
    /// <summary>
    /// 组Id
    /// </summary>
    public int SharedId { get { return _sharedId; } }

    private IComponent _currentComponent = null;
    /// <summary>
    /// 当前组件
    /// </summary>
    public IComponent CurrentComponent { get { return _currentComponent; } }
    /// <summary>
    /// 组件Id
    /// </summary>
    public Int64 CurrentId { get { return _currentComponent.CurrentId; } }

    private HashSet<Entity> _sharedEntityHashSet;

    public HashSet<Entity> SharedEntityHashSet { get { return _sharedEntityHashSet; } }

    public int ReferenceCount { get { return SharedEntityHashSet.Count; } }


    public SharedComponent(IComponent com, int shardId)
    {
        if (com == null)
        {
            throw new Exception("组件实体为空");
        }
        _currentComponent = com;
        _sharedId = shardId;
        _sharedEntityHashSet = new HashSet<Entity>();
    }
}