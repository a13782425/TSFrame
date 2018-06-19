using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Diagnostics;
using System.Linq;

public class Testing : MonoBehaviour
{

    //Stopwatch stopwatch = new Stopwatch();
    //Dictionary<int, int> dic = new Dictionary<int, int>();
    //private void Start()
    //{
    //    for (int i = 0; i < 1000; i++)
    //    {
    //        dic.Add(i, i);
    //    }
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.B))
    //    {
    //        int num;
    //        stopwatch.Reset();
    //        stopwatch.Start();
    //        foreach (KeyValuePair<int, int> item in dic)
    //        {
    //            num = item.Value;
    //        }
    //        stopwatch.Stop();
    //        Debug.LogError("foreach:" + stopwatch.Elapsed);
    //        stopwatch.Reset();
    //        stopwatch.Start();
    //        List<int> keys = dic.Keys.ToList();
    //        int count = keys.Count;
    //        for (int i = 0; i < count; i++)
    //        {
    //            num = dic[keys[i]];
    //        }
    //        stopwatch.Stop();
    //        Debug.LogError("for:" + stopwatch.Elapsed);

    //    }
    //}

    //Stopwatch stopwatch = new Stopwatch();
    //void Start()
    //{

    //    HashSet<int> test = new HashSet<int>();
    //    for (int i = 0; i < 10000; i++)
    //    {
    //        test.Add(i);
    //    }
    //    stopwatch.Reset();
    //    stopwatch.Start();
    //    for (int i = 0; i < 10000; i++)
    //    {
    //        test.Add(i);
    //    }
    //    stopwatch.Stop();
    //    Debug.LogError("添加一样:" + stopwatch.Elapsed);
    //    stopwatch.Reset();
    //    stopwatch.Start();
    //    for (int i = 100000; i < 110000; i++)
    //    {
    //        test.Add(i);
    //    }
    //    stopwatch.Stop();
    //    Debug.LogError("添加不一样:" + stopwatch.Elapsed);
    //}

    //void Start()
    //{
    //    Stopwatch stopwatch = new Stopwatch();
    //    Dictionary<long, int> dic = new Dictionary<long, int>();
    //    Dictionary<string, int> dicStr = new Dictionary<string, int>();
    //    string str = "";
    //    for (int i = 0; i < 10000; i++)
    //    {
    //        dic.Add(long.MaxValue - i, i);
    //        if (i == 999)
    //        {
    //            str = System.Guid.NewGuid().ToString();
    //            dicStr.Add(str, i);
    //        }
    //        else
    //            dicStr.Add(System.Guid.NewGuid().ToString(), i);
    //    }
    //    int num;
    //    stopwatch.Reset();
    //    stopwatch.Start();

    //    for (int i = 0; i < 10000; i++)
    //    {
    //        num = dic[long.MaxValue];
    //    }
    //    stopwatch.Stop();
    //    Debug.LogError("int:" + stopwatch.Elapsed);
    //    stopwatch.Reset();
    //    stopwatch.Start();

    //    for (int i = 0; i < 10000; i++)
    //    {
    //        num = dicStr[str];
    //    }
    //    stopwatch.Stop();
    //    Debug.LogError("string:" + stopwatch.Elapsed);
    //}

    //private void Start()
    //{
    //    TestA[] arr = new TestA[10];
    //    for (int i = 0; i < 10; i++)
    //    {
    //        arr[i] = new TestA();
    //    }
    //    TestA[] arrTwo = arr;
    //    arrTwo[0].id = 9999;
    //    Debug.LogError(arr[0].id);
    //}

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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                GameObject obj = GameObject.Instantiate(res);
                monoList.Add(obj);
            }
            stopwatch.Stop();
            Debug.LogError("使用mono创建用时:" + stopwatch.Elapsed);
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                Entity entity = Observer.Instance.CreateEntity().AddComponent(ComponentIds.VIEW).AddComponent(ComponentIds.GAME_OBJECT).SetValue(ViewComponentVariable.prefabName, "Test");
                ecsList.Add(entity);
            }
            stopwatch.Stop();
            Debug.LogError("使用ecs创建用时:" + stopwatch.Elapsed);

        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                monoList[i].AddComponent<TestMono>();
                monoList[i].AddComponent<TestMono1>();
            }
            stopwatch.Stop();
            Debug.LogError("使用mono添加用时:" + stopwatch.Elapsed);
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                ecsList[i].AddComponent(ComponentIds.POSITION).AddComponent(ComponentIds.ROATION);
            }
            stopwatch.Stop();
            Debug.LogError("使用ecs添加用时:" + stopwatch.Elapsed);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                DestroyImmediate(monoList[i].GetComponent<TestMono>());
                DestroyImmediate(monoList[i].GetComponent<TestMono1>());
            }
            stopwatch.Stop();
            Debug.LogError("使用mono删除用时:" + stopwatch.Elapsed);
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                ecsList[i].RemoveComponent(ComponentIds.POSITION).RemoveComponent(ComponentIds.ROATION);
            }
            stopwatch.Stop();
            Debug.LogError("使用ecs删除用时:" + stopwatch.Elapsed);
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
class TestMono1 : MonoBehaviour
{
    //public Vector3 pos;
    public Quaternion quta;
}