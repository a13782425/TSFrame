using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed partial class Observer
{
    #region Public

    /// <summary>
    /// 是否是测试
    /// </summary>
    /// <param name="isTest"></param>
    /// <returns></returns>
    public Observer SetIsTest(bool isTest)
    {
        this._isTest = isTest;
        this.gameObject.hideFlags = this._isTest ? HideFlags.None : HideFlags.HideInHierarchy;
        return this;
    }
    /// <summary>
    /// 继续
    /// </summary>
    /// <returns></returns>
    public Observer Continue()
    {
        if (_pause)
        {
            _pause = false;
            Time.timeScale = 1;
        }
        return this;
    }
    /// <summary>
    /// 暂停
    /// </summary>
    /// <returns></returns>
    public Observer Pause()
    {
        if (!_pause)
        {
            _pause = true;
            Time.timeScale = 0;
        }
        return this;
    }

    /// <summary>
    /// 执行一帧
    /// </summary>
    /// <returns></returns>
    public Observer GameOneStep()
    {
        if (_isRun && _pause)
            SystemOneStep();
        return this;
    }
    /// <summary>
    /// 设置fps
    /// </summary>
    /// <param name="fps"></param>
    /// <returns></returns>
    public Observer SetFPS(int fps)
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = fps;
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
