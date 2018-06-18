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


class TestMono : MonoBehaviour
{
    public Vector3 pos;
    public Quaternion quta;
}