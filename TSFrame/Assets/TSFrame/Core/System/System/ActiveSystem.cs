using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TSFrame.ECS
{
    public class ActiveSystem : IReactiveSystem
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

        public ComponentFlag ExecuteCondition
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
                //if (item.GetComponentFlag().HasFlag(ComponentIds.GAME_OBJECT))
                //{
                //    GameObject obj = item.GetValue<GameObject>(GameObjectComponentVariable.value);
                //    if (obj != null)
                //    {
                //        obj.SetActive(isActive);
                //    }
                //}
                Observer.Instance.SetActive(item, isActive);
            }
        }
    }
}