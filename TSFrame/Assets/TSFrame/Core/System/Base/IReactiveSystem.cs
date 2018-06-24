using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSFrame.ECS
{
    public interface IReactiveSystem : ISystem
    {
        /// <summary>
        /// 触发条件
        /// </summary>
        ComponentFlag ReactiveCondition { get; }
        /// <summary>
        /// 执行条件
        /// </summary>
        ComponentFlag ExecuteCondition { get; }
        /// <summary>
        /// 触发忽略条件
        /// </summary>
        ComponentFlag ReactiveIgnoreCondition { get; }

        void Execute(List<Entity> entitys);
    }
}