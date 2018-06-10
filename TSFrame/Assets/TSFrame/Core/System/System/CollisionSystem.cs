﻿using System.Collections.Generic;
using UnityEngine;


public class CollisionSystem : IReactiveSystem
{
    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.COLLISION, ComponentIds.GAME_OBJECT);
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
            List<CollisionModel> collisionList = entity.GetValue<List<CollisionModel>>(CollisionComponentVariable.collisionList);
            CollisionCallBack enter = entity.GetValue<CollisionCallBack>(CollisionComponentVariable.enterCallBack);
            CollisionCallBack stay = entity.GetValue<CollisionCallBack>(CollisionComponentVariable.stayCallBack);
            CollisionCallBack exit = entity.GetValue<CollisionCallBack>(CollisionComponentVariable.exitCallBack);

            int count = collisionList.Count;
            bool isChange = false;
            bool isRemove = false;
            for (int i = 0; i < count; i++)
            {
                CollisionModel item = collisionList[i];
                switch (item.CollisionState)
                {
                    case CollisionEnum.Enter:
                        if (enter != null)
                        {
                            enter.Invoke(entity, item.CurrentCollision);
                        }
                        if (stay != null)
                        {
                            item.CollisionState = CollisionEnum.Stay;
                            isChange = true;
                        }
                        break;
                    case CollisionEnum.Stay:
                        if (stay != null)
                        {
                            stay.Invoke(entity, item.CurrentCollision);
                            isChange = true;
                        }
                        break;
                    case CollisionEnum.Exit:
                        if (exit != null)
                        {
                            exit.Invoke(entity, item.CurrentCollision);
                            item.CollisionState = CollisionEnum.None;
                            isRemove = true;
                        }
                        break;
                    case CollisionEnum.None:
                    default:
                        break;
                }
            }
            if (isChange)
            {
                entity.SetValue(CollisionComponentVariable.isPhysical, true);
            }
            if (isRemove)
            {
                collisionList.RemoveAll(a => a.CollisionState == CollisionEnum.None);
            }
        }
    }
}
