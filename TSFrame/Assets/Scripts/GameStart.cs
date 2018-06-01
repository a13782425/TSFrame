using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    Thread th1;
    Thread th2;
    Thread th3;
    Queue<int> q1;
    Queue<int> q2;
    int num = 0;
    int getCount = 0;
    private void Awake()
    {
        //Observer.Instance.SetIsTest(true);
    }
    private void Start()
    {
        //ComponentFlag f1 = new ComponentFlag();
        //f1.SetFlag(1);
        //f1.SetFlag(2);
        //f1.SetFlag(ComponentIds.Test1);
        //ComponentFlag f2 = new ComponentFlag();
        //f2.SetFlag(1);
        //f2.SetFlag(4);
        //f2.SetFlag(ComponentIds.Test1);
        //ComponentFlag f3 = new ComponentFlag();
        //f3.SetFlag(1);
        //f3.SetFlag(ComponentIds.Test1);
        //ComponentFlag f4 = new ComponentFlag();
        //f4.SetFlag(1);
        //f4.SetFlag(2);
        //f4.SetFlag(ComponentIds.Test2);

        //Debug.LogError(f1.GetHashCode());
        //Debug.LogError(f2.GetHashCode());
        //Debug.LogError(f3.GetHashCode());
        //Debug.LogError(f4.GetHashCode());

        //Dictionary<ComponentFlag, int> dic = new Dictionary<ComponentFlag, int>();
        //dic.Add(f1,1);
        //dic.Add(f2,2);
        //dic.Add(f3,3);
        //dic.Add(f4, 4);

        //th = new Thread(new ThreadStart(Test));
        //th.IsBackground = true;
        //th.Start();
        //Debug.LogError(th.ThreadState.ToString());

        //Observer.Instance
        //    .SetCheckTime(5)
        //    .SetResourcesTime(180)
        //    .SetIsTest(true)
        //    .AddSystem(new InstantiateSystem());
        //q1 = new Queue<int>();
        //q1.Dequeue();
        //th1 = new Thread(new ThreadStart(Test1));
        //th1.IsBackground = true;
        //th1.Start();
        //th2 = new Thread(new ThreadStart(Test2));
        //th2.IsBackground = true;
        //th2.Start();
        //th3 = new Thread(new ThreadStart(Test3));
        //th3.IsBackground = true;
        //th3.Start();

        //TestComponent test = new TestComponent();
        //test["value"] = "10";
        //Debug.LogError(test["value"]);

        //Entity entity = new Entity();
        //entity.SetChangeComponent(Test123)
        //    .AddComponent(ComponentIds.TEST)
        //    .SetValueChange(Test1231)
        //    .AddComponent(ComponentIds.STRING)
        //    .SetValue("value", "dasdas")
        //    .SetValue(ComponentIds.TEST,"value","poiy")
        //    .SetValue("test1", "qweqwe");
        //Debug.LogError(entity.GetValue<string>("value"));
        //entity.RemoveComponent(ComponentIds.TEST).SetValue("VALUE", "2EWA");
        Entity entity = Observer.Instance.SetIsTest(true)
            .Run()
            .CreateEntity()
            .AddComponent(ComponentIds.TEST)
            .SetValue("value", "坑爹")
            .SetValue("test1", "component")
            .AddComponent(ComponentIds.STRING)
            .SetValue("value", "string测试");
        Debug.LogError(entity.GetValue<string>("value"));
        
    }

    private void Test123(Entity entity)
    {
        Debug.LogError(entity.GetId());
    }

    private void Test1231(Entity entity, Int64 id)
    {
        Debug.LogError(id);
    }
    private void OnApplicationPause(bool pause)
    {
        //th1.Abort();

        //th2.Abort();

        //th3.Abort();
    }
    private void OnApplicationQuit()
    {
        //th1.Abort();

        //th2.Abort();

        //th3.Abort();
    }
    private void Test1()
    {
        //while (true)
        {
            for (int i = 0; i < 1000; i++)
            {
                q1.Enqueue(i);
            }
        }
    }
    private void Test2()
    {
        while (true)
        {
            if (q1.Count > 0)
            {
                int value = q1.Dequeue();
                if (getCount != value)
                {
                    Debug.LogError("Error" + value + "=========>GetCount:" + getCount);
                }
                else
                {
                    Debug.LogError("OK" + value + "=========>GetCount:" + getCount);
                }
                getCount++;
            }
        }
    }
    private void Test3()
    {
        while (true)
        {
            Debug.LogError(getCount);
            Thread.Sleep(1000);

        }
    }
}