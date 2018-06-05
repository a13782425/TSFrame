using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed partial class Observer
{
    #region Public

    public Observer SetIsTest(bool isTest)
    {
        this._isTest = isTest;
        this.gameObject.hideFlags = this._isTest ? HideFlags.None : HideFlags.HideInHierarchy;
        return this;
    }
    /// <summary>
    /// 启动观察着
    /// </summary>
    /// <returns></returns>
    public Observer Run()
    {
        ILHelper.SetChangeCallBack("Call");
        VariableLoad();
        ResourcesLoad();
        CameraLoad();
        MatchLoad();
        GameLoad();
        UILoad();
        NetLoad();
        SystemLoad();
        EntityLoad();
        _isRun = true;
        return this;
    }


    #endregion
    partial void GameLoad()
    {
        _gameGameObject = new GameObject("GameGameObject");
        _gameGameObject.transform.SetParent(this.transform);
    }

    partial void GameUpdate()
    {
    }
}
