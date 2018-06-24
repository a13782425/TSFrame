using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TSFrame.ECS
{
    public class GameObjectActiveSystem : IReactiveSystem
    {
        private ComponentFlag _reactiveCondition = null;

        public ComponentFlag ReactiveCondition
        {
            get
            {
                if (_reactiveCondition == null)
                {
                    _reactiveCondition = Observer.Instance.GetFlag(OperatorIds.ACTIVE);
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
                    _executeCondition = Observer.Instance.GetFlag(OperatorIds.ACTIVE, OperatorIds.GAME_OBJECT);
                }
                return _executeCondition;
            }
        }
        public ComponentFlag ReactiveIgnoreCondition
        {
            get
            {
                return ComponentFlag.None;
            }
        }
        public void Execute(List<Entity> entitys)
        {
            foreach (Entity item in entitys)
            {
                bool isActive = item.GetValue<bool>(ActiveComponentVariable.active);
                GameObject obj = item.GetValue<GameObject>(GameObjectComponentVariable.value);
                if (obj != null)
                {
                    obj.SetActive(isActive);
                }
                else
                {
                    item.SetValue(ActiveComponentVariable.active, isActive);
                }
            }
        }
    }
}

