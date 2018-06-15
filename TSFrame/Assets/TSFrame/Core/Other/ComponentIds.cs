﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class ComponentIds
{
    #region 标签位

    /// <summary>
    /// 用户低位标签
    /// </summary>
    public const Int64 PLAYER_LOW_FLAG = 0L;
    /// <summary>
    /// 用户高位标签
    /// </summary>
    public const Int64 PLAYER_HIGH_FLAG = 1L << 63;
    /// <summary>
    /// 系统低位标签
    /// </summary>
    public const Int64 SYSTEM_LOW_FLAG = 1L << 62;
    /// <summary>
    /// 系统高位标签
    /// </summary>
    public const Int64 SYSTEM_HIGH_FLAG = 3L << 62;

    #endregion


    /// <summary>
    /// 字符串
    /// </summary>
    public const Int64 STRING = (1L << 0) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 在某个系统执行完毕添加新的组件
    /// </summary>
    public const Int64 ADDITIVE = (1L << 1) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 游戏物体
    /// </summary>
    public const Int64 GAME_OBJECT = (1L << 2) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 实例化物体
    /// </summary>
    public const Int64 VIEW = (1L << 3) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 关联场景物体
    /// </summary>
    public const Int64 LINK = (1L << 4) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 输入
    /// </summary>
    public const Int64 INPUT = (1L << 5) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 游戏物品的名字
    /// </summary>
    public const Int64 GAME_OBJECT_NAME = (1L << 6) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 生命周期
    /// </summary>
    public const Int64 LIFE_CYCLE = (1L << 7) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 碰撞组件
    /// </summary>
    public const Int64 COLLISION = (1L << 8) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 触发器
    /// </summary>
    public const Int64 TRIGGER = (1L << 9) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 2D碰撞组件
    /// </summary>
    public const Int64 COLLISION2D = (1L << 10) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 2D触发器
    /// </summary>
    public const Int64 TRIGGER2D = (1L << 11) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 是否存在物理
    /// </summary>
    public const Int64 HAS_PHYSICAL = (1L << 12) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 是否可视
    /// </summary>
    public const Int64 ACTIVE = (1L << 13) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 对象池
    /// </summary>
    public const Int64 POOL = (1L << 14) | SYSTEM_LOW_FLAG;
    /// <summary>
    /// 测试
    /// </summary>
    public const Int64 TEST = (1L << 0) | SYSTEM_HIGH_FLAG;

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
