using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Awake()
    {
        //Observer.Instance.SetIsTest(true);
    }
    Entity entity1;
    private void Start()
    {
        //初始化观察者
        Observer.Instance
            .SetIsTest(true)//设置是否是测试版
            .SetResourcesTime(100)//设置资源监测时间
            .GameLaunch();//启动观察者
        //增加系统
        Observer.Instance
            .AddSystem(new MoveSystem())
            .AddSystem(new InputSystem());

        //创建移动的实体
        entity1 = Observer.Instance
            .CreateEntity()
            .AddComponent(ComponentIds.STRING)//增加string组件
            .AddComponent(ComponentIds.INPUT)
            .AddComponent(ComponentIds.VIEW)//增加实例化组件
            .AddComponent(ComponentIds.GAME_OBJECT)//增加游戏物体组件
            .SetValue(ComponentIds.STRING, "value", "string测试")//设置string组件的参数
            .SetValue(ComponentIds.VIEW, "pos", new Vector3(0, 0, 15))//设置实例化组件坐标
            .SetValue("prefabname", "Test")
            .SetValue(ActiveComponentVariable.active, false);//设置预制物体名称
        Observer.Instance.CreatePool("Test", entity1);


        ////创建带有碰撞的对象
        //Entity entity2 = Observer.Instance
        //    .CreateEntity()
        //    .AddComponent(ComponentIds.INPUT)
        //    .AddComponent(ComponentIds.VIEW)
        //    .AddComponent(ComponentIds.GAME_OBJECT)
        //    .AddComponent(ComponentIds.HAS_PHYSICAL)
        //    .AddComponent(ComponentIds.COLLISION)
        //    .SetValue(ViewComponentVariable.prefabName, "Test")
        //    .SetValue(ViewComponentVariable.pos, new Vector3(0, 0, 15))
        //    .SetValue(CollisionComponentVariable.enterCallBack, new CollisionCallBack(enterCallBack))
        //    .SetValue(CollisionComponentVariable.exitCallBack, new CollisionCallBack(exitCallBack))
        //    .SetValue(CollisionComponentVariable.stayCallBack, new CollisionCallBack(stayCallBack))
        //    .SetValue(HasPhysicalComponentVariable.isHas, true);
        //Entity entity3 = Observer.Instance
        //    .CreateEntity()
        //    .AddComponent(ComponentIds.VIEW)
        //    .AddComponent(ComponentIds.GAME_OBJECT)
        //    .SetValue(ViewComponentVariable.prefabName, "Test")
        //    .SetValue(ViewComponentVariable.pos, new Vector3(10, 0, 15));

        ////拷贝一个实体的所有组件
        //Entity entity4 = Observer.Instance
        // .CreateEntity().CopyComponent(entity3);
    }
    List<Entity> entities = new List<Entity>();
    float time = 0;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            entities.Add(Observer.Instance.CreateEntityToPool("Test"));
        }
        if (time > 5)
        {
            time = 0;
            Entity entity = entities[0];
            entities.RemoveAt(0);
            entity.SetValue(PoolComponentVariable.recover, true);
        }
        time += Time.deltaTime;
    }
    private void stayCallBack(Entity self, Collision target)
    {
        Debug.LogError("Stay");
    }

    private void exitCallBack(Entity self, Collision target)
    {
        Debug.LogError("Exit");
    }

    private void enterCallBack(Entity entity, Collision collision)
    {
        Debug.LogError("Enter");
    }
}