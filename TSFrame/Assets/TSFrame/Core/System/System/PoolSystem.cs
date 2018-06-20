using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PoolSystem : IReactiveSystem
{
    private ComponentFlag _reactiveCondition = null;

    public ComponentFlag ReactiveCondition
    {
        get
        {
            if (_reactiveCondition == null)
            {
                _reactiveCondition = Observer.Instance.GetFlag(OperatorIds.POOL);
            }
            return _reactiveCondition;
        }
    }

    public ComponentFlag ReactiveIgnoreCondition { get { return ComponentFlag.None; } }

    public void Execute(List<Entity> entitys)
    {
        foreach (Entity entity in entitys)
        {
            if (entity.GetValue<bool>(PoolComponentVariable.recover))
            {
                Observer.Instance.RecoverEntity(entity, entity.GetValue<string>(PoolComponentVariable.poolName));
            }
        }
    }
}

