using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSFrame.ECS
{
    public class PoolSystem : IReactiveSystem
    {
        private ComponentFlag _condition = null;
        public ComponentFlag ReactiveCondition
        {
            get
            {
                if (_condition == null)
                {
                    _condition = Observer.Instance.GetFlag(OperatorIds.POOL);
                }
                return _condition;
            }
        }
        public ComponentFlag ExecuteCondition
        {
            get
            {
                if (_condition == null)
                {
                    _condition = Observer.Instance.GetFlag(OperatorIds.POOL);
                }
                return _condition;
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
}

