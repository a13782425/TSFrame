//------------------------------------------------------------------------------------------------------------
//-----------------------------------generate file 2018-06-20 12:27:57----------------------------------------
//------------------------------------------------------------------------------------------------------------
using System;

public static partial class ComponentIds
{
    public const int ADDITIVE = 0;
    public const int GAME_OBJECT_NAME = 1;
    public const int INPUT = 2;
    public const int LINK = 3;
    public const int STRING = 4;
    public const int ACTIVE = 5;
    public const int COLLISION2D = 6;
    public const int COLLISION = 7;
    public const int GAME_OBJECT = 8;
    public const int HAS_PHYSICAL = 9;
    public const int LIFE_CYCLE = 10;
    public const int POOL = 11;
    public const int POSITION = 12;
    public const int ROATION = 13;
    public const int TRIGGER2D = 14;
    public const int TRIGGER = 15;
    public const int VIEW = 16;
    public const int TEST = 17;

    public const int COMPONENT_MAX_COUNT = 18;

    public static NormalComponent GetComponent(Int32 componentId)
    {
        switch (componentId)
        {
            case ComponentIds.ADDITIVE:
                return new NormalComponent(new AdditiveComponent(), ComponentIds.ADDITIVE);
            case ComponentIds.GAME_OBJECT_NAME:
                return new NormalComponent(new GameObjectNameComponent(), ComponentIds.GAME_OBJECT_NAME);
            case ComponentIds.INPUT:
                return new NormalComponent(new InputComponent(), ComponentIds.INPUT);
            case ComponentIds.LINK:
                return new NormalComponent(new LinkComponent(), ComponentIds.LINK);
            case ComponentIds.STRING:
                return new NormalComponent(new StringComponent(), ComponentIds.STRING);
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
            case ComponentIds.TEST:
                return new NormalComponent(new TestComponent(), ComponentIds.TEST);
            default:
                return null;
        }
    }

    static ComponentIds()
    {
        ComponentTypeArray[0] = typeof(AdditiveComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[0], 0); ;
        ComponentTypeArray[1] = typeof(GameObjectNameComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[1], 1); ;
        ComponentTypeArray[2] = typeof(InputComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[2], 2); ;
        ComponentTypeArray[3] = typeof(LinkComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[3], 3); ;
        ComponentTypeArray[4] = typeof(StringComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[4], 4); ;
        ComponentTypeArray[5] = typeof(ActiveComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[5], 5); ;
        ComponentTypeArray[6] = typeof(Collision2DComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[6], 6); ;
        ComponentTypeArray[7] = typeof(CollisionComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[7], 7); ;
        ComponentTypeArray[8] = typeof(GameObjectComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[8], 8); ;
        ComponentTypeArray[9] = typeof(HasPhysicalComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[9], 9); ;
        ComponentTypeArray[10] = typeof(LifeCycleComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[10], 10); ;
        ComponentTypeArray[11] = typeof(PoolComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[11], 11); ;
        ComponentTypeArray[12] = typeof(PositionComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[12], 12); ;
        ComponentTypeArray[13] = typeof(RoationComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[13], 13); ;
        ComponentTypeArray[14] = typeof(Trigger2DComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[14], 14); ;
        ComponentTypeArray[15] = typeof(TriggerComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[15], 15); ;
        ComponentTypeArray[16] = typeof(ViewComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[16], 16); ;
        ComponentTypeArray[17] = typeof(TestComponent);
        ILHelper.RegisteComponent(ComponentTypeArray[17], 17); ;
    }

}
