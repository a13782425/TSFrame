using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReactiveSystem : ISystem
{
    /// <summary>
    /// 触发条件
    /// </summary>
    ComponentFlag ReactiveCondition { get; }
    void Execute(List<Entity> entitys);
}
