//------------------------------------------------------------------------------------------------------------
//-----------------------------------generate file 2018-06-23 22:28:33----------------------------------------
//------------------------------------------------------------------------------------------------------------
using System;

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
                return new NormalComponent(new ActiveComponent(), ComponentIds.ACTIVE);
            case ComponentIds.COLLISION2D:
                return new NormalComponent(new Collision2DComponent(), ComponentIds.COLLISION2D);
            case ComponentIds.COLLISION:
                return new NormalComponent(new CollisionComponent(), ComponentIds.COLLISION);
            case ComponentIds.GAME_OBJECT:
                return new NormalComponent(new GameObjectComponent(), ComponentIds.GAME_OBJECT);
            case ComponentIds.HAS_PHYSICAL:
                return new NormalComponent(new HasPhysicalComponent(), ComponentIds.HAS_PHYSICAL);
            case ComponentIds.LIFE_CYCLE:
                return new NormalComponent(new LifeCycleComponent(), ComponentIds.LIFE_CYCLE);
            case ComponentIds.POOL:
                return new NormalComponent(new PoolComponent(), ComponentIds.POOL);
            case ComponentIds.POSITION:
                return new NormalComponent(new PositionComponent(), ComponentIds.POSITION);
            case ComponentIds.ROATION:
                return new NormalComponent(new RoationComponent(), ComponentIds.ROATION);
            case ComponentIds.TRIGGER2D:
                return new NormalComponent(new Trigger2DComponent(), ComponentIds.TRIGGER2D);
            case ComponentIds.TRIGGER:
                return new NormalComponent(new TriggerComponent(), ComponentIds.TRIGGER);
            case ComponentIds.VIEW:
                return new NormalComponent(new ViewComponent(), ComponentIds.VIEW);
            default:
                return null;
        }
    }

    static ComponentIds()
    {
        ComponentTypeArray[0] = typeof(ActiveComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[0], 0);;
        ComponentTypeArray[1] = typeof(Collision2DComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[1], 1);;
        ComponentTypeArray[2] = typeof(CollisionComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[2], 2);;
        ComponentTypeArray[3] = typeof(GameObjectComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[3], 3);;
        ComponentTypeArray[4] = typeof(HasPhysicalComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[4], 4);;
        ComponentTypeArray[5] = typeof(LifeCycleComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[5], 5);;
        ComponentTypeArray[6] = typeof(PoolComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[6], 6);;
        ComponentTypeArray[7] = typeof(PositionComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[7], 7);;
        ComponentTypeArray[8] = typeof(RoationComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[8], 8);;
        ComponentTypeArray[9] = typeof(Trigger2DComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[9], 9);;
        ComponentTypeArray[10] = typeof(TriggerComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[10], 10);;
        ComponentTypeArray[11] = typeof(ViewComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[11], 11);;
    }

}
