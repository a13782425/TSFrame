using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class Utils
{
    private static int id = 0;
    /// <summary>
    /// 获取唯一ID
    /// </summary>
    /// <returns></returns>
    public static int GetId()
    {
        Interlocked.Increment(ref id);
        return id;
    }
}

