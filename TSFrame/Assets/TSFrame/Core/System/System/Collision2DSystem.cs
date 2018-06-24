using System.Collections.Generic;
using UnityEngine;


namespace TSFrame.ECS
{
    public class Collision2DSystem : IReactiveSystem
    {
        private ComponentFlag _reactiveCondition = null;
        public ComponentFlag ReactiveCondition
        {
            get
            {
                if (_reactiveCondition == null)
                {
                    _reactiveCondition = Observer.Instance.GetFlag(OperatorIds.COLLISION2D);
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
                    _executeCondition = Observer.Instance.GetFlag(OperatorIds.COLLISION2D, OperatorIds.GAME_OBJECT);
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
                    entity.SetValue(Collision2DComponentVariable.isPhysical, true);
                }
                if (isRemove)
                {
                    collisionList.RemoveAll(a => a.CollisionState == CollisionEnum.None);
                }
            }
        }
        private void Invoke(Collision2DCallBack callback, Entity entity, Collision2D collision)
        {
            callback.Invoke(entity, collision);
            if (entity.Parent != null)
            {
                Invoke(callback, entity.Parent, collision);
            }
            return;
        }
    }
}
