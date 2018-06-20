using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Object = UnityEngine.Object;

public sealed partial class Observer
{

    #region Public Method
    /// <summary>
    /// 设置资源回收时间
    /// </summary>
    /// <param name="time">秒</param>
    /// <returns></returns>
    public Observer SetResourcesTime(int second = 180)
    {
        this._resourcesTime = second;
        return this;
    }
    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="isRecycle">是否自动回收</param>
    /// <returns></returns>
    public Object ResourcesLoad(string path, bool isRecycle = true)
    {
        ResourcesDto dto;
        if (_resourcesDtoDic.ContainsKey(path))
        {
            dto = _resourcesDtoDic[path];
        }
        else
        {
            Object obj = Resources.Load(path);
            if (obj != null)
            {
                dto = new ResourcesDto(path, obj, isRecycle);
                _resourcesDtoDic.Add(path, dto);
                goto End;
            }
            return null;
        }
        End: dto.LastUseTime = Time.time;
        return dto.CacheObj;
    }

    /// <summary>
    /// 资源回收(立即回收,)
    /// </summary>
    /// <param name="isForceCollect">包含不检测回收的对象</param>
    /// <returns></returns>
    public Observer ResourcesCollect(bool isForceCollect = false)
    {
        foreach (KeyValuePair<string, ResourcesDto> item in _resourcesDtoDic)
        {
            if (isForceCollect)
            {
                item.Value.IsCanRecycle = true;
            }
            else
            {
                if (item.Value.IsAutoRecycle)
                {
                    item.Value.IsCanRecycle = true;
                }
            }
        }

        return this;
    }

    #endregion

    partial void ResourcesLoad()
    {
        _resourcesGameObject = new GameObject("ResourcesGameObject");
        _resourcesGameObject.transform.SetParent(this.transform);

    }

    partial void ResourcesUpdate()
    {
        foreach (KeyValuePair<string, ResourcesDto> item in _resourcesDtoDic)
        {
            if (item.Value.IsAutoRecycle)
            {
                if (Time.time - item.Value.LastUseTime > this._resourcesTime)
                {
                    item.Value.IsCanRecycle = true;
                }
            }
        }
        List<ResourcesDto> tempList = _resourcesDtoDic.Values.Where(a => a.IsCanRecycle).ToList();
        if (tempList.Count > 0)
        {
            for (int i = 0; i < tempList.Count; i++)
            {
                Debug.LogError(_resourcesDtoDic.Count);
                Debug.LogError(tempList[i].PathName);
                _resourcesDtoDic.Remove(tempList[i].PathName);
                tempList[i].Release();
            }
            tempList = null;
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }
    }
}

