using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class Utils
{
    private static int _id = 0;

    private static int  _sharedId = 0;

    /// <summary>
    /// 获取唯一Id
    /// </summary>
    /// <returns></returns>
    public static int GetId()
    {
        Interlocked.Increment(ref _id);
        return _id;
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

