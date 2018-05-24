using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExecuteSystem : ISystem
{
    ComponentFlag ExecuteCondition { get; }
    void Execute();
}
