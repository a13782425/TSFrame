using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed partial class Observer : MonoBehaviour
{
    /// <summary>
    /// rootName只会访问prefab下一层的物体
    /// 如果rootName没有找到则返回自身
    /// </summary>
    /// <param name="prefabName"></param>
    /// <param name="rootName"></param>
    /// <param name="instantName"></param>
    /// <returns></returns>
    public Observer UILoadRootUI(string prefabName, string rootName = "root", string instantName = null)
    {
        CreateEntity()
            .SetChangeComponent(MatchEntity)
            .AddComponent(ComponentIds.INSTANTIATE);
        //CreateEntity()
        //    .SetChangeComponent(MatchEntity)
        //    .AddComponent(new InstantiateComponent() { Parent = this._uiGameObject.transform, PrefabName = prefabName, InstabtiateName = instantName })
        //    .AddComponent(new StringComponent() { Value = rootName })
        //    .AddComponent(new GameObjectComponent())
        //    .AddComponent(new AdditiveComponent() { AdditiveComponentIds = new List<Int64>() { ComponentIds.FIND_GAMEOBJECT } });
        return this;
    }

    public Observer UILoadRootUI(RenderMode renderMode = RenderMode.ScreenSpaceOverlay, Camera cam = null, string instantName = null)
    {
        if (string.IsNullOrEmpty(instantName))
        {
            instantName = UI_ROOT_DEFAULT_NAME;
        }
        return this;
    }


    #region Partial Method

    partial void UILoad()
    {
        _uiGameObject = new GameObject("UIObserver");
        _uiGameObject.transform.SetParent(this.transform);
    }
    partial void UIUpdate()
    {

    }

    #endregion
}
