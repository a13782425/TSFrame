using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class ComponentIds
{
    /// <summary>
    /// 低位标签
    /// </summary>
    public const Int64 LOW_FLAG = 0;
    /// <summary>
    /// 高位标签
    /// </summary>
    public const Int64 HIGH_FLAG = 1 << 31;


    /// <summary>
    /// 字符串
    /// </summary>
    public const Int64 STRING = (1 << 0) | LOW_FLAG;
    /// <summary>
    /// 在某个系统执行完毕添加新的组件
    /// </summary>
    public const Int64 ADDITIVE = (1 << 1) | LOW_FLAG;
    /// <summary>
    /// 游戏物体
    /// </summary>
    public const Int64 GAME_OBJECT = (1 << 2) | LOW_FLAG;
    /// <summary>
    /// 实例化物体
    /// </summary>
    public const Int64 INSTANTIATE = (1 << 3) | LOW_FLAG;
    /// <summary>
    /// 查找游戏物体
    /// </summary>
    public const Int64 FIND_GAMEOBJECT = (1 << 4) | LOW_FLAG;
    /// <summary>
    /// 关联场景物体
    /// </summary>
    public const Int64 LINK = (1 << 5) | LOW_FLAG;

    /// <summary>
    /// 测试
    /// </summary>
    public const Int64 TEST = (1 << 0) | HIGH_FLAG;
    private static readonly Dictionary<Int64, Type> _componentTypeDic = new Dictionary<long, Type>();
    /// <summary>
    /// 组件字典
    /// </summary>
    public static Dictionary<Int64, Type> ComponentTypeDic { get { return _componentTypeDic; } }

    #region 构造函数

    static ComponentIds()
    {
        Type type = typeof(IComponent);
        PropertyInfo propertyInfo = type.GetProperty("CurrentId");
        Assembly assembly = type.Assembly;
        Type[] types = assembly.GetTypes();
        for (int i = 0; i < types.Length; i++)
        {
            if (type.IsAssignableFrom(types[i]) && !types[i].IsAbstract)
            {
                object obj = Activator.CreateInstance(types[i]);
                Int64 num = (Int64)propertyInfo.GetValue(obj, null);
                //Debug.LogError(types[i].Name);
                if (ComponentTypeDic.ContainsKey(num))
                {
                    Debug.LogError("Id : " + num + ",的组件已经存在！！！Type ：" + types[i].Name);
                }
                else
                {
                    ComponentTypeDic.Add(num, types[i]);
                }
            }
        }
    }

    #endregion
}
