using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public delegate void ComponentCallBack(Entity entity, NormalComponent component);

public delegate NormalComponent GetComponentFunc(Int64 componentId);

public delegate void ValueChangeCallBack(Entity entity, Int64 componentId);

public delegate void CollisionCallBack(Entity self, Collision target);

public delegate void TriggerCallBack(Entity self, Collider target);

public delegate void Collision2DCallBack(Entity self, Collision2D target);

public delegate void Trigger2DCallBack(Entity self, Collider2D target);

