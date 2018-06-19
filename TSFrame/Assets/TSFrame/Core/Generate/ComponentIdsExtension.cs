//------------------------------------------------------------------------------------------------------------
//-----------------------------------generate file 2018-06-19 23:41:04----------------------------------------
//------------------------------------------------------------------------------------------------------------
using System;

public static partial class ComponentIds
{
    public static NormalComponent GetComponent(Int64 componentId)
    {
        switch (componentId)
        {
            case ComponentIds.ADDITIVE:
                return new NormalComponent(new AdditiveComponent());
            case ComponentIds.GAME_OBJECT_NAME:
                return new NormalComponent(new GameObjectNameComponent());
            case ComponentIds.INPUT:
                return new NormalComponent(new InputComponent());
            case ComponentIds.LINK:
                return new NormalComponent(new LinkComponent());
            case ComponentIds.STRING:
                return new NormalComponent(new StringComponent());
            case ComponentIds.ACTIVE:
                return new NormalComponent(new ActiveComponent());
            case ComponentIds.COLLISION2D:
                return new NormalComponent(new Collision2DComponent());
            case ComponentIds.COLLISION:
                return new NormalComponent(new CollisionComponent());
            case ComponentIds.GAME_OBJECT:
                return new NormalComponent(new GameObjectComponent());
            case ComponentIds.HAS_PHYSICAL:
                return new NormalComponent(new HasPhysicalComponent());
            case ComponentIds.LIFE_CYCLE:
                return new NormalComponent(new LifeCycleComponent());
            case ComponentIds.POOL:
                return new NormalComponent(new PoolComponent());
            case ComponentIds.POSITION:
                return new NormalComponent(new PositionComponent());
            case ComponentIds.ROATION:
                return new NormalComponent(new RoationComponent());
            case ComponentIds.TRIGGER2D:
                return new NormalComponent(new Trigger2DComponent());
            case ComponentIds.TRIGGER:
                return new NormalComponent(new TriggerComponent());
            case ComponentIds.VIEW:
                return new NormalComponent(new ViewComponent());
            case ComponentIds.TEST:
                return new NormalComponent(new TestComponent());
            default:
                return null;
        }
    }
}
