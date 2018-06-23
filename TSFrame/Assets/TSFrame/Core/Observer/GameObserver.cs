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
    public Observer OneStep()
    {
        if (_isRun && _pause)
            GameOneStep();
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
    /// 是否使用线程
    /// </summary>
    /// <param name="isUse"></param>
    /// <returns></returns>
    public Observer SetUseThread(bool isUse)
    {
        _isUseThread = isUse;
        return this;
    }

    /// <summary>
    /// 游戏启动
    /// </summary>
    /// <returns></returns>
    public Observer GameLaunch()
    {
        VariableLoad();
        ResourcesLoad();
        CameraLoad();
        MatchLoad();
        GameLoad();
        UILoad();
        NetLoad();
        SystemLoad();
        EntityLoad();
        PoolLoad();
        SceneLoad();
        _isRun = true;
        return this;
    }


    #endregion
    partial void GameLoad()
    {
        _gameGameObject = new GameObject("GameGameObject");
        _gameGameObject.transform.SetParent(this.transform);
        _isPlaying = true;
        TSThread.Instance.Run();
    }

    partial void GameUpdate()
    {
        _allTime = Time.time;
        _deltaTime = Time.deltaTime;
    }

    partial void GameOneStep()
    {
        VariableUpdate();
        ResourcesUpdate();
        CameraUpdate();
        MatchUpdate();
        GameUpdate();
        UIUpdate();
        NetUpdate();
        SystemUpdate();
        EntityUpdate();
        PoolUpdate();
        SceneUpdate();
    }
}
