using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CollisionModel
{
    public Collision CurrentCollision { get; set; }
    public CollisionEnum CollisionState { get; set; }
}

public class Collision2DModel
{
    public Collision2D CurrentCollision { get; set; }
    public CollisionEnum CollisionState { get; set; }
}

public class TriggerModel
{
    public Collider CurrentCollider { get; set; }
    public TriggerEnum TriggerState { get; set; }
}

public class Trigger2DModel
{
    public Collider2D CurrentCollider { get; set; }
    public TriggerEnum TriggerState { get; set; }
}
