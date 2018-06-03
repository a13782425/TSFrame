using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 组件的标记为
/// </summary>
public struct ComponentFlag
{
    private Int64 _lowFlag;
    private Int64 _highFlag;

    public Int64 LowFlag { get { return _lowFlag; } private set { _lowFlag = value; } }

    public Int64 HighFlag { get { return _highFlag; } private set { _highFlag = value; } }
    public bool HasFlag(ComponentFlag flag)
    {
        return (LowFlag & flag.LowFlag) == flag.LowFlag && (HighFlag & flag.HighFlag) == flag.HighFlag;
    }
    public Boolean HasFlag(Int64 flag)
    {
        if ((flag & ComponentIds.LOW_FLAG) == ComponentIds.LOW_FLAG)
        {
            //低位
            return (flag & LowFlag) == flag;
        }
        else
        {
            //高位
            return (flag & HighFlag) == flag;
        }
    }

    public ComponentFlag SetFlag(Int64 flag)
    {
        if ((flag & ComponentIds.LOW_FLAG) == ComponentIds.LOW_FLAG)
        {
            //低位
            LowFlag = flag | LowFlag;
        }
        else
        {
            //高位
            HighFlag = flag | HighFlag;
        }
        return this;
    }
    public ComponentFlag RemoveFlag(Int64 flag)
    {
        if ((flag & ComponentIds.LOW_FLAG) == ComponentIds.LOW_FLAG)
        {
            //低位
            LowFlag = flag ^ LowFlag;
        }
        else
        {
            //高位
            HighFlag = flag ^ HighFlag;
        }
        return this;
    }
    public static bool operator ==(ComponentFlag cf1, ComponentFlag cf2)
    {
        return cf1.LowFlag == cf2.LowFlag && cf1.HighFlag == cf2.HighFlag;
    }
    public static ComponentFlag operator &(ComponentFlag cf1, ComponentFlag cf2)
    {
        ComponentFlag temp = new ComponentFlag();
        temp.LowFlag = cf1.LowFlag & cf2.LowFlag;
        temp.HighFlag = cf1.LowFlag & cf2.HighFlag;
        return temp;
    }



    public static bool operator !=(ComponentFlag cf1, ComponentFlag cf2)
    {
        return !(cf1 == cf2);
    }
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return LowFlag.GetHashCode() ^ HighFlag.GetHashCode();
    }
}