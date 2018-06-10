using System.Collections.Generic;
using UnityEngine;

public class Trigger2DSystem : IReactiveSystem
{
    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.TRIGGER2D, ComponentIds.GAME_OBJECT);
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
                entity.SetValue(Trigger2DComponentVariable.isPhysical, true);
            }
            if (isRemove)
            {
                triggerList.RemoveAll(a => a.TriggerState == TriggerEnum.None);
            }
        }
    }
}

