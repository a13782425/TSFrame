using System.Collections.Generic;
using UnityEngine;

public class Trigger2DSystem : IReactiveSystem
{
    private ComponentFlag _reactiveCondition = null;

    public ComponentFlag ReactiveCondition
    {
        get
        {
            if (_reactiveCondition == null)
            {
                _reactiveCondition = Observer.Instance.GetFlag(OperatorIds.TRIGGER2D, OperatorIds.GAME_OBJECT);
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
        foreach (Entity entity in entitys)
        {
            GameObject obj = entity.GetValue<GameObject>(GameObjectComponentVariable.value);
            if (obj == null)
            {
                continue;
            }
            List<Trigger2DModel> triggerList = entity.GetValue<List<Trigger2DModel>>(Trigger2DComponentVariable.triggerList);
            Trigger2DCallBack enter = entity.GetValue<Trigger2DCallBack>(Trigger2DComponentVariable.enterCallBack);
            Trigger2DCallBack stay = entity.GetValue<Trigger2DCallBack>(Trigger2DComponentVariable.stayCallBack);
            Trigger2DCallBack exit = entity.GetValue<Trigger2DCallBack>(Trigger2DComponentVariable.exitCallBack);

            int count = triggerList.Count;
            bool isChange = false;
            bool isRemove = false;
            for (int i = 0; i < count; i++)
            {
                Trigger2DModel item = triggerList[i];
                switch (item.TriggerState)
                {
                    case TriggerEnum.Enter:
                        if (enter != null)
                        {
                            Invoke(enter, entity, item.CurrentCollider);
                        }
                        if (stay != null)
                        {
                            item.TriggerState = TriggerEnum.Stay;
                            isChange = true;
                        }
                        if (item.TriggerState == TriggerEnum.Enter)
                        {
                            item.TriggerState = TriggerEnum.None;
                            isRemove = true;
                        }
                        break;
                    case TriggerEnum.Stay:
                        if (stay != null)
                        {
                            Invoke(stay, entity, item.CurrentCollider);
                            isChange = true;
                        }
                        break;
                    case TriggerEnum.Exit:
                        if (exit != null)
                        {
                            Invoke(exit, entity, item.CurrentCollider);
                            item.TriggerState = TriggerEnum.None;
                            isRemove = true;
                        }
                        break;
                    case TriggerEnum.None:
                    default:
                        break;
                }
            }
            if (isChange)
            {
                entity.SetValue(Trigger2DComponentVariable.isPhysical, true);
            }
            if (isRemove)
            {
                triggerList.RemoveAll(a => a.TriggerState == TriggerEnum.None);
            }
        }
    }
    private void Invoke(Trigger2DCallBack callback, Entity entity, Collider2D collider)
    {
        callback.Invoke(entity, collider);
        if (entity.Parent != null)
        {
            Invoke(callback, entity.Parent, collider);
        }
        return;
    }
}

