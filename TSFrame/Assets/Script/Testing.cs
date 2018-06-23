using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Diagnostics;
using System.Linq;
using System;

public class Testing : MonoBehaviour
{
    private List<GameObject> monoList = new List<GameObject>();
    private List<Entity> ecsList = new List<Entity>();

    private GameObject res;
    Stopwatch stopwatch = new Stopwatch();

    private GameObject monoTest;
    private Entity entityTest;
    [SerializeField]
    private int Count = 1000;
    [SerializeField]
    private bool IsUseThread = false;
    void Start()
    {
        //初始化观察者
        Observer.Instance
            .SetIsTest(true)//设置是否是测试版
            .SetResourcesTime(100)
            .SetUseThread(IsUseThread)
            .GameLaunch();//启动观察者
        res = Resources.Load<GameObject>("Test");
    }
    string str = "初始化";
    private bool isAdd = false;
    private bool isCreate = false;
    //void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.A))
    //    {
    //        DateTime begin = DateTime.Now;
    //        //stopwatch.Reset();
    //        //stopwatch.Start();
    //        for (int i = 0; i < Count; i++)
    //        {
    //            GameObject instant = GameObject.Instantiate<GameObject>(res);
    //            monoList.Add(instant);
    //        }
    //        Debug.LogError("使用mono创建" + Count + "个用时:" + (DateTime.Now - begin).TotalMilliseconds);
    //        begin = DateTime.Now;
    //        //stopwatch.Stop();
    //        //Debug.LogError("使用mono创建" + Count + "个用时:" + stopwatch.Elapsed);
    //        //stopwatch.Reset();
    //        //stopwatch.Start();
    //        for (int i = 0; i < Count; i++)
    //        {
    //            Entity entity = Observer.Instance.CreateEntity().AddComponent(ComponentIds.VIEW).AddComponent(ComponentIds.GAME_OBJECT).SetValue(ViewComponentVariable.prefabName, "Test");
    //            ecsList.Add(entity);
    //        }
    //        Debug.LogError("使用ecs创建" + Count + "个用时:" + (DateTime.Now - begin).TotalMilliseconds);
    //    }
    //    if (Input.GetKeyUp(KeyCode.D))
    //    {
    //        stopwatch.Reset();
    //        stopwatch.Start();
    //        for (int i = 0; i < Count; i++)
    //        {
    //            GameObject.Destroy(monoList[i]);
    //        }
    //        monoList.Clear();
    //        stopwatch.Stop();
    //        Debug.LogError("使用mono销毁" + Count + "个用时:" + stopwatch.Elapsed);
    //        stopwatch.Reset();
    //        stopwatch.Start();
    //        for (int i = 0; i < Count; i++)
    //        {
    //            ecsList[i].SetValue(LifeCycleComponentVariable.lifeCycle, LifeCycleEnum.Destory);
    //        }
    //        ecsList.Clear();
    //        stopwatch.Stop();
    //        Debug.LogError("使用ecs销毁" + Count + "个用时:" + stopwatch.Elapsed);
    //    }
    //}
    void OnGUI()
    {
        GUILayout.Label("初始化个数:" + Count);
        str = monoList.Count > 0 ? "复原" : "初始化";
        if (GUILayout.Button("创建个数"))
        {
            Debug.LogError("游戏物体数量：" + GameObject.FindGameObjectsWithTag("Player").Length);
        }
        if (GUILayout.Button(isCreate ? "复原" : "初始化"))
        {
            if (isCreate)
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < Count; i++)
                {
                    GameObject.Destroy(monoList[i]);
                }
                monoList.Clear();
                stopwatch.Stop();
                Debug.LogError("使用mono销毁" + Count + "个用时:" + stopwatch.Elapsed);
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < Count; i++)
                {
                    ecsList[i].SetValue(LifeCycleComponentVariable.lifeCycle, LifeCycleEnum.Destory);
                }
                ecsList.Clear();
                stopwatch.Stop();
                Debug.LogError("使用ecs销毁" + Count + "个用时:" + stopwatch.Elapsed);
                GameObject.Destroy(monoTest);
                entityTest.SetValue(LifeCycleComponentVariable.lifeCycle, LifeCycleEnum.Destory);
            }
            else
            {
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < Count; i++)
                {
                    GameObject instant = GameObject.Instantiate<GameObject>(res);
                    monoList.Add(instant);
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
            isCreate = !isCreate;
        }
        if (monoList.Count > 0)
        {
            if (GUILayout.Button(isAdd ? "删除Component" : "添加Componnet"))
            {
                if (isAdd)
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
                        ecsList[i].RemoveComponent(ComponentIds.POSITION).RemoveComponent(ComponentIds.ROATION);
                    }
                    stopwatch.Stop();
                    Debug.LogError("使用ecs删除" + Count + "个组件用时:" + stopwatch.Elapsed);
                }
                else
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
                        ecsList[i].AddComponent(ComponentIds.POSITION).AddComponent(ComponentIds.ROATION);
                    }
                    stopwatch.Stop();
                    Debug.LogError("使用ecs添加" + Count + "个组件用时:" + stopwatch.Elapsed);
                }
                isAdd = !isAdd;
            }
        }
        if (GUILayout.Button("同时添加删除Component"))
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
                entityTest.AddComponent(ComponentIds.POSITION).AddComponent(ComponentIds.ROATION);
                entityTest.RemoveComponent(ComponentIds.POSITION).RemoveComponent(ComponentIds.ROATION);
            }
            stopwatch.Stop();
            Debug.LogError("使用ecs添加删除" + Count + "个组件用时:" + stopwatch.Elapsed);
        }
    }
    // Update is called once per frame

}


class TestMono : MonoBehaviour
{
    public Vector3 pos;
    public Quaternion quta;
}