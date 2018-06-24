using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace TSFrame.ECS
{
    public sealed partial class Observer
    {

        #region Public
        public Group MatchGetGroup(ComponentFlag flag)
        {
            foreach (Group item in _matchGroupHashSet)
            {
                if (item.GetHashCode() == flag.GetHashCode())
                {
                    return item;
                }
            }

            Group group = new Group(flag);
            _matchGroupHashSet.Add(group);
            return group;
        }
        public Group MatchGetGroup(params Int64[] componentIds)
        {
            if (componentIds == null)
            {
                throw new Exception("ComponentIds is null");
            }
            ComponentFlag flag = new ComponentFlag();
            for (int i = 0; i < componentIds.Length; i++)
            {
                if (flag.HasFlag(componentIds[i]))
                {
                    continue;
                }
                flag.SetFlag(componentIds[i]);
            }
            return MatchGetGroup(flag);
        }

        public ComponentFlag GetFlag(params Int64[] args)
        {
            ComponentFlag flag = new ComponentFlag();
            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (!flag.HasFlag(args[i]))
                    {
                        flag.SetFlag(args[i]);
                    }
                }
            }
            return flag;
        }

        [Obsolete("外界不要调用")]
        public void DataDrivenMethod(Entity entity, NormalComponent com)
        {
            if (entity == null)
            {
                return;
            }
            int sharedId = com.SharedId;
            SharedComponent sharedComponent = null;
            if (sharedId > 0)
            {
                sharedComponent = _sharedComponentDic[sharedId];
            }
            int count = _systemReactiveDic.Count;
            for (int i = 0; i < count; i++)
            {
                ReactiveSystemDto dto = _systemReactiveDic[i];
                ComponentFlag executeCondition = dto.CurrentSystem.ExecuteCondition;
                ComponentFlag reactiveCondition = dto.CurrentSystem.ExecuteCondition;
                ComponentFlag reactiveIgnoreCondition = dto.CurrentSystem.ReactiveIgnoreCondition;
                if (!reactiveCondition.HasFlag(com.OperatorId))
                {
                    continue;
                }
                if (sharedId > 0)
                {
                    foreach (Entity setEntity in sharedComponent.SharedEntityHashSet)
                    {
                        if (setEntity.GetComponentFlag().HasFlag(reactiveIgnoreCondition))
                        {
                            continue;
                        }
                        if (setEntity.GetComponentFlag().HasFlag(executeCondition))
                        {
                            dto.EntityHashSet.Add(setEntity);
                        }
                    }
                }
                else
                {
                    if (entity.GetComponentFlag().HasFlag(reactiveIgnoreCondition))
                    {
                        continue;
                    }
                    if (entity.GetComponentFlag().HasFlag(executeCondition))
                    {
                        dto.EntityHashSet.Add(entity);
                    }
                }
            }
        }
        #endregion


        partial void MatchLoad()
        {
            _matchGameObject = new GameObject("MatchGameObject");
            _matchGameObject.transform.SetParent(this.transform);
        }

        partial void MatchUpdate()
        {

        }

        partial void MatchEntity(Entity entity, bool isActive)
        {
            foreach (Group item in _matchGroupHashSet)
            {
                if (isActive)
                {
                    if (entity.GetComponentFlag().HasFlag(item.ComponentFlag))
                    {
                        item.AddEntity(entity);
                    }
                }
                else
                {
                    item.RemoveEntity(entity);
                }
            }
        }

        partial void MatchEntity(Entity entity, NormalComponent component)
        {
            int sharedId = component.SharedId;
            if (!entity.GetComponentFlag().HasFlag(component.OperatorId))
            {
                if (sharedId > 0)
                {
                    if (_sharedComponentDic.ContainsKey(sharedId))
                    {
                        _sharedComponentDic[sharedId].SharedEntityHashSet.Remove(entity);
                    }
                }
                foreach (Group item in _matchGroupHashSet)
                {
                    if (item.ComponentFlag.HasFlag(component.OperatorId))
                    {
                        item.RemoveEntity(entity);
                    }
                }
                RecoverComponent(component);
            }
            else
            {
                if (sharedId > 0)
                {
                    if (_sharedComponentDic.ContainsKey(sharedId))
                    {
                        _sharedComponentDic[sharedId].SharedEntityHashSet.Add(entity);
                    }
                }
                foreach (Group item in _matchGroupHashSet)
                {
                    if (entity.GetComponentFlag().HasFlag(item.ComponentFlag))
                    {
                        item.AddEntity(entity);
                    }
                }
            }
        }
    }
}

