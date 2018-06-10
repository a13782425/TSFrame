using System.Collections.Generic;
using UnityEngine;

public class Collision2DSystem : IReactiveSystem
{
    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.COLLISION2D, ComponentIds.GAME_OBJECT);
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
            List<Collision2DModel> collisionList = entity.GetValue<List<Collision2DModel>>(Collision2DComponentVariable.collisionList);
            Collision2DCallBack enter = entity.GetValue<Collision2DCallBack>(Collision2DComponentVariable.enterCallBack);
            Collision2DCallBack stay = entity.GetValue<Collision2DCallBack>(Collision2DComponentVariable.stayCallBack);
            Collision2DCallBack exit = entity.GetValue<Collision2DCallBack>(Collision2DComponentVariable.exitCallBack);

            int count = collisionList.Count;
            bool isChange = false;
            bool isRemove = false;
            for (int i = 0; i < count; i++)
            {
                Collision2DModel item = collisionList[i];
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
                entity.SetValue(Collision2DComponentVariable.isPhysical, true);
            }
            if (isRemove)
            {
                collisionList.RemoveAll(a => a.CollisionState == CollisionEnum.None);
            }
        }
    }
}

