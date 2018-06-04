using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Group
{
    private List<Int64> _componentList = null;

    private ComponentFlag _componentFlag;

    public ComponentFlag ComponentFlag { get { return _componentFlag; } }

    private List<Entity> _entityList;
    /// <summary>
    /// 实体数组
    /// </summary>
    public List<Entity> EntityList { get { return _entityList; } }

    public int Count { get { return EntityList.Count; } }


    public Group(ComponentFlag flag)
    {
        _componentFlag = flag;

        _entityList = new List<Entity>();
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
        _entityList = new List<Entity>();
    }
    public bool HasComponent(Int32 flag)
    {
        return ComponentFlag.HasFlag(flag);
    }

    public Group AddEntity(Entity entity)
    {
        if (!this.ComponentFlag.HasFlag(entity.GetComponentFlag()))
        {
            throw new Exception("该组件不属于这个组!");
        }
        if (this._entityList.Exists(a => a.GetHashCode() == entity.GetHashCode()))
        {
            return this;
        }
        this._entityList.Add(entity);
        return this;
    }
    public Group RemoveEntity(Entity entity)
    {
        if (this._entityList.Exists(a => a.GetHashCode() == entity.GetHashCode()))
        {
            this._entityList.Remove(entity);
        }
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
        return this.ComponentFlag.GetHashCode();// base.GetHashCode();
    }
}