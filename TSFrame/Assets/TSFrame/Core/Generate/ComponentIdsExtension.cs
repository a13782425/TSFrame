//------------------------------------------------------------------------------------------------------------
//-----------------------------------generate file 2018-06-19 14:34:51----------------------------------------
//------------------------------------------------------------------------------------------------------------
using System;

public static partial class ComponentIds
{
    public static IComponent GetComponent(Int64 componentId)
    {
        switch (componentId)
        {
            case ComponentIds.ADDITIVE:
                return new AdditiveComponent();
            case ComponentIds.GAME_OBJECT_NAME:
                return new GameObjectNameComponent();
            case ComponentIds.INPUT:
                return new InputComponent();
            case ComponentIds.LINK:
                return new LinkComponent();
            case ComponentIds.STRING:
                return new StringComponent();
            case ComponentIds.ACTIVE:
                return new ActiveComponent();
            case ComponentIds.COLLISION2D:
                return new Collision2DComponent();
            case ComponentIds.COLLISION:
                return new CollisionComponent();
            case ComponentIds.GAME_OBJECT:
                return new GameObjectComponent();
            case ComponentIds.HAS_PHYSICAL:
                return new HasPhysicalComponent();
            case ComponentIds.LIFE_CYCLE:
                return new LifeCycleComponent();
            case ComponentIds.POOL:
                return new PoolComponent();
            case ComponentIds.POSITION:
                return new PositionComponent();
            case ComponentIds.ROATION:
                return new RoationComponent();
            case ComponentIds.TRIGGER2D:
                return new Trigger2DComponent();
            case ComponentIds.TRIGGER:
                return new TriggerComponent();
            case ComponentIds.VIEW:
                return new ViewComponent();
            case ComponentIds.TEST:
                return new TestComponent();
            default:
                return null;
        }
    }
}
