using System.Collections.Generic;
using UnityEngine;


public class CollisionSystem : IReactiveSystem
{
    private ComponentFlag _reactiveCondition = null;

    public ComponentFlag ReactiveCondition
    {
        get
        {
            if (_reactiveCondition == null)
            {
                _reactiveCondition = Observer.Instance.GetFlag(OperatorIds.COLLISION, OperatorIds.GAME_OBJECT);
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
                            Invoke(enter, entity, item.CurrentCollision);
                        }
                        if (stay != null)
                        {
                            item.CollisionState = CollisionEnum.Stay;
                            isChange = true;
                        }
                        if (item.CollisionState == CollisionEnum.Enter)
                        {
                            item.CollisionState = CollisionEnum.None;
                            isRemove = true;
                        }
                        break;
                    case CollisionEnum.Stay:
                        if (stay != null)
                        {
                            Invoke(stay, entity, item.CurrentCollision);
                            isChange = true;
                        }
                        break;
                    case CollisionEnum.Exit:
                        if (exit != null)
                        {
                            Invoke(exit, entity, item.CurrentCollision);
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


    private void Invoke(CollisionCallBack callback, Entity entity, Collision collision)
    {
        callback.Invoke(entity, collision);
        if (entity.Parent != null)
        {
            Invoke(callback, entity.Parent, collision);
        }
        return;
    }
}

