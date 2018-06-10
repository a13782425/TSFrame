using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum LifeCycleEnum
{
    /// <summary>
    /// 默认跟随场景
    /// </summary>
    None = 0,
    /// <summary>
    /// 从不销毁
    /// </summary>
    DontDestory = 1,
    /// <summary>
    /// 立即销毁
    /// </summary>
    Immediately = 2
}

public enum CollisionEnum
{
    /// <summary>
    /// 无状态
    /// </summary>
    None = 0,
    /// <summary>
    /// 进入
    /// </summary>
    Enter,
    /// <summary>
    /// 常住
    /// </summary>
    Stay,
    /// <summary>
    /// 退出
    /// </summary>
    Exit
}
public enum TriggerEnum
{
    /// <summary>
    /// 无状态
    /// </summary>
    None = 0,
    /// <summary>
    /// 进入
    /// </summary>
    Enter,
    /// <summary>
    /// 常住
    /// </summary>
    Stay,
    /// <summary>
    /// 退出
    /// </summary>
    Exit
}