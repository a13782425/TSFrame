using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSFrame.ECS
{
    public class ValueModel
    {
        public ValueModel(int id)
        {
            _id = id;
        }
        private int _id;
        public int Id { get { return _id; } }
        public Entity CurrentEntity;
        public NormalComponent CurrentComponent;
        public int CurrentPropertyId;
        public object CurrentValue;
    }
}
