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

    private NormalComponent _currentComponent = null;
    /// <summary>
    /// 当前组件
    /// </summary>
    public NormalComponent CurrentComponent { get { return _currentComponent; } }

    private Int64 _currentId = 0L;
    /// <summary>
    /// 组件Id
    /// </summary>
    public Int64 CurrentId { get { return _currentId; } }

    private HashSet<Entity> _sharedEntityHashSet;

    public HashSet<Entity> SharedEntityHashSet { get { return _sharedEntityHashSet; } }

    public int ReferenceCount { get { return SharedEntityHashSet.Count; } }


    public SharedComponent(NormalComponent com, int shardId)
    {
        if (com == null)
        {
            throw new Exception("组件实体为空");
        }
        _currentComponent = com;
        _sharedId = shardId;
        _currentComponent.SharedId = shardId;
        _currentId = _currentComponent.CurrentId;
        _sharedEntityHashSet = new HashSet<Entity>();
    }

    public override int GetHashCode()
    {
        return this.SharedId;
    }

}