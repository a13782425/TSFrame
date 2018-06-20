using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Diagnostics;
public class TestOther : MonoBehaviour
{
    HashSet<int> test1 = new HashSet<int>();
    HashSet<int> test2 = new HashSet<int>();
    ComponentFlag flag1 = new ComponentFlag();
    ComponentFlag flag2 = new ComponentFlag();
    Stopwatch stopwatch = new Stopwatch();
    [SerializeField]
    private int Count = 1000;
    // Use this for initialization
    void Start()
    {
        test1.Add(1);
        test1.Add(5);
        test1.Add(9);
        test1.Add(15);
        test1.Add(30);
        test1.Add(6);
        test1.Add(20);
        test1.Add(60);
        test2.Add(5);
        test2.Add(30);
        test2.Add(20);
        flag1.SetFlag(OperatorIds.ACTIVE).SetFlag(OperatorIds.ADDITIVE).SetFlag(OperatorIds.COLLISION).SetFlag(OperatorIds.COLLISION2D).SetFlag(OperatorIds.GAME_OBJECT).SetFlag(OperatorIds.GAME_OBJECT_NAME).SetFlag(OperatorIds.HAS_PHYSICAL).SetFlag(OperatorIds.INPUT);
        flag2.SetFlag(OperatorIds.ACTIVE).SetFlag(OperatorIds.COLLISION2D).SetFlag(OperatorIds.GAME_OBJECT_NAME);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            stopwatch.Reset();
            stopwatch.Start();
            bool res = false;
            for (int i = 0; i < Count; i++)
            {
                res = flag1.HasFlag(flag2);
            }
            stopwatch.Stop();
            Debug.LogError("使用位运算检测是否包含" + Count + "次用时:" + stopwatch.Elapsed + "=====>" + res);
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < Count; i++)
            {
                res = test2.IsSubsetOf(test1);
            }
            stopwatch.Stop();
            Debug.LogError("使用HashSet检测是否包含" + Count + "次用时:" + stopwatch.Elapsed + "=====>" + res);
        }
    }
}
