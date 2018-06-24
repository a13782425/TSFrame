using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TSFrame.ECS
{
    public class PositionSystem : IReactiveSystem
    {
        private ComponentFlag _reactiveCondition = null;

        public ComponentFlag ReactiveCondition
        {
            get
            {
                if (_reactiveCondition == null)
                {
                    _reactiveCondition = Observer.Instance.GetFlag(OperatorIds.POSITION);
                }
                return _reactiveCondition;
            }
        }

        private ComponentFlag _executeCondition = null;

        public ComponentFlag ExecuteCondition
        {
            get
            {
                if (_executeCondition == null)
                {
                    _executeCondition = Observer.Instance.GetFlag(OperatorIds.POSITION, OperatorIds.GAME_OBJECT);
                }
                return _executeCondition;
            }
        }

        public ComponentFlag ReactiveIgnoreCondition { get { return ComponentFlag.None; } }

        public void Execute(List<Entity> entitys)
        {
            foreach (Entity item in entitys)
            {
                GameObject obj = item.GetValue<GameObject>(GameObjectComponentVariable.value);
                if (obj != null)
                {
                    obj.transform.position = item.GetValue<Vector3>(PositionComponentVariable.position);
                }
                else
                {
                    item.SetValue(PositionComponentVariable.position, item.GetValue<Vector3>(PositionComponentVariable.position));
                }
            }
        }
    }
}

