using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSFrame.ECS
{
    public class SharedComponent
    {
        private int _sharedId;
        /// <summary>
        /// 组Id
        /// </summary>
        public int SharedId { get { return _sharedId; } }

        private NormalComponent _currentComponent = null;
        /// <summary>
        /// 当前组件
        /// </summary>
        public NormalComponent CurrentComponent { get { return _currentComponent; } }

        private Int64 _operatorId = 0L;
        /// <summary>
        /// 组件Id
        /// </summary>
        public Int64 OperatorId { get { return _operatorId; } }
        private int _componentId = 0;
        /// <summary>
        /// 组件ID
        /// </summary>
        public int ComponentId { get { return _componentId; } }

        private HashSet<Entity> _sharedEntityHashSet;

        public HashSet<Entity> SharedEntityHashSet { get { return _sharedEntityHashSet; } }

        public int ReferenceCount { get { return SharedEntityHashSet.Count; } }


        public SharedComponent(NormalComponent com, int shardId)
        {
            if (com == null)
            {
                throw new Exception("组件实体为空");
            }
            _currentComponent = com;
            _sharedId = shardId;
            _currentComponent.SharedId = shardId;
            _operatorId = _currentComponent.OperatorId;
            _componentId = _currentComponent.ComponentId;
            _sharedEntityHashSet = new HashSet<Entity>();
        }

        public override int GetHashCode()
        {
            return this.SharedId;
        }
    }
}