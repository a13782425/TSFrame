using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed partial class Observer : MonoBehaviour
{
    #region Public

    public Observer SetCheckTime(int time)
    {
        this._checkTime = time;
        return this;
    }

    public Observer SetIsTest(bool isTest)
    {
        this._isTest = isTest;
        this.gameObject.hideFlags = this._isTest ? HideFlags.None : HideFlags.HideInHierarchy;
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
