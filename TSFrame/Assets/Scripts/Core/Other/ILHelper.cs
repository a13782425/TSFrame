using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
public delegate void PropertySetter(Entity entity, object instance, object value);
public delegate object PropertyGetter(object instance);
/// <summary>
/// 框架中component属性
/// </summary>
public class TSProperty
{
    public string Name { get; set; }
    public PropertySetter Setter { get; set; }
    public PropertyGetter Getter { get; set; }
}
/// <summary>
/// 生成方法，仅限ecs中component属性
/// </summary>
public static class ILHelper
{
    private static MethodInfo callBackMethod = null;

    private static Type _interfaceType = typeof(IReactiveComponent);
    private static Type _dataDrivenType = typeof(DataDrivenAttribute);

    public static void SetChangeCallBack(string methodName = "")
    {
        callBackMethod = typeof(Observer).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
    }

    public static Dictionary<string, TSProperty> RegisteComponent(IComponent instance)
    {
        Dictionary<string, TSProperty> tempReturnDic = new Dictionary<string, TSProperty>();
        Type instanceType = instance.GetType();
        bool isNeedReactive = false;
        if (_interfaceType.IsAssignableFrom(instanceType))
        {
            isNeedReactive = true;
        }
        #region Property

        PropertyInfo[] props = instanceType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
        if (props != null && props.Length > 0)
        {
            for (int i = 0; i < props.Length; i++)
            {
                bool isDataDriven = false;
                PropertyInfo property = props[i];
                object[] objs = property.GetCustomAttributes(typeof(DataDrivenAttribute), false);
                //DataDrivenAttribute 特性长度大于0 则这个属性需要数据驱动，否则则不需要数据驱动
                if (objs.Length > 0)
                {
                    isDataDriven = isNeedReactive;
                }
                TSProperty tsProperty = CreateProperty(instance, property, isDataDriven);
                tempReturnDic.Add(property.Name.ToLower(), tsProperty);
            }
        }

        #endregion

        return tempReturnDic;
    }

    public static TSProperty CreateProperty(object instance, PropertyInfo property, bool isDataDriven = false)
    {
        TSProperty tsProperty = new TSProperty();
        tsProperty.Name = property.Name;
        tsProperty.Setter = CreateSetter(property, isDataDriven);
        tsProperty.Getter = CreateGetter(property);
        return tsProperty;
    }

    private static PropertyGetter CreateGetter(PropertyInfo property)
    {
        Type type = property.DeclaringType;

        var dm = new DynamicMethod("", typeof(object), new[] { typeof(object) }, type);
        //var dm = new DynamicMethod("", typeof(object), null, type);
        var il = dm.GetILGenerator();
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Callvirt, property.GetGetMethod());
        il.Emit(OpCodes.Ret);
        return (PropertyGetter)dm.CreateDelegate(typeof(PropertyGetter));
    }

    private static PropertySetter CreateSetter(PropertyInfo property, bool isDataDriven = false)
    {
        Type type = property.DeclaringType;
        DynamicMethod dm = new DynamicMethod("", null, new[] { typeof(Entity), typeof(object), typeof(object) }, type);
        //DynamicMethod dm = new DynamicMethod("", null, new[] { typeof(object) }, type);
        //=== IL ===
        ILGenerator il = dm.GetILGenerator();
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Ldarg_1);
        if (property.PropertyType.IsValueType)//判断属性类型是否是值类型
        {
            il.Emit(OpCodes.Unbox, property.PropertyType);//如果是值类型就拆箱
        }
        else
        {
            il.Emit(OpCodes.Castclass, property.PropertyType);//否则强转
        }
        il.Emit(OpCodes.Callvirt, property.GetSetMethod());
        il.Emit(OpCodes.Ret);
        //=== IL ===
        return (PropertySetter)dm.CreateDelegate(typeof(PropertySetter));
    }
}


//public delegate void PropertySetter(object instance, object value);
//public delegate object PropertyGetter(object instance);
///// <summary>
///// 框架中component属性
///// </summary>
//public class TSProperty
//{
//    public PropertyInfo Info { get; set; }
//    public PropertySetter Setter { get; set; }
//    public PropertyGetter Getter { get; set; }
//}

///// <summary>
///// 生成方法，仅限ecs中component属性
///// </summary>
//public static class ILHelper
//{
//    public static Dictionary<string, TSProperty> RegistPropertys(object instance)
//    {
//        Dictionary<string, TSProperty> tempReturnDic = new Dictionary<string, TSProperty>();
//        Type instanceType = instance.GetType();
//        PropertyInfo[] props = instanceType.GetProperties();
//        if (props != null && props.Length > 0)
//        {
//            for (int i = 0; i < props.Length; i++)
//            {
//                PropertyInfo property = props[i];
//                object[] objs = property.GetCustomAttributes(typeof(DataDrivenAttribute), false);
//                //DataDrivenAttribute 特性长度大于0 则这个属性需要数据驱动，否则则不需要数据驱动
//                if (objs.Length > 0)
//                {
//                    //存在
//                    TSProperty tsProperty = CreateProperty(instance, property);
//                    tempReturnDic.Add(property.Name.ToLower(), tsProperty);
//                }
//            }
//        }
//        return tempReturnDic;
//    }

//    public static TSProperty CreateProperty(object instance, PropertyInfo property)
//    {
//        TSProperty tsProperty = new TSProperty();
//        tsProperty.Info = property;
//        tsProperty.Setter = CreateSetter(property);
//        tsProperty.Getter = CreateGetter(property);
//        return tsProperty;
//    }

//    private static PropertyGetter CreateGetter(PropertyInfo property)
//    {
//        Type type = property.DeclaringType;

//        var dm = new DynamicMethod("", typeof(object), new[] { typeof(object) }, type);
//        //var dm = new DynamicMethod("", typeof(object), null, type);
//        var il = dm.GetILGenerator();
//        il.Emit(OpCodes.Ldarg_0);
//        il.Emit(OpCodes.Callvirt, property.GetGetMethod());
//        il.Emit(OpCodes.Ret);
//        return (PropertyGetter)dm.CreateDelegate(typeof(PropertyGetter));
//    }

//    private static PropertySetter CreateSetter(PropertyInfo property)
//    {
//        Type type = property.DeclaringType;
//        DynamicMethod dm = new DynamicMethod("", null, new[] { typeof(object), typeof(object) }, type);
//        //DynamicMethod dm = new DynamicMethod("", null, new[] { typeof(object) }, type);
//        //=== IL ===
//        ILGenerator il = dm.GetILGenerator();
//        il.Emit(OpCodes.Ldarg_0);
//        il.Emit(OpCodes.Ldarg_1);
//        if (property.PropertyType.IsValueType)//判断属性类型是否是值类型
//        {
//            il.Emit(OpCodes.Unbox, property.PropertyType);//如果是值类型就拆箱
//        }
//        else
//        {
//            il.Emit(OpCodes.Castclass, property.PropertyType);//否则强转
//        }
//        il.Emit(OpCodes.Callvirt, property.GetSetMethod());
//        il.Emit(OpCodes.Ret);
//        //=== IL ===
//        return (PropertySetter)dm.CreateDelegate(typeof(PropertySetter));
//    }
//}

