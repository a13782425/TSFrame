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
    partial void OnDestroy();

    #endregion

    #region GameObserver Method

    partial void GameLoad();
    partial void GameUpdate();

    partial void GameOneStep();

    #endregion

    #region UIObserver Method

    partial void UILoad();
    partial void UIUpdate();

    #endregion

    #region VariableObserver Method

    partial void VariableLoad();
    partial void VariableUpdate();
    partial void CreateComponentPool();

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
    partial void MatchEntity(Entity entity, NormalComponent component);
    partial void MatchEntity(Entity entity, bool isActive);

    partial void DataDrivenMethod(Entity entity, Int64 componentId);

    #endregion

    #region ResourcesObserver Method

    partial void ResourcesLoad();
    partial void ResourcesUpdate();

    #endregion

    #region SystemObserver Method

    partial void SystemLoad();
    partial void SystemUpdate();
    partial void ReactiveSysyemRun();

    #endregion

    #region EntityObserver Method

    partial void EntityLoad();
    partial void EntityUpdate();

    #endregion

    #region PoolObserver Method

    partial void PoolLoad();
    partial void PoolUpdate();
    partial void RecoverComponent(NormalComponent component);

    #endregion

    #region SceneObserver Method

    partial void SceneLoad();

    partial void SceneUpdate();

    #endregion

    #endregion

    #region Implement Method
    partial void Awake()
    {
    }
    partial void Start()
    {

    }

    partial void OnDestroy()
    {
        _isPlaying = false;
    }

    partial void Update()
    {
        if (!_isRun || _pause)
        {
            return;
        }
        GameOneStep();
    }

    #endregion

}
