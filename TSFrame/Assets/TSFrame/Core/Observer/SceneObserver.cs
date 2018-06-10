using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed partial class Observer
{

    //private string _currentSceneName;

    //private string _nextSceneName;

    //private Action _beforCallBack;
    //private Action _afterCallBack;
    //private IEnumerator _beforAsyncCallBack;
    //private IEnumerator _afterAsyncCallBack;

    #region PublicMethod

    //public Observer SetBeforLoadCallBack(Action callback)
    //{
    //    _beforCallBack = callback;
    //    return this;
    //}

    //public Observer SetBeforLoadCallBack(IEnumerator callback)
    //{
    //    _beforAsyncCallBack = callback;
    //    return this;
    //}

    //public Observer SetAfterLoadCallBack(Action callback)
    //{
    //    _afterCallBack = callback;
    //    return this;
    //}

    //public Observer SetAfterLoadCallBack(IEnumerator callback)
    //{
    //    _afterAsyncCallBack = callback;
    //    return this;
    //}

    ///// <summary>
    ///// 加载场景
    ///// </summary>
    ///// <param name="sceneName">场景名称</param>
    ///// <param name="isAsync">是否异步</param>
    ///// <param name="noCheck">是否检查场景名称是否一致</param>
    ///// <returns></returns>
    //public Observer LoadScene(string sceneName, bool isAsync = true, bool noCheck = true)
    //{
    //    if (string.IsNullOrEmpty(sceneName))
    //    {
    //        return this;
    //    }
    //    if (noCheck)
    //    {
    //        if (sceneName == _currentSceneName)
    //        {
    //            return this;
    //        }
    //    }
    //    if (isAsync)
    //    {

    //    }
    //    else
    //    {
    //        SceneManager.LoadScene(sceneName);
    //    }

    //    return this;
    //}

    #endregion

    //private IEnumerator LoadSceneAsync(string sceneName)
    //{
    //    yield return null;
    //}

    //private void LoadScene(string sceneName)
    //{
    //    if (_beforCallBack != null)
    //    {
    //        _beforCallBack.Invoke();
    //    }
    //    SceneManager.LoadScene(sceneName);
    //    if (_afterCallBack != null)
    //    {
    //        _afterCallBack.Invoke();
    //    }
    //}

    partial void SceneLoad()
    {
        _sceneGameObject = new GameObject("SceneGameObject");
        _sceneGameObject.transform.SetParent(this.transform);
        //_currentSceneName = SceneManager.GetActiveScene().name;
    }

    partial void SceneUpdate()
    {

    }
}

