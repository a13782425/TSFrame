using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Group
{
    private List<Int64> _componentList = null;

    private ComponentFlag _componentFlag;

    public ComponentFlag ComponentFlag { get { return _componentFlag; } }

    private List<Entity> entityList;


    public Group(ComponentFlag flag)
    {
        _componentFlag = flag;

        entityList = new List<Entity>();
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
        entityList = new List<Entity>();
    }
    public bool HasComponent(Int32 flag)
    {
        return ComponentFlag.HasFlag(flag);
    }

    public Group AddEntity(Entity entity)
    {
        if ((entity.GetComponentFlag().LowFlag & this.ComponentFlag.LowFlag) != this.ComponentFlag.LowFlag || (entity.GetComponentFlag().HighFlag & this.ComponentFlag.HighFlag) != this.ComponentFlag.HighFlag)
        {
            throw new Exception("该组件不属于这个组!");
        }
        if (this.entityList.Exists(a => a.GetHashCode() == entity.GetHashCode()))
        {
            return this;
        }
        this.entityList.Add(entity);
        return this;
    }
    public Group RemoveEntity(Entity entity)
    {
        if (this.entityList.Exists(a => a.GetHashCode() == entity.GetHashCode()))
        {
            this.entityList.Remove(entity);
        }
        return this;
    }

    public static bool operator ==(Group g1, Group g2)
    {
        if (g1 == null && g2 == null)
        {
            return true;
        }
        if (g1 == null || g2 == null)
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