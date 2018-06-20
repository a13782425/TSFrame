using System.Collections.Generic;
using UnityEngine;

public class HasPhysicalSystem : IReactiveSystem
{
    private ComponentFlag _reactiveCondition = null;


    public ComponentFlag ReactiveCondition
    {
        get
        {
            if (_reactiveCondition == null)
            {
                _reactiveCondition = Observer.Instance.GetFlag(OperatorIds.HAS_PHYSICAL, OperatorIds.GAME_OBJECT);
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
            bool isHas = item.GetValue<bool>(HasPhysicalComponentVariable.isHas);
            GameObject obj = item.GetValue<GameObject>(GameObjectComponentVariable.value);
            if (obj == null)
            {
                item.SetValue(HasPhysicalComponentVariable.isHas, isHas);
                continue;
            }
            if (item.GetComponentFlag().HasFlag(OperatorIds.COLLISION))
            {
                EntityCollisionMono mono = obj.GetComponent<EntityCollisionMono>();
                if (isHas)
                {
                    if (mono == null)
                    {
                        mono = obj.AddComponent<EntityCollisionMono>();
                        mono.Init(item);
                    }
                }
                else
                {
                    if (mono != null)
                    {
                        GameObject.Destroy(mono);
                    }
                }
            }
            if (item.GetComponentFlag().HasFlag(OperatorIds.COLLISION2D))
            {
                EntityCollision2DMono mono = obj.GetComponent<EntityCollision2DMono>();
                if (isHas)
                {
                    if (mono == null)
                    {
                        mono = obj.AddComponent<EntityCollision2DMono>();
                        mono.Init(item);
                    }
                }
                else
                {
                    if (mono != null)
                    {
                        GameObject.Destroy(mono);
                    }
                }
            }
            if (item.GetComponentFlag().HasFlag(OperatorIds.TRIGGER))
            {
                EntityTriggerMono mono = obj.GetComponent<EntityTriggerMono>();
                if (isHas)
                {
                    if (mono == null)
                    {
                        mono = obj.AddComponent<EntityTriggerMono>();
                        mono.Init(item);
                    }
                }
                else
                {
                    if (mono != null)
                    {
                        GameObject.Destroy(mono);
                    }
                }
            }
            if (item.GetComponentFlag().HasFlag(OperatorIds.TRIGGER2D))
            {
                EntityTrigger2DMono mono = obj.GetComponent<EntityTrigger2DMono>();
                if (isHas)
                {
                    if (mono == null)
                    {
                        mono = obj.AddComponent<EntityTrigger2DMono>();
                        mono.Init(item);
                    }
                }
                else
                {
                    if (mono != null)
                    {
                        GameObject.Destroy(mono);
                    }
                }
            }
        }
    }
}

