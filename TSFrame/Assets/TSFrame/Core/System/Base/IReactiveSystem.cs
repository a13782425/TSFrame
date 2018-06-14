using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReactiveSystem : ISystem
{
    /// <summary>
    /// 触发条件
    /// </summary>
    ComponentFlag ReactiveCondition { get; }
    /// <summary>
    /// 触发忽略条件
    /// </summary>
    ComponentFlag ReactiveIgnoreCondition { get; }

    void Execute(List<Entity> entitys);
}
