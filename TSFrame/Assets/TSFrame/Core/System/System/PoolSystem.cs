using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PoolSystem : IReactiveSystem
{
    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.POOL);
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

