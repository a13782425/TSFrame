using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TSFrame.ECS
{
    public class ViewSystem : IReactiveSystem
    {
        private ComponentFlag _reactiveCondition = null;

        public ComponentFlag ReactiveCondition
        {
            get
            {
                if (_reactiveCondition == null)
                {
                    _reactiveCondition = Observer.Instance.GetFlag(OperatorIds.VIEW);
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
                    _executeCondition = Observer.Instance.GetFlag(OperatorIds.VIEW, OperatorIds.GAME_OBJECT);
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
            foreach (var item in entitys)
            {
                string prefabName = item.GetValue<string>(ViewComponentVariable.prefabName);
                GameObject obj = Observer.Instance.ResourcesLoad(prefabName) as GameObject;
                if (obj != null)
                {
                    GameObject instant = GameObject.Instantiate<GameObject>(obj);
                    instant.transform.SetParent(item.GetValue<Transform>(ViewComponentVariable.parent));
                    instant.hideFlags = item.GetValue<HideFlags>(ViewComponentVariable.hideFlags);
                    item.SetValue(GameObjectComponentVariable.value, instant);
                }
                else
                {
                    Debug.LogError("实例化游戏物体失败！！！");
                }

            }
        }
    }
}
