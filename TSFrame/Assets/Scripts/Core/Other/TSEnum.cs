using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum LifeCycleEnum
{
    /// <summary>
    /// 默认跟随场景
    /// </summary>
    None = 1,
    /// <summary>
    /// 从不销毁
    /// </summary>
    DontDestory = 2,
    /// <summary>
    /// 立即销毁
    /// </summary>
    Immediately = 4
}
