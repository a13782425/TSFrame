# TSFrame
## 框架说明
* 一个谁用谁SB的框架
* __本人小白一枚，无数BUG在等着你__
## 设计说明
1. 全局有Observer来管理，考虑到此类后期过于庞大，因此采用部分类，每个部分类分管自己的事情
2. System系统：初始化系统，每帧执行系统，触发系统
3. Component组件：对于实体的单一组件
## API介绍
* [中文Wiki](https://github.com/a13782425/TSFrame/wiki)

## 下一步工作
1. 增加场景切换Entity的销毁
2. 优化System模块
3. 更改Observer初始化流程
5. UI系统
6. 场景管理

## 性能测试
![性能测试](http://photo.timeslip.cn/Test.png)

## 目前使用方法
+ 文字描述
1. 目前不知道写点啥
+ 代码示例1
``` cs
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
            .AddSystem(new MoveSystem());

        //创建实体
        Entity entity = Observer.Instance
            .CreateEntity()
            .AddComponent(ComponentIds.STRING)//增加string组件
            .AddComponent(ComponentIds.INSTANTIATE)//增加实例化组件
            .AddComponent(ComponentIds.GAME_OBJECT)//增加游戏物体组件
            .SetValue(ComponentIds.STRING, "value", "string测试")//设置string组件的参数
            .SetValue(ComponentIds.INSTANTIATE, "pos", new Vector3(100, 100, 152))//设置实例化组件坐标
            .SetValue("prefabname", "Test");//设置预制物体名称
        Debug.LogError(entity.GetValue<GameObject>(ComponentIds.GAME_OBJECT, "gameobj"));//获取游戏物体组件的游戏物体
    }
```
+代码示例2
``` cs
    private void Start()
    {
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
```