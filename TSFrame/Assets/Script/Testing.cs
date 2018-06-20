using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Diagnostics;
using System.Linq;

public class Testing : MonoBehaviour
{

    void Start()
    {
        //初始化观察者
        Observer.Instance
            .SetIsTest(true)//设置是否是测试版
            .SetResourcesTime(100)
            .GameLaunch();//启动观察者
        res = Resources.Load<GameObject>("Test");
    }
    private List<GameObject> monoList = new List<GameObject>();
    private List<Entity> ecsList = new List<Entity>();

    private GameObject res;
    Stopwatch stopwatch = new Stopwatch();

    private GameObject monoTest;
    private Entity entityTest;
    [SerializeField]
    private int Count = 10000;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < Count; i++)
            {
                GameObject obj = GameObject.Instantiate(res);
                monoList.Add(obj);
            }
            stopwatch.Stop();
            Debug.LogError("使用mono创建" + Count + "个用时:" + stopwatch.Elapsed);
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < Count; i++)
            {
                Entity entity = Observer.Instance.CreateEntity().AddComponent(ComponentIds.VIEW).AddComponent(ComponentIds.GAME_OBJECT).SetValue(ViewComponentVariable.prefabName, "Test");
                ecsList.Add(entity);
            }
            stopwatch.Stop();
            Debug.LogError("使用ecs创建" + Count + "个用时:" + stopwatch.Elapsed);
            monoTest = GameObject.Instantiate(res);
            entityTest = Observer.Instance.CreateEntity().AddComponent(ComponentIds.VIEW).AddComponent(ComponentIds.GAME_OBJECT).SetValue(ViewComponentVariable.prefabName, "Test");
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < Count; i++)
            {
                monoList[i].AddComponent<TestMono>();
            }
            stopwatch.Stop();
            Debug.LogError("使用mono添加" + Count + "个组件用时:" + stopwatch.Elapsed);
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < Count; i++)
            {
                ecsList[i].AddComponent(ComponentIds.POSITION);//.AddComponent(ComponentIds.ROATION);
            }
            stopwatch.Stop();
            Debug.LogError("使用ecs添加" + Count + "个组件用时:" + stopwatch.Elapsed);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < Count; i++)
            {
                DestroyImmediate(monoList[i].GetComponent<TestMono>());
            }
            stopwatch.Stop();
            Debug.LogError("使用mono删除" + Count + "个组件用时:" + stopwatch.Elapsed);
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < Count; i++)
            {
                ecsList[i].RemoveComponent(ComponentIds.POSITION);//.RemoveComponent(ComponentIds.ROATION);
            }
            stopwatch.Stop();
            Debug.LogError("使用ecs删除" + Count + "个组件用时:" + stopwatch.Elapsed);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < Count; i++)
            {
                monoTest.AddComponent<TestMono>();
                DestroyImmediate(monoTest.GetComponent<TestMono>());
            }
            stopwatch.Stop();
            Debug.LogError("使用mono添加删除" + Count + "个组件用时:" + stopwatch.Elapsed);
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < Count; i++)
            {
                entityTest.AddComponent(ComponentIds.POSITION);//.AddComponent(ComponentIds.ROATION);
                entityTest.RemoveComponent(ComponentIds.POSITION);//.RemoveComponent(ComponentIds.ROATION);
            }
            stopwatch.Stop();
            Debug.LogError("使用ecs添加删除" + Count + "个组件用时:" + stopwatch.Elapsed);
        }
    }
}
class TestA
{
    public int id { get; set; }
    public override int GetHashCode()
    {
        return 1;
    }
}

class TestMono : MonoBehaviour
{
    public Vector3 pos;
    //public Quaternion quta;
}