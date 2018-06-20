using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class Utils
{
    private static int _entityId = -1;

    private static int  _sharedId = -1;

    /// <summary>
    /// 获取实体Id
    /// </summary>
    /// <returns></returns>
    public static int GetEntityId()
    {
        //Interlocked.Increment(ref _entityId);
        _entityId++;
        return _entityId;
    }
    /// <summary>
    /// 获取共享Id
    /// </summary>
    /// <returns></returns>
    public static int GetSharedId()
    {
        //Interlocked.Increment(ref _sharedId);
        _sharedId++;
        return _sharedId;
    }
}

