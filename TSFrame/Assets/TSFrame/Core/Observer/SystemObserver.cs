using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace TSFrame.ECS
{
    public sealed partial class Observer
    {
        public Observer AddSystem(ISystem system)
        {
            if (system != null)
            {
                if (system is IInitSystem)
                {
                    _systemInitList.Add(system);
                    (system as IInitSystem).Init();
                }
                if (system is IReactiveSystem)
                {
                    _systemReactiveDic.Add(new ReactiveSystemDto(system as IReactiveSystem));
                }
                if (system is IExecuteSystem)
                {
                    _systemExecuteList.Add(system);
                }
            }
            return this;
        }

        /// <summary>
        /// 删除系统
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Observer RemoveSystem<T>()
        {
            IInitSystem initSystem = null;
            foreach (IInitSystem item in _systemInitList)
            {
                if (item.GetType() == typeof(T))
                {
                    initSystem = item;
                    break;
                }
            }
            if (initSystem != null)
                _systemInitList.Remove(initSystem);
            //ISystem reactiveSystem = null;
            int index = -1;
            for (int i = 0; i < _systemReactiveDic.Count; i++)
            {
                if (_systemReactiveDic[i].CurrentSystem.GetType() == typeof(T))
                {
                    index = i;
                    break;
                }
            }
            if (index > 0)
            {
                _systemReactiveDic.RemoveAt(index);
            }
            IExecuteSystem executeSystem = null;
            foreach (IExecuteSystem item in _systemExecuteList)
            {
                if (item.GetType() == typeof(T))
                {
                    executeSystem = item;
                    break;
                }
            }
            if (executeSystem != null)
                _systemExecuteList.Remove(executeSystem);
            return this;
        }


        partial void SystemLoad()
        {
            _systemGameObject = new GameObject("SystemGameObject");
            _systemGameObject.transform.SetParent(this.transform);
            AddSystem(new ActiveSystem());
            AddSystem(new ViewSystem());
            AddSystem(new PoolSystem());
            AddSystem(new HasPhysicalSystem());
            AddSystem(new Collision2DSystem());
            AddSystem(new CollisionSystem());
            AddSystem(new TriggerSystem());
            AddSystem(new Trigger2DSystem());
            AddSystem(new PositionSystem());
            AddSystem(new RoationSystem());
            AddSystem(new GameObjectActiveSystem());
            AddSystem(new GameObjectLifeCycleSystem());
            AddSystem(new LifeCycleSystem());
        }
        partial void ReactiveSysyemRun()
        {
            foreach (ReactiveSystemDto item in _systemReactiveDic)
            {
                if (item.EntityHashSet.Count > 0)
                {
                    List<Entity> temp = new List<Entity>();
                    temp.AddRange(item.EntityHashSet);
                    item.EntityHashSet.Clear();
                    item.CurrentSystem.Execute(temp);
                }
            }
        }
        partial void SystemUpdate()
        {
            if (_systemExecuteList.Count > 0)
            {
                foreach (IExecuteSystem item in _systemExecuteList)
                {
                    item.Execute();
                }
            }
            if (IsUseThread)
            {
                if (System.Threading.Interlocked.CompareExchange(ref TSThread.Instance.OperatorLock, 1, 0) == 0)
                {
                    ReactiveSysyemRun();
                    System.Threading.Interlocked.Exchange(ref TSThread.Instance.OperatorLock, 0);
                }
            }
            else
            {
                ReactiveSysyemRun();
            }
        }
    }
}

