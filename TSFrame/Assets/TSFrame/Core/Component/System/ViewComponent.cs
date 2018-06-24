using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TSFrame.ECS
{
    public class ViewComponent : IComponent, IReactiveComponent
    {
        public Int64 CurrentId
        {
            get
            {
                return OperatorIds.VIEW;
            }
        }
        [DataDriven]
        private string prefabName;
        private Transform parent;
        private Vector3 pos;
        private Quaternion rot;
        private HideFlags hideFlags;
    }
}
