using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSystem : IReactiveSystem
{

    public ComponentFlag ReactiveCondition
    {
        get
        {
            return Observer.Instance.GetFlag(ComponentIds.VIEW, ComponentIds.GAME_OBJECT);
        }
    }

    public void Execute(List<Entity> entitys)
    {
        foreach (var item in entitys)
        {
            string prefabName = item.GetValue<string>(ComponentIds.VIEW, "prefabname");
            GameObject obj = Observer.Instance.ResourcesLoad(prefabName) as GameObject;
            if (obj != null)
            {
                GameObject instant = GameObject.Instantiate<GameObject>(obj);
                instant.transform.SetParent(item.GetValue<Transform>("parent"));
                instant.transform.position = item.GetValue<Vector3>("pos");
                instant.transform.rotation = item.GetValue<Quaternion>("rot");
                instant.hideFlags = item.GetValue<HideFlags>("hideflags");
                item.SetValue(ComponentIds.GAME_OBJECT, "value", instant);
            }
            else
            {
                Debug.LogError("实例化游戏物体失败！！！");
            }

        }
    }
}
