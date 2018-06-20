using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 实体mono基类
/// </summary>
public class EntityMono : MonoBehaviour
{
    private Entity _currentEntity = null;
    /// <summary>
    /// 当前的实体
    /// </summary>
    public Entity CurrentEntity { get { return _currentEntity; } }

    public virtual void Init(Entity entity)
    {
        this._currentEntity = entity;
    }
}

/// <summary>
/// 3D碰撞类
/// </summary>
public class EntityCollisionMono : EntityMono
{
    private List<CollisionModel> collisionList;

    public override void Init(Entity entity)
    {
        base.Init(entity);
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.COLLISION) && this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.GAME_OBJECT))
        {
            collisionList = new List<CollisionModel>();
            this.CurrentEntity.SetValue(CollisionComponentVariable.collisionList, collisionList);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.CurrentEntity == null)
        {
            Destroy(this);
        }
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.COLLISION))
        {
            if (this.CurrentEntity.GetValue<CollisionCallBack>(CollisionComponentVariable.enterCallBack) == null)
            {
                return;
            }
            int count = collisionList.Count;
            for (int i = 0; i < count; i++)
            {
                CollisionModel item = collisionList[i];
                if (item.CurrentCollision.gameObject == collision.gameObject)
                {
                    return;
                }
            }
            collisionList.Add(new CollisionModel() { CollisionState = CollisionEnum.Enter, CurrentCollision = collision });
            this.CurrentEntity.SetValue(CollisionComponentVariable.isPhysical, true);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (this.CurrentEntity == null)
        {
            Destroy(this);
        }
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.COLLISION))
        {
            if (this.CurrentEntity.GetValue<CollisionCallBack>(CollisionComponentVariable.exitCallBack) == null)
            {
                return;
            }
            int count = collisionList.Count;
            for (int i = 0; i < count; i++)
            {
                CollisionModel item = collisionList[i];
                if (item.CurrentCollision.gameObject == collision.gameObject)
                {
                    item.CollisionState = CollisionEnum.Exit;
                    this.CurrentEntity.SetValue(CollisionComponentVariable.isPhysical, true);
                }
            }
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.COLLISION))
        {
            this.CurrentEntity.SetValue(CollisionComponentVariable.collisionList, new List<CollisionModel>());
        }
    }
}

/// <summary>
/// 2D碰撞类
/// </summary>
public class EntityCollision2DMono : EntityMono
{
    private List<Collision2DModel> collisionList;

    public override void Init(Entity entity)
    {
        base.Init(entity);
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.COLLISION2D) && this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.GAME_OBJECT))
        {
            collisionList = new List<Collision2DModel>();
            this.CurrentEntity.SetValue(Collision2DComponentVariable.collisionList, collisionList);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.CurrentEntity == null)
        {
            Destroy(this);
        }
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.COLLISION2D))
        {
            if (this.CurrentEntity.GetValue<Collision2DCallBack>(Collision2DComponentVariable.enterCallBack) == null)
            {
                return;
            }
            int count = collisionList.Count;
            for (int i = 0; i < count; i++)
            {
                Collision2DModel item = collisionList[i];
                if (item.CurrentCollision.gameObject == collision.gameObject)
                {
                    return;
                }
            }
            collisionList.Add(new Collision2DModel() { CollisionState = CollisionEnum.Enter, CurrentCollision = collision });
            this.CurrentEntity.SetValue(Collision2DComponentVariable.isPhysical, true);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (this.CurrentEntity == null)
        {
            Destroy(this);
        }
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.COLLISION2D))
        {
            if (this.CurrentEntity.GetValue<CollisionCallBack>(Collision2DComponentVariable.exitCallBack) == null)
            {
                return;
            }
            int count = collisionList.Count;
            for (int i = 0; i < count; i++)
            {
                Collision2DModel item = collisionList[i];
                if (item.CurrentCollision.gameObject == collision.gameObject)
                {
                    item.CollisionState = CollisionEnum.Exit;
                    this.CurrentEntity.SetValue(Collision2DComponentVariable.isPhysical, true);
                }
            }
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.COLLISION2D))
        {
            this.CurrentEntity.SetValue(Collision2DComponentVariable.collisionList, new List<Collision2DModel>());
        }
    }
}

/// <summary>
/// 3D触发类
/// </summary>
public class EntityTriggerMono : EntityMono
{
    private List<TriggerModel> triggerList;

    public override void Init(Entity entity)
    {
        base.Init(entity);
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.TRIGGER) && this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.GAME_OBJECT))
        {
            triggerList = new List<TriggerModel>();
            this.CurrentEntity.SetValue(TriggerComponentVariable.triggerList, triggerList);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (this.CurrentEntity == null)
        {
            Destroy(this);
        }
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.TRIGGER))
        {
            if (this.CurrentEntity.GetValue<TriggerCallBack>(TriggerComponentVariable.enterCallBack) == null)
            {
                return;
            }
            int count = triggerList.Count;
            for (int i = 0; i < count; i++)
            {
                TriggerModel item = triggerList[i];
                if (item.CurrentCollider.gameObject == collider.gameObject)
                {
                    return;
                }
            }
            triggerList.Add(new TriggerModel() { TriggerState = TriggerEnum.Enter, CurrentCollider = collider });
            this.CurrentEntity.SetValue(TriggerComponentVariable.isPhysical, true);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (this.CurrentEntity == null)
        {
            Destroy(this);
        }
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.TRIGGER))
        {
            if (this.CurrentEntity.GetValue<TriggerCallBack>(TriggerComponentVariable.exitCallBack) == null)
            {
                return;
            }
            int count = triggerList.Count;
            for (int i = 0; i < count; i++)
            {
                TriggerModel item = triggerList[i];
                if (item.CurrentCollider.gameObject == collider.gameObject)
                {
                    item.TriggerState = TriggerEnum.Exit;
                    this.CurrentEntity.SetValue(TriggerComponentVariable.isPhysical, true);
                }
            }
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.TRIGGER))
        {
            this.CurrentEntity.SetValue(TriggerComponentVariable.triggerList, new List<TriggerModel>());
        }
    }
}

/// <summary>
/// 2D触发类
/// </summary>
public class EntityTrigger2DMono : EntityMono
{
    private List<Trigger2DModel> triggerList;

    public override void Init(Entity entity)
    {
        base.Init(entity);
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.TRIGGER2D) && this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.GAME_OBJECT))
        {
            triggerList = new List<Trigger2DModel>();
            this.CurrentEntity.SetValue(Trigger2DComponentVariable.triggerList, triggerList);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.CurrentEntity == null)
        {
            Destroy(this);
        }
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.TRIGGER2D))
        {
            if (this.CurrentEntity.GetValue<Trigger2DCallBack>(Trigger2DComponentVariable.enterCallBack) == null)
            {
                return;
            }
            int count = triggerList.Count;
            for (int i = 0; i < count; i++)
            {
                Trigger2DModel item = triggerList[i];
                if (item.CurrentCollider.gameObject == collider.gameObject)
                {
                    return;
                }
            }
            triggerList.Add(new Trigger2DModel() { TriggerState = TriggerEnum.Enter, CurrentCollider = collider });
            this.CurrentEntity.SetValue(Trigger2DComponentVariable.isPhysical, true);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (this.CurrentEntity == null)
        {
            Destroy(this);
        }
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.TRIGGER2D))
        {
            if (this.CurrentEntity.GetValue<TriggerCallBack>(Trigger2DComponentVariable.exitCallBack) == null)
            {
                return;
            }
            int count = triggerList.Count;
            for (int i = 0; i < count; i++)
            {
                Trigger2DModel item = triggerList[i];
                if (item.CurrentCollider.gameObject == collider.gameObject)
                {
                    item.TriggerState = TriggerEnum.Exit;
                    this.CurrentEntity.SetValue(Trigger2DComponentVariable.isPhysical, true);
                }
            }
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (this.CurrentEntity.GetComponentFlag().HasFlag(OperatorIds.TRIGGER2D))
        {
            this.CurrentEntity.SetValue(Trigger2DComponentVariable.triggerList, new List<Trigger2DModel>());
        }
    }
}
