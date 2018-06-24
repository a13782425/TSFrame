//------------------------------------------------------------------------------------------------------------
//-----------------------------------generate file 2018-06-24 21:52:53----------------------------------------
//------------------------------------------------------------------------------------------------------------

namespace TSFrame.ECS
{

    public class ActiveComponentVariable
    {
        /// <summary>
        /// Type : Boolean
        /// </summary>
        public static ComponentValue active = new ComponentValue() { ComponentId = 0, PropertyId = 0, OperatorId = 4611686018427388160, DontCopy = false, NeedReactive = true };
        public static int Count { get { return 1; } }
    }

    public class Collision2DComponentVariable
    {
        /// <summary>
        /// Type : Boolean
        /// </summary>
        public static ComponentValue isPhysical = new ComponentValue() { ComponentId = 1, PropertyId = 0, OperatorId = 4611686018427387936, DontCopy = false, NeedReactive = true };
        /// <summary>
        /// Type : List`1
        /// </summary>
        public static ComponentValue collisionList = new ComponentValue() { ComponentId = 1, PropertyId = 1, OperatorId = 4611686018427387936, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : Collision2DCallBack
        /// </summary>
        public static ComponentValue enterCallBack = new ComponentValue() { ComponentId = 1, PropertyId = 2, OperatorId = 4611686018427387936, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : Collision2DCallBack
        /// </summary>
        public static ComponentValue stayCallBack = new ComponentValue() { ComponentId = 1, PropertyId = 3, OperatorId = 4611686018427387936, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : Collision2DCallBack
        /// </summary>
        public static ComponentValue exitCallBack = new ComponentValue() { ComponentId = 1, PropertyId = 4, OperatorId = 4611686018427387936, DontCopy = false, NeedReactive = false };
        public static int Count { get { return 5; } }
    }

    public class CollisionComponentVariable
    {
        /// <summary>
        /// Type : Boolean
        /// </summary>
        public static ComponentValue isPhysical = new ComponentValue() { ComponentId = 2, PropertyId = 0, OperatorId = 4611686018427387912, DontCopy = false, NeedReactive = true };
        /// <summary>
        /// Type : List`1
        /// </summary>
        public static ComponentValue collisionList = new ComponentValue() { ComponentId = 2, PropertyId = 1, OperatorId = 4611686018427387912, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : CollisionCallBack
        /// </summary>
        public static ComponentValue enterCallBack = new ComponentValue() { ComponentId = 2, PropertyId = 2, OperatorId = 4611686018427387912, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : CollisionCallBack
        /// </summary>
        public static ComponentValue stayCallBack = new ComponentValue() { ComponentId = 2, PropertyId = 3, OperatorId = 4611686018427387912, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : CollisionCallBack
        /// </summary>
        public static ComponentValue exitCallBack = new ComponentValue() { ComponentId = 2, PropertyId = 4, OperatorId = 4611686018427387912, DontCopy = false, NeedReactive = false };
        public static int Count { get { return 5; } }
    }

    public class GameObjectComponentVariable
    {
        /// <summary>
        /// Type : GameObject
        /// </summary>
        public static ComponentValue value = new ComponentValue() { ComponentId = 3, PropertyId = 0, OperatorId = 4611686018427387905, DontCopy = true, NeedReactive = false };
        public static int Count { get { return 1; } }
    }

    public class HasPhysicalComponentVariable
    {
        /// <summary>
        /// Type : Boolean
        /// </summary>
        public static ComponentValue isHas = new ComponentValue() { ComponentId = 4, PropertyId = 0, OperatorId = 4611686018427388032, DontCopy = false, NeedReactive = true };
        public static int Count { get { return 1; } }
    }

    public class LifeCycleComponentVariable
    {
        /// <summary>
        /// Type : LifeCycleEnum
        /// </summary>
        public static ComponentValue lifeCycle = new ComponentValue() { ComponentId = 5, PropertyId = 0, OperatorId = 4611686018427387908, DontCopy = false, NeedReactive = true };
        /// <summary>
        /// Type : Single
        /// </summary>
        public static ComponentValue dealyTime = new ComponentValue() { ComponentId = 5, PropertyId = 1, OperatorId = 4611686018427387908, DontCopy = false, NeedReactive = false };
        public static int Count { get { return 2; } }
    }

    public class PoolComponentVariable
    {
        /// <summary>
        /// Type : String
        /// </summary>
        public static ComponentValue poolName = new ComponentValue() { ComponentId = 6, PropertyId = 0, OperatorId = 4611686018427388416, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : Boolean
        /// </summary>
        public static ComponentValue recover = new ComponentValue() { ComponentId = 6, PropertyId = 1, OperatorId = 4611686018427388416, DontCopy = false, NeedReactive = true };
        public static int Count { get { return 2; } }
    }

    public class PositionComponentVariable
    {
        /// <summary>
        /// Type : Vector3
        /// </summary>
        public static ComponentValue position = new ComponentValue() { ComponentId = 7, PropertyId = 0, OperatorId = 4611686018427388928, DontCopy = false, NeedReactive = true };
        public static int Count { get { return 1; } }
    }

    public class RoationComponentVariable
    {
        /// <summary>
        /// Type : Quaternion
        /// </summary>
        public static ComponentValue roation = new ComponentValue() { ComponentId = 8, PropertyId = 0, OperatorId = 4611686018427389952, DontCopy = false, NeedReactive = true };
        public static int Count { get { return 1; } }
    }

    public class Trigger2DComponentVariable
    {
        /// <summary>
        /// Type : Boolean
        /// </summary>
        public static ComponentValue isPhysical = new ComponentValue() { ComponentId = 9, PropertyId = 0, OperatorId = 4611686018427387968, DontCopy = false, NeedReactive = true };
        /// <summary>
        /// Type : List`1
        /// </summary>
        public static ComponentValue triggerList = new ComponentValue() { ComponentId = 9, PropertyId = 1, OperatorId = 4611686018427387968, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : Trigger2DCallBack
        /// </summary>
        public static ComponentValue enterCallBack = new ComponentValue() { ComponentId = 9, PropertyId = 2, OperatorId = 4611686018427387968, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : Trigger2DCallBack
        /// </summary>
        public static ComponentValue stayCallBack = new ComponentValue() { ComponentId = 9, PropertyId = 3, OperatorId = 4611686018427387968, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : Trigger2DCallBack
        /// </summary>
        public static ComponentValue exitCallBack = new ComponentValue() { ComponentId = 9, PropertyId = 4, OperatorId = 4611686018427387968, DontCopy = false, NeedReactive = false };
        public static int Count { get { return 5; } }
    }

    public class TriggerComponentVariable
    {
        /// <summary>
        /// Type : Boolean
        /// </summary>
        public static ComponentValue isPhysical = new ComponentValue() { ComponentId = 10, PropertyId = 0, OperatorId = 4611686018427387920, DontCopy = false, NeedReactive = true };
        /// <summary>
        /// Type : List`1
        /// </summary>
        public static ComponentValue triggerList = new ComponentValue() { ComponentId = 10, PropertyId = 1, OperatorId = 4611686018427387920, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : TriggerCallBack
        /// </summary>
        public static ComponentValue enterCallBack = new ComponentValue() { ComponentId = 10, PropertyId = 2, OperatorId = 4611686018427387920, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : TriggerCallBack
        /// </summary>
        public static ComponentValue stayCallBack = new ComponentValue() { ComponentId = 10, PropertyId = 3, OperatorId = 4611686018427387920, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : TriggerCallBack
        /// </summary>
        public static ComponentValue exitCallBack = new ComponentValue() { ComponentId = 10, PropertyId = 4, OperatorId = 4611686018427387920, DontCopy = false, NeedReactive = false };
        public static int Count { get { return 5; } }
    }

    public class ViewComponentVariable
    {
        /// <summary>
        /// Type : String
        /// </summary>
        public static ComponentValue prefabName = new ComponentValue() { ComponentId = 11, PropertyId = 0, OperatorId = 4611686018427387906, DontCopy = false, NeedReactive = true };
        /// <summary>
        /// Type : Transform
        /// </summary>
        public static ComponentValue parent = new ComponentValue() { ComponentId = 11, PropertyId = 1, OperatorId = 4611686018427387906, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : Vector3
        /// </summary>
        public static ComponentValue pos = new ComponentValue() { ComponentId = 11, PropertyId = 2, OperatorId = 4611686018427387906, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : Quaternion
        /// </summary>
        public static ComponentValue rot = new ComponentValue() { ComponentId = 11, PropertyId = 3, OperatorId = 4611686018427387906, DontCopy = false, NeedReactive = false };
        /// <summary>
        /// Type : HideFlags
        /// </summary>
        public static ComponentValue hideFlags = new ComponentValue() { ComponentId = 11, PropertyId = 4, OperatorId = 4611686018427387906, DontCopy = false, NeedReactive = false };
        public static int Count { get { return 5; } }
    }

}
