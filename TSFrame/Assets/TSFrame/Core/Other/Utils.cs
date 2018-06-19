using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class Utils
{
    private static int _entityId = 0;

    private static int _componentId = 0;

    private static int  _sharedId = 0;

    /// <summary>
    /// 获取实体Id
    /// </summary>
    /// <returns></returns>
    public static int GetEntityId()
    {
        Interlocked.Increment(ref _entityId);
        return _entityId;
    }
    /// <summary>
    /// 获取组件Id
    /// </summary>
    /// <returns></returns>
    public static int GetComponentId()
    {
        Interlocked.Increment(ref _componentId);
        return _componentId;
    }

    /// <summary>
    /// 获取共享Id
    /// </summary>
    /// <returns></returns>
    public static int GetSharedId()
    {
        Interlocked.Increment(ref _sharedId);
        return _sharedId;
    }
}

