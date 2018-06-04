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
            .AddSystem(new InstantiateSystem())
            .AddSystem(new MoveSystem())
            .AddSystem(new InputSystem());

        //创建实体
        Entity entity = Observer.Instance
            .CreateEntity()
            .AddComponent(ComponentIds.STRING)//增加string组件
            .AddComponent(ComponentIds.INPUT)
            .AddComponent(ComponentIds.INSTANTIATE)//增加实例化组件
            .AddComponent(ComponentIds.GAME_OBJECT)//增加游戏物体组件
            .SetValue(ComponentIds.STRING, "value", "string测试")//设置string组件的参数
            .SetValue(ComponentIds.INSTANTIATE, "pos", new Vector3(0, 0, 15))//设置实例化组件坐标
            .SetValue("prefabname", "Test");//设置预制物体名称
        Debug.LogError(entity.GetValue<GameObject>(ComponentIds.GAME_OBJECT, "value"));//获取游戏物体组件的游戏物体
    }
}