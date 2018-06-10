using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TriggerSystem : IReactiveSystem
{
    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.TRIGGER, ComponentIds.GAME_OBJECT);
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
            List<TriggerModel> triggerList = entity.GetValue<List<TriggerModel>>(TriggerComponentVariable.triggerList);
            TriggerCallBack enter = entity.GetValue<TriggerCallBack>(TriggerComponentVariable.enterCallBack);
            TriggerCallBack stay = entity.GetValue<TriggerCallBack>(TriggerComponentVariable.stayCallBack);
            TriggerCallBack exit = entity.GetValue<TriggerCallBack>(TriggerComponentVariable.exitCallBack);

            int count = triggerList.Count;
            bool isChange = false;
            bool isRemove = false;
            for (int i = 0; i < count; i++)
            {
                TriggerModel item = triggerList[i];
                switch (item.TriggerState)
                {
                    case TriggerEnum.Enter:
                        if (enter != null)
                        {
                            enter.Invoke(entity, item.CurrentCollider);
                        }
                        if (stay != null)
                        {
                            item.TriggerState = TriggerEnum.Stay;
                            isChange = true;
                        }
                        break;
                    case TriggerEnum.Stay:
                        if (stay != null)
                        {
                            stay.Invoke(entity, item.CurrentCollider);
                            isChange = true;
                        }
                        break;
                    case TriggerEnum.Exit:
                        if (exit != null)
                        {
                            exit.Invoke(entity, item.CurrentCollider);
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
                entity.SetValue(TriggerComponentVariable.isPhysical, true);
            }
            if (isRemove)
            {
                triggerList.RemoveAll(a => a.TriggerState == TriggerEnum.None);
            }
        }
    }
}

