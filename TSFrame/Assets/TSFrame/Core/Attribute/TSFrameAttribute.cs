using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 数据驱动特性
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class DataDrivenAttribute : Attribute
{

}
/// <summary>
/// 不要拷贝特性
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class DontCopyAttribute : Attribute
{

}