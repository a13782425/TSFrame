# TSFrame
## 框架说明
* 一个谁用谁SB的框架
* __本人小白一枚，无数BUG在等着你__
## 设计说明
1. 全局有Observer来管理，考虑到此类后期过于庞大，因此采用部分类，每个部分类分管自己的事情
2. System系统：初始化系统，每帧执行系统，触发系统
3. Component组件：对于实体的单一组件
## 已有功能
### Observer(观察者)
1. Observer.Instance.SetIsTest(bool)//设置是否是测试版
2. Observer.Instance.SetResourcesTime(int)//设置资源监测时间,用作资源回收
3. Observer.Instance.SetFPS(int)//设置游戏帧率
4. Observer.Instance.Run(void)//启动整个框架,要在所有设置之后调用
5. Observer.Instance.Pause(void)//暂停游戏
6. Observer.Instance.Continue(void)//继续游戏
7. Observer.Instance.GameOneStep(void)//暂停时候用于一步一步执行
8. Observer.Instance.AddSystem(ISystem)//增加一个非内置系统
9. Observer.Instance.CreateEntity(void)//创建一个没有父亲的实体
11. Observer.Instance.CreateEntity(Entity)//创建一个有父亲的实体
12. Observer.Instance.ResourcesLoad(string,bool)//在Resoures下面加载一个资源,并设置时候自动回收
13. Observer.Instance.ResourcesCollect(bool)//强制回收一次资源
### Entity(实体)
1. entity.GetGUID(void)//获取GUID
2. entity.GetId(void)//获取唯一ID
3. entity.GetComponentFlag(void)//获取组件标识
4. entity.AddComponent(Int64)//添加一个组件
5. entity.RemoveComponent(Int64)//删除一个组件
6. entity.SetChangeComponent(CallBack)//设置组件改变回调(请勿调用)
7. entity.SetValue(string,object)//设置上次操作的组件某个参数的值
8. entity.SetValue(ComponentValue,object)//设置一个组件某个参数的值
9. entity.SetValue(Int64,string,object)//设置一个组件某个参数的值
9. entity.GetValue<T>(string)//获取上次操作的组件某个参数的值
9. entity.GetValue<T>(ComponentValue)//获取一个组件某个参数的值
9. entity.GetValue<T>(Int64,string)//获取一个组件某个参数的值
### GenerateComponentEditor(组件生成工具)
1. 点击TSFrame/GenerateComponent,会生成对应的ComponentValue类
### Component(组件)
1. 必须继承IComponent
2. 需要数据驱动的需要继承IReactiveComponent,并在对应字段添加DataDriven特性

## 工作进度
1. 完成Component数据驱动
2. System模块第一版完成
3. 碰撞系统完成(有带测试)
4. 修改文件结构(使其更符合插件的结构)
5. 增加Observer暂停和一步功能
6. 增加Observer设置帧率功能
## 下一步工作
1. 增加场景切换Entity的销毁
2. 优化System模块
3. 更改Observer初始化流程
4. 碰撞系统需要通知父亲
5. UI系统
6. 场景管理

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