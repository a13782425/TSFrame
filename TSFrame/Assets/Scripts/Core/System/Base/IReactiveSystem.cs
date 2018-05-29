using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReactiveSystem : ISystem
{
    ComponentFlag ReactiveCondition { get; }
    void Execute(List<Entity> entitys);
}
