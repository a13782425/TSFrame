//------------------------------------------------------------------------------------------------------------
//-----------------------------------generate file 2018-06-24 21:52:54----------------------------------------
//------------------------------------------------------------------------------------------------------------
using System;

namespace TSFrame.ECS
{

    public static partial class ComponentIds
    {
        public const int ACTIVE = 0;
        public const int COLLISION2D = 1;
        public const int COLLISION = 2;
        public const int GAME_OBJECT = 3;
        public const int HAS_PHYSICAL = 4;
        public const int LIFE_CYCLE = 5;
        public const int POOL = 6;
        public const int POSITION = 7;
        public const int ROATION = 8;
        public const int TRIGGER2D = 9;
        public const int TRIGGER = 10;
        public const int VIEW = 11;

        public const int COMPONENT_MAX_COUNT = 12;

        public static NormalComponent GetComponent(Int32 componentId)
        {
            switch (componentId)
            {
                case ComponentIds.ACTIVE:
                    return new NormalComponent(new TSFrame.ECS.ActiveComponent(), ComponentIds.ACTIVE);
                case ComponentIds.COLLISION2D:
                    return new NormalComponent(new TSFrame.ECS.Collision2DComponent(), ComponentIds.COLLISION2D);
                case ComponentIds.COLLISION:
                    return new NormalComponent(new TSFrame.ECS.CollisionComponent(), ComponentIds.COLLISION);
                case ComponentIds.GAME_OBJECT:
                    return new NormalComponent(new TSFrame.ECS.GameObjectComponent(), ComponentIds.GAME_OBJECT);
                case ComponentIds.HAS_PHYSICAL:
                    return new NormalComponent(new TSFrame.ECS.HasPhysicalComponent(), ComponentIds.HAS_PHYSICAL);
                case ComponentIds.LIFE_CYCLE:
                    return new NormalComponent(new TSFrame.ECS.LifeCycleComponent(), ComponentIds.LIFE_CYCLE);
                case ComponentIds.POOL:
                    return new NormalComponent(new TSFrame.ECS.PoolComponent(), ComponentIds.POOL);
                case ComponentIds.POSITION:
                    return new NormalComponent(new TSFrame.ECS.PositionComponent(), ComponentIds.POSITION);
                case ComponentIds.ROATION:
                    return new NormalComponent(new TSFrame.ECS.RoationComponent(), ComponentIds.ROATION);
                case ComponentIds.TRIGGER2D:
                    return new NormalComponent(new TSFrame.ECS.Trigger2DComponent(), ComponentIds.TRIGGER2D);
                case ComponentIds.TRIGGER:
                    return new NormalComponent(new TSFrame.ECS.TriggerComponent(), ComponentIds.TRIGGER);
                case ComponentIds.VIEW:
                    return new NormalComponent(new TSFrame.ECS.ViewComponent(), ComponentIds.VIEW);
                default:
                    return null;
            }
        }

        static ComponentIds()
        {
            ComponentTypeArray[0] = typeof(TSFrame.ECS.ActiveComponent);
            ILHelper.RegisteComponent(ComponentTypeArray[0], 0);
            ComponentTypeArray[1] = typeof(TSFrame.ECS.Collision2DComponent);
            ILHelper.RegisteComponent(ComponentTypeArray[1], 1);
            ComponentTypeArray[2] = typeof(TSFrame.ECS.CollisionComponent);
            ILHelper.RegisteComponent(ComponentTypeArray[2], 2);
            ComponentTypeArray[3] = typeof(TSFrame.ECS.GameObjectComponent);
            ILHelper.RegisteComponent(ComponentTypeArray[3], 3);
            ComponentTypeArray[4] = typeof(TSFrame.ECS.HasPhysicalComponent);
            ILHelper.RegisteComponent(ComponentTypeArray[4], 4);
            ComponentTypeArray[5] = typeof(TSFrame.ECS.LifeCycleComponent);
            ILHelper.RegisteComponent(ComponentTypeArray[5], 5);
            ComponentTypeArray[6] = typeof(TSFrame.ECS.PoolComponent);
            ILHelper.RegisteComponent(ComponentTypeArray[6], 6);
            ComponentTypeArray[7] = typeof(TSFrame.ECS.PositionComponent);
            ILHelper.RegisteComponent(ComponentTypeArray[7], 7);
            ComponentTypeArray[8] = typeof(TSFrame.ECS.RoationComponent);
            ILHelper.RegisteComponent(ComponentTypeArray[8], 8);
            ComponentTypeArray[9] = typeof(TSFrame.ECS.Trigger2DComponent);
            ILHelper.RegisteComponent(ComponentTypeArray[9], 9);
            ComponentTypeArray[10] = typeof(TSFrame.ECS.TriggerComponent);
            ILHelper.RegisteComponent(ComponentTypeArray[10], 10);
            ComponentTypeArray[11] = typeof(TSFrame.ECS.ViewComponent);
            ILHelper.RegisteComponent(ComponentTypeArray[11], 11);
        }

    }
}
