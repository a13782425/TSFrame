using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class ComponentBase
{
    public abstract Int64 CurrentId { get; }
    private Dictionary<string, TSProperty> memberDic = new Dictionary<string, TSProperty>();
    public object this[string propName]
    {
        get
        {
            if (memberDic.ContainsKey(propName.ToLower()))
            {
                return memberDic[propName.ToLower()].Getter(this);
            }
            throw new Exception(propName + "属性不存在数据驱动！！！");
        }
        set
        {
            CheckMember();
            if (memberDic.ContainsKey(propName.ToLower()))
            {
                memberDic[propName.ToLower()].Setter(this, value);
            }
            else
            {
                throw new Exception(propName + "属性不存在数据驱动！！！");
            }
        }
    }

    protected T GetValue<T>(string propName)
    {
        CheckMember();
        if (memberDic.ContainsKey(propName.ToLower()))
        {
            return (T)memberDic[propName.ToLower()].Getter(this);
        }
        else
        {
            throw new Exception(propName + "属性不存在数据驱动！！！");
        }
    }

    protected void SetValue(string propName, object value)
    {
        CheckMember();
        if (memberDic.ContainsKey(propName.ToLower()))
        {
            memberDic[propName.ToLower()].Setter(this, value);
        }
        else
        {
            throw new Exception(propName + "属性不存在数据驱动！！！");
        }
    }
    private void CheckMember()
    {
        if (memberDic.Count < 1)
        {
            memberDic = ILHelper.RegistPropertys(this);
        }
    }
}

