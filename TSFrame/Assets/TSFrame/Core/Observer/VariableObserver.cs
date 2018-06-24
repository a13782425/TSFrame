using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed partial class Observer
{
    private static bool isCreate = false;

    private static Observer _instance = null;
    /// <summary>
    /// 观察者单例
    /// </summary>
    public static Observer Instance
    {
        get
        {
            if (!isCreate)
            {
                GameObject obj = new GameObject();
                obj.name = "Observer";
                DontDestroyOnLoad(obj);
                _instance = obj.AddComponent<Observer>();
                obj.hideFlags = HideFlags.HideInHierarchy;
                isCreate = true;
            }
            return _instance;
        }
    }

    /// <summary>
    /// 游戏内所有实体
    /// </summary>
    private Dictionary<int, Entity> _entityDic;
    /// <summary>
    /// 测试工程
    /// </summary>
    private bool _isTest = false;
    /// <summary>
    /// 启动观察者
    /// </summary>
    private bool _isRun = false;

    private static bool _isPlaying = false;
    /// <summary>
    /// 游戏是否在运行
    /// </summary>
    public static bool IsPlaying { get { return _isPlaying; } }

    private static float _allTime = 0f;
    /// <summary>
    /// 游戏从运行开始的全部时间
    /// </summary>
    public static float AllTime { get { return _allTime; } }

    private static float _deltaTime = 0f;
    /// <summary>
    /// 每一帧的时间
    /// </summary>
    public static float DeltaTime { get { return _deltaTime; } }

    private static bool _isUseThread = false;
    /// <summary>
    /// 是否使用线程
    /// </summary>
    public static bool IsUseThread { get { return _isUseThread; } }


    #region Variable Var

    /// <summary>
    /// 变量游戏物体
    /// </summary>
    private GameObject _variableGameObject;

    #endregion

    #region Game Var

    /// <summary>
    /// 游戏游戏物体
    /// </summary>
    private GameObject _gameGameObject;
    /// <summary>
    /// 暂停
    /// </summary>
    private bool _pause = false;

    #endregion

    #region Match Var

    /// <summary>
    /// 匹配游戏物体
    /// </summary>
    private GameObject _matchGameObject;
    ///// <summary>
    ///// 匹配组的字典
    ///// </summary>
    //private Dictionary<ComponentFlag, Group> _matchGroupDic;
    /// <summary>
    /// 匹配组的字典
    /// </summary>
    private HashSet<Group> _matchGroupHashSet;

    #endregion

    #region System Var

    /// <summary>
    /// 系统游戏物体
    /// </summary>
    private GameObject _systemGameObject;
    /// <summary>
    /// 初始化执行的系统
    /// </summary>
    private List<ISystem> _systemInitList;
    /// <summary>
    /// 触发执行的系统
    /// </summary>
    private List<ReactiveSystemDto> _systemReactiveDic;
    /// <summary>
    /// 循环执行的系统
    /// </summary>
    private List<ISystem> _systemExecuteList;
    #endregion

    #region Entity Var

    /// <summary>
    /// 实体游戏物体
    /// </summary>
    private GameObject _entityGameObject;

    #endregion

    #region Resources Var

    private GameObject _resourcesGameObject;

    private Dictionary<string, ResourcesDto> _resourcesDtoDic;

    /// <summary>
    /// 资源GC的时间(秒)
    /// </summary>
    private int _resourcesTime = 180;

    #endregion

    #region Pool Var
    /// <summary>
    /// 对象池实体
    /// </summary>
    private GameObject _poolGameObject;
    /// <summary>
    /// 组件对象池
    /// </summary>
    private ComponentPoolDto[] _componentPoolArray;
    //private Dictionary<Int64, ComponentPoolDto> _componentPoolDic;
    /// <summary>
    /// 用户自定义实体对象池
    /// </summary>
    private Dictionary<string, EntitySubPoolDto> _entityPoolDic;
    /// <summary>
    /// 默认实体对象池
    /// </summary>
    private EntitySubPoolDto _entityDefaultPool;
    /// <summary>
    /// 全部实体对象池
    /// </summary>
    private EntityPoolDto _allEntityPool;
    /// <summary>
    /// 共享组件管理
    /// </summary>
    private Dictionary<int, SharedComponent> _sharedComponentDic;
    #endregion

    #region Implement Method

    partial void VariableLoad()
    {
        _entityDic = new Dictionary<int, Entity>();
        _systemInitList = new List<ISystem>();
        _systemReactiveDic = new List<ReactiveSystemDto>();
        _systemExecuteList = new List<ISystem>();
        _resourcesDtoDic = new Dictionary<string, ResourcesDto>();
        _componentPoolArray = new ComponentPoolDto[ComponentIds.COMPONENT_MAX_COUNT];
        _entityPoolDic = new Dictionary<string, EntitySubPoolDto>();
        _sharedComponentDic = new Dictionary<int, SharedComponent>();
        _matchGroupHashSet = new HashSet<Group>();
        CreateComponentPool();
        CreateEntityDefaultPool();
        _variableGameObject = new GameObject("VariableGameObject");
        _variableGameObject.transform.SetParent(this.transform);
    }

    private void CreateEntityDefaultPool()
    {
        Entity entity = new Entity(MatchEntity, GetComponent);
        entity.AddComponent(ComponentIds.ACTIVE);
        entity.AddComponent(ComponentIds.LIFE_CYCLE);
        entity.AddComponent(ComponentIds.POOL);
        _allEntityPool = new EntityPoolDto(entity);
        _entityDefaultPool = new EntitySubPoolDto(entity);
    }

    partial void CreateComponentPool()
    {
        //int length = ComponentIds.ComponentTypeArray.Length;
        for (int i = 0; i < ComponentIds.COMPONENT_MAX_COUNT; i++)
        {
            _componentPoolArray[i] = new ComponentPoolDto(i);
            _componentPoolArray[i].Init(10);
        }
        //foreach (KeyValuePair<Int64, Type> item in ComponentIds.ComponentTypeDic)
        //{
        //    _componentPoolDic.Add(item.Key, new ComponentPoolDto(item.Key));
        //}
    }

    partial void VariableUpdate()
    {
    }

    #endregion

    #region Class

    class ResourcesDto
    {
        private string _pathName = "";

        public string PathName { get { return _pathName; } }

        private Object _cacheObj = null;

        public Object CacheObj { get { return _cacheObj; } }

        private bool _isAutoRecycle = true;
        /// <summary>
        /// 是否自动回收
        /// </summary>
        public bool IsAutoRecycle { get { return _isAutoRecycle; } set { _isAutoRecycle = value; } }

        private bool _isCanRecycle = false;
        /// <summary>
        /// 是否可以回收
        /// </summary>
        public bool IsCanRecycle { get { return _isCanRecycle; } set { _isCanRecycle = value; } }

        /// <summary>
        /// 最后使用时间
        /// </summary>
        public float LastUseTime { get; set; }

        public ResourcesDto(string path, Object obj, bool isRecycle = true)
        {
            if (string.IsNullOrEmpty(path) || obj == null)
            {
                throw new Exception("Resources cache create failure");
            }
            _isAutoRecycle = isRecycle;
            _pathName = path;
            _cacheObj = obj;
        }

        public void Release()
        {
            _cacheObj = null;
            _pathName = null;
        }
        ~ResourcesDto()
        {
            Release();
        }

        public static bool operator ==(ResourcesDto res1, ResourcesDto res2)
        {
            object o1 = res1;
            object o2 = res2;
            if (o1 == null && o2 == null)
            {
                return true;
            }
            if (o1 == null || o2 == null)
            {
                return false;
            }
            return res1.PathName == res2.PathName && res1.CacheObj == res2.CacheObj;
        }
        public static bool operator !=(ResourcesDto res1, ResourcesDto res2)
        {
            return !(res1 == res2);
        }

        public static bool operator ==(ResourcesDto res1, string res2)
        {
            object o1 = res1;
            if (o1 == null && res2 == null)
            {
                return true;
            }
            if (o1 == null || res2 == null)
            {
                return false;
            }
            return res1.PathName == res2;
        }
        public static bool operator !=(ResourcesDto res1, string res2)
        {
            return !(res1 == res2);
        }

        public static bool operator ==(string res2, ResourcesDto res1)
        {
            return res1 == res2;
        }
        public static bool operator !=(string res2, ResourcesDto res1)
        {
            return !(res1 == res2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return this.PathName.GetHashCode();
        }


    }

    class EntitySubPoolDto
    {
        private Queue<int> _indexQueue;
        private Entity _origin = null;
        public EntitySubPoolDto(Entity origin)
        {
            if (origin == null)
            {
                throw new Exception("实体对象池源为空");
            }
            _origin = origin;
            _origin.SetValue(ActiveComponentVariable.active, false);
            _indexQueue = new Queue<int>();
        }
        public Entity Dequeue()
        {
            Entity entity = null;
            int index = -1;
            if (_indexQueue.Count > 0)
            {
                index = _indexQueue.Dequeue();
            }
            entity = Instance._allEntityPool.Dequeue(index);
            return entity;
        }

        public bool Contains(Entity entity)
        {
            return Instance._allEntityPool.Contains(entity);
        }

        public bool Enqueue(Entity entity)
        {
            int index = entity.GetId();
            if (Instance._allEntityPool.Enqueue(entity, index))
            {
                _indexQueue.Enqueue(index);
                return true;
            }
            return false;
        }
    }

    class EntityPoolDto
    {
        private Entity _origin = null;
        private List<Entity> _entityList;
        private Queue<int> _indexQueue;
        public EntityPoolDto(Entity origin)
        {
            if (origin == null)
            {
                throw new Exception("实体对象池源为空");
            }
            _origin = origin;
            _origin.SetValue(ActiveComponentVariable.active, false);
            _entityList = new List<Entity>();
            _entityList.Add(origin);
            _indexQueue = new Queue<int>();
        }

        public Entity Dequeue(int index)
        {
            Entity entity = null;
            if (index > 0)
            {
                entity = _entityList[index];
                _entityList[index] = null;
                return entity;
            }
            //if (_indexQueue.Count > 0)
            //{
            //    int index = _indexQueue.Dequeue();
            //    entity = _entityList[index];
            //    _entityList[index] = null;
            //    return entity;
            //}
            entity = Instance.GetEntity();
            entity.CopyComponent(_origin).Parent = _origin.Parent;
            _entityList.Add(null);
            return entity;
        }

        public bool Contains(Entity entity)
        {
            if (_entityList.Count > entity.GetId())
            {
                return _entityList[entity.GetId()] != null;
            }
            throw new Exception("该对象不属于对象池!");
        }

        public bool Enqueue(Entity entity, int index)
        {
            if (_entityList.Count > index)
            {
                if (_entityList[index] == null)
                {
                    _indexQueue.Enqueue(index);
                    _entityList[index] = entity;
                    return true;
                }
                return false;

            }
            return false;
        }
    }

    class ComponentPoolDto
    {
        private List<NormalComponent> _componentList;
        private Queue<int> _indexQueue;
        private int _componentId;

        public ComponentPoolDto(int componentId)
        {
            _componentId = componentId;
            _componentList = new List<NormalComponent>();
            _indexQueue = new Queue<int>();
        }

        public void Init(int count)
        {
            for (int i = 0; i < 10; i++)
            {
                NormalComponent component = ComponentIds.GetComponent(_componentId);
                if (component == null)
                {
                    throw new Exception("需要获取的组件不存在");
                }
                component.InstanceId = _componentList.Count;
                _componentList.Add(component);
                _indexQueue.Enqueue(i);
            }
        }

        public NormalComponent Dequeue()
        {
            NormalComponent component = null;
            if (_indexQueue.Count > 0)
            {
                int index = _indexQueue.Dequeue();
                component = _componentList[index];
                _componentList[index] = null;
                return component;
            }
            component = ComponentIds.GetComponent(_componentId);
            if (component == null)
            {
                throw new Exception("需要获取的组件不存在");
            }
            component.InstanceId = _componentList.Count;
            _componentList.Add(null);
            return component;
        }

        public bool Contains(NormalComponent component)
        {
            if (_componentList.Count > component.InstanceId)
            {
                return _componentList[component.InstanceId] != null;
            }
            throw new Exception("该对象不属于对象池!");
        }

        public bool Enqueue(NormalComponent component)
        {
            int index = component.InstanceId;
            if (_componentList.Count > index)
            {
                if (_componentList[index] == null)
                {
                    _indexQueue.Enqueue(index);
                    _componentList[index] = component;
                    return true;
                }
                return false;

            }
            return false;
        }
    }

    class ReactiveSystemDto
    {
        private IReactiveSystem _currentSystem;

        public IReactiveSystem CurrentSystem { get { return _currentSystem; } }

        private HashSet<Entity> _entityHashSet;

        public HashSet<Entity> EntityHashSet { get { return _entityHashSet; } }

        public ReactiveSystemDto(IReactiveSystem reactiveSystem)
        {
            if (reactiveSystem == null)
            {
                throw new Exception("System Is Null");
            }
            _currentSystem = reactiveSystem;
            _entityHashSet = new HashSet<Entity>();
        }

        public override int GetHashCode()
        {
            return CurrentSystem.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    #endregion
}
