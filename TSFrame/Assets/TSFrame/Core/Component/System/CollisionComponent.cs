using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TSFrame.ECS
{
    public class CollisionComponent : IComponent, IReactiveComponent
    {
        public Int64 OperatorId
        {
            get
            {
                return OperatorIds.COLLISION;
            }
        }

        [DataDriven]
        private bool isPhysical;

        private List<CollisionModel> collisionList;

        private CollisionCallBack enterCallBack;

        private CollisionCallBack stayCallBack;

        private CollisionCallBack exitCallBack;

    }
}
