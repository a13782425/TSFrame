using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Group
{
    private ComponentFlag _componentFlag;

    public ComponentFlag ComponentFlag { get { return _componentFlag; } }

    private HashSet<Entity> _entityHashSet;
    /// <summary>
    /// 实体字典
    /// </summary>
    public HashSet<Entity> EntityHashSet { get { return _entityHashSet; } }
    /// <summary>
    /// 实体数量
    /// </summary>
    public int Count { get { return EntityHashSet.Count; } }

    private int _hashCode = -1;


    public Group(ComponentFlag flag)
    {
        _componentFlag = flag;
        _hashCode = _componentFlag.GetHashCode();
        _entityHashSet = new HashSet<Entity>();
    }
    public Group(params Int64[] array)
    {
        _componentFlag = new ComponentFlag();
        if (array != null)
        {
            for (int i = 0; i < array.Length; i++)
            {
                ComponentFlag.SetFlag(array[i]);
            }
        }
        _hashCode = _componentFlag.GetHashCode();
        _entityHashSet = new HashSet<Entity>();
    }
    public bool HasComponent(Int32 flag)
    {
        return ComponentFlag.HasFlag(flag);
    }

    public Group AddEntity(Entity entity)
    {
        //if (this._entityDic.ContainsKey(entity.GetId()))
        //{
        //    return this;
        //}
        this.EntityHashSet.Add(entity);
        return this;
    }
    public Group RemoveEntity(Entity entity)
    {
        this.EntityHashSet.Remove(entity);
        return this;
    }

    public static bool operator ==(Group g1, Group g2)
    {
        object o1 = g1;
        object o2 = g2;
        if (o1 == null && o2 == null)
        {
            return true;
        }
        if (o1 == null || o2 == null)
        {
            return false;
        }
        return g1.ComponentFlag == g2.ComponentFlag;
    }
    public static bool operator !=(Group g1, Group g2)
    {
        return !(g1 == g2);
    }
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return _hashCode;//this.ComponentFlag.GetHashCode();// base.GetHashCode();
    }
}