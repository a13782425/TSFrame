using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TSFrame.ECS
{
    public class GameObjectLifeCycleSystem : IReactiveSystem
    {
        private ComponentFlag _reactiveCondition = null;

        public ComponentFlag ReactiveCondition
        {
            get
            {
                if (_reactiveCondition == null)
                {
                    _reactiveCondition = Observer.Instance.GetFlag(OperatorIds.LIFE_CYCLE);
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
                    _executeCondition = Observer.Instance.GetFlag(OperatorIds.LIFE_CYCLE, OperatorIds.GAME_OBJECT);
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
                GameObject obj = item.GetValue<GameObject>(GameObjectComponentVariable.value);
                LifeCycleEnum lifeEnum = item.GetValue<LifeCycleEnum>(LifeCycleComponentVariable.lifeCycle);
                switch (lifeEnum)
                {
                    case LifeCycleEnum.Destory:
                        GameObject.Destroy(obj);
                        break;
                    case LifeCycleEnum.DelayDestory:
                        float delayTime = item.GetValue<float>(LifeCycleComponentVariable.dealyTime);
                        GameObject.Destroy(obj, delayTime);
                        break;
                    case LifeCycleEnum.DontDestory:
                        GameObject.DontDestroyOnLoad(obj);
                        break;
                    case LifeCycleEnum.None:
                    default:
                        break;
                }
            }
        }
    }
}