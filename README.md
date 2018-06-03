# TSFrame
## 框架说明
* 一个谁用谁SB的框架
* __本人小白一枚，无数BUG在等着你__
## 设计说明
1. 全局有Observer来管理，考虑到此类后期过于庞大，因此采用部分类，每个部分类分管自己的事情
2. System系统：初始化系统，每帧执行系统，触发系统
3. Component组件：__此代码需要用反射去做处理,暂时未做__
## 工作进度
1. 完成Component数据驱动
2. System模块第一版完成
## 下一步工作
1. 优化System模块
2. 更改Observer初始化流程

## 目前使用方法
+ 文字描述
1. 目前不知道写点啥
+ 代码示例
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