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
    private void Start()
    {
        //初始化观察者
        Observer.Instance
            .SetIsTest(true)//设置是否是测试版
            .SetResourcesTime(100)//设置资源监测时间
            .Run();//启动观察者
        //增加系统
        Observer.Instance
            .AddSystem(new MoveSystem())
            .AddSystem(new InputSystem());

        //创建实体
        Entity entity1 = Observer.Instance
            .CreateEntity()
            .AddComponent(ComponentIds.STRING)//增加string组件
            .AddComponent(ComponentIds.INPUT)
            .AddComponent(ComponentIds.VIEW)//增加实例化组件
            .AddComponent(ComponentIds.GAME_OBJECT)//增加游戏物体组件
            .SetValue(ComponentIds.STRING, "value", "string测试")//设置string组件的参数
            .SetValue(ComponentIds.VIEW, "pos", new Vector3(0, 0, 15))//设置实例化组件坐标
            .SetValue("prefabname", "Test");//设置预制物体名称
        Debug.LogError(entity1.GetValue<GameObject>(ComponentIds.GAME_OBJECT, "value"));//获取游戏物体组件的游戏物体
        
        //创建带有碰撞的对象
        Entity entity2 = Observer.Instance
            .CreateEntity()
            .AddComponent(ComponentIds.INPUT)
            .AddComponent(ComponentIds.VIEW)
            .AddComponent(ComponentIds.GAME_OBJECT)
            .AddComponent(ComponentIds.HAS_PHYSICAL)
            .AddComponent(ComponentIds.COLLISION)
            .SetValue(ViewComponentVariable.prefabName, "Test")
            .SetValue(ViewComponentVariable.pos, new Vector3(0, 0, 15))
            .SetValue(CollisionComponentVariable.enterCallBack, new CollisionCallBack(enterCallBack))
            .SetValue(CollisionComponentVariable.exitCallBack, new CollisionCallBack(exitCallBack))
            .SetValue(CollisionComponentVariable.stayCallBack, new CollisionCallBack(stayCallBack))
            .SetValue(HasPhysicalComponentVariable.isHas, true);
        Entity entity3 = Observer.Instance
            .CreateEntity()
            .AddComponent(ComponentIds.VIEW)
            .AddComponent(ComponentIds.GAME_OBJECT)
            .SetValue(ViewComponentVariable.prefabName, "Test")
            .SetValue(ViewComponentVariable.pos, new Vector3(10, 0, 15));

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