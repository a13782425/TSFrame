using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 观察者
/// </summary>
public sealed partial class Observer : MonoBehaviour
{

    #region Partial Method

    #region This Method
    partial void Awake();
    partial void Start();
    partial void Update();

    #endregion

    #region GameObserver Method

    partial void GameLoad();
    partial void GameUpdate();

    #endregion

    #region UIObserver Method

    partial void UILoad();
    partial void UIUpdate();

    #endregion

    #region VariableObserver Method

    partial void VariableLoad();
    partial void VariableUpdate();

    #endregion

    #region NetObserver Method

    partial void NetLoad();
    partial void NetUpdate();

    #endregion

    #region CameraObserver Method

    partial void CameraLoad();
    partial void CameraUpdate();

    #endregion

    #region MatchObserver Method

    partial void MatchLoad();
    partial void MatchUpdate();
    partial void MatchEntity(Entity entity);

    #endregion

    #region ResourcesObserver Method

    partial void ResourcesLoad();
    partial void ResourcesUpdate();

    #endregion

    #region SystemObserver Method

    partial void SystemLoad();
    partial void SystemUpdate();

    #endregion

    #region EntityObserver Method

    partial void EntityLoad();
    partial void EntityUpdate();

    #endregion

    #endregion

    #region Implement Method
    partial void Awake()
    {
    }
    partial void Start()
    {

    }

    partial void Update()
    {
        if (!_isRun)
        {
            return;
        }
        VariableUpdate();
        ResourcesUpdate();
        CameraUpdate();
        MatchUpdate();
        GameUpdate();
        UIUpdate();
        NetUpdate();
        SystemUpdate();
        EntityUpdate();
    }

    #endregion

}
