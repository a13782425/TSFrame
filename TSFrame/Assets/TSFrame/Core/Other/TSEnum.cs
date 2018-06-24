using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSFrame.ECS
{
    public enum LifeCycleEnum
    {
        /// <summary>
        /// 默认跟随场景
        /// </summary>
        None = 0,
        /// <summary>
        /// 立刻销毁
        /// </summary>
        Destory = 1,
        /// <summary>
        /// 延时销毁
        /// </summary>
        DelayDestory = 2,
        /// <summary>
        /// 从不销毁
        /// </summary>
        DontDestory = 3,
        ///// <summary>
        ///// 立即销毁
        ///// </summary>
        //Immediately = 4
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
}