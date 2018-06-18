using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameObjectLifeCycleSystem : IReactiveSystem
{
    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.LIFE_CYCLE, ComponentIds.GAME_OBJECT);
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
