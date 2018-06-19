using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;



public delegate void PropertySetter(Entity entity, NormalComponent instance, object value);
public delegate object PropertyGetter(object instance);

/// <summary>
/// 框架中component属性
/// </summary>
public class TSProperty
{
    //public string Name { get; set; }
    //public Type PropertyType { get; set; }
    public PropertySetter Setter { get; set; }
    public PropertyGetter Getter { get; set; }
    public bool DontCopy { get; set; }
    public object DefaultValue { get; set; }
}
/// <summary>
/// 生成方法，仅限ecs中component属性
/// </summary>
public static class ILHelper
{
    private static string _methodName = "DataDrivenMethod";

    //private static Type _interfaceType = typeof(IReactiveComponent);

    //private static Type _dataDrivenType = typeof(DataDrivenAttribute);

    //private static Type _dontCopyType = typeof(DontCopyAttribute);

    private static Type _defaultValueType = typeof(DefaultValueAttribute);

    //private static Dictionary<Int64, Dictionary<string, TSProperty>> _ilCache = new Dictionary<Int64, Dictionary<string, TSProperty>>();
    private static Dictionary<Int64, TSProperty[]> _ilCache = new Dictionary<long, TSProperty[]>();

    public static TSProperty[] GetComponentProperty(Int64 componentId)
    {
        try
        {
            return _ilCache[componentId];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void RegisteComponent(IComponent instance)
    {
        try
        {

            Type instanceType = instance.GetType();
            Type varType = instanceType.Assembly.GetType(instanceType.Name + "Variable");
            if (varType == null)
            {
                throw new Exception(instanceType.Name + "没有组件说明类,请先生成组件说明类");
            }
            PropertyInfo countProperty = varType.GetProperty("Count", BindingFlags.Public | BindingFlags.Static);
            if (countProperty == null)
            {
                throw new Exception(instanceType.Name + "没有组件说明类,请先生成组件说明类");
            }
            int count = (int)countProperty.GetValue(null);
            if (count <= 0)
            {
                _ilCache.Add(instance.CurrentId, null);
                return;
            }
            TSProperty[] tempArray = new TSProperty[count];
            //Dictionary<string, TSProperty> tempReturnDic = new Dictionary<string, TSProperty>();
            #region Property

            PropertyInfo[] props = instanceType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (props != null && props.Length > 0)
            {
                for (int i = 0; i < props.Length; i++)
                {
                    //bool isDataDriven = false;
                    PropertyInfo property = props[i];
                    if (property.GetSetMethod() == null || property.GetGetMethod() == null)
                    {
                        continue;
                    }
                    FieldInfo varFieldInfo = varType.GetField(property.Name, BindingFlags.Public | BindingFlags.Static);
                    if (varFieldInfo == null)
                    {
                        continue;
                    }
                    ComponentValue componentValue = varFieldInfo.GetValue(null) as ComponentValue;
                    if (componentValue == null)
                    {
                        continue;
                    }
                    TSProperty tsProperty = CreateProperty(instance, property, componentValue.NeedReactive);
                    tsProperty.DontCopy = componentValue.DontCopy;
                    object[] objs = property.GetCustomAttributes(_defaultValueType, false);
                    if (objs.Length > 0)
                    {
                        tsProperty.DefaultValue = (objs[0] as DefaultValueAttribute).Value;
                    }
                    else
                    {
                        tsProperty.DefaultValue = property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null;
                    }
                    tempArray[componentValue.PropertyId] = tsProperty;
                    //tempReturnDic.Add(property.Name, tsProperty);
                }
            }

            #endregion

            #region FieldInfo

            FieldInfo[] fieldInfos = instanceType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (fieldInfos != null && fieldInfos.Length > 0)
            {
                for (int i = 0; i < fieldInfos.Length; i++)
                {
                    FieldInfo fieldInfo = fieldInfos[i];
                    FieldInfo varFieldInfo = varType.GetField(fieldInfo.Name, BindingFlags.Public | BindingFlags.Static);
                    if (varFieldInfo == null)
                    {
                        continue;
                    }
                    ComponentValue componentValue = varFieldInfo.GetValue(null) as ComponentValue;
                    if (componentValue == null)
                    {
                        continue;
                    }
                    TSProperty tsProperty = CreateProperty(instance, fieldInfo, componentValue.NeedReactive);
                    tsProperty.DontCopy = componentValue.DontCopy;
                    object[] objs = fieldInfo.GetCustomAttributes(_defaultValueType, false);
                    if (objs.Length > 0)
                    {
                        tsProperty.DefaultValue = (objs[0] as DefaultValueAttribute).Value;
                    }
                    else
                    {
                        tsProperty.DefaultValue = fieldInfo.FieldType.IsValueType ? Activator.CreateInstance(fieldInfo.FieldType) : null;
                    }
                    tempArray[componentValue.PropertyId] = tsProperty;
                }
            }

            #endregion
            _ilCache.Add(instance.CurrentId, tempArray);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //public static Dictionary<string, TSProperty> RegisteComponent(IComponent instance)
    //{
    //    try
    //    {
    //        if (_ilCache.ContainsKey(instance.CurrentId))
    //        {
    //            return _ilCache[instance.CurrentId];
    //        }
    //        Dictionary<string, TSProperty> tempReturnDic = new Dictionary<string, TSProperty>();
    //        Type instanceType = instance.GetType();
    //        bool isNeedReactive = false;
    //        if (_interfaceType.IsAssignableFrom(instanceType))
    //        {
    //            isNeedReactive = true;
    //        }

    //        #region Property

    //        PropertyInfo[] props = instanceType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
    //        if (props != null && props.Length > 0)
    //        {
    //            for (int i = 0; i < props.Length; i++)
    //            {
    //                bool isDataDriven = false;
    //                PropertyInfo property = props[i];
    //                if (property.GetSetMethod() == null || property.GetGetMethod() == null)
    //                {
    //                    continue;
    //                }
    //                object[] objs = property.GetCustomAttributes(_dataDrivenType, false);
    //                //DataDrivenAttribute 特性长度大于0 则这个属性需要数据驱动，否则则不需要数据驱动
    //                if (objs.Length > 0)
    //                {
    //                    isDataDriven = isNeedReactive;
    //                }
    //                TSProperty tsProperty = CreateProperty(instance, property, isDataDriven);
    //                tsProperty.DontCopy = property.GetCustomAttributes(_dontCopyType, false).Length > 0;
    //                objs = property.GetCustomAttributes(_defaultValueType, false);
    //                if (objs.Length > 0)
    //                {
    //                    tsProperty.DefaultValue = (objs[0] as DefaultValueAttribute).Value;
    //                }
    //                else
    //                {
    //                    tsProperty.DefaultValue = property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null;
    //                }
    //                tempReturnDic.Add(property.Name, tsProperty);
    //            }
    //        }

    //        #endregion

    //        #region FieldInfo

    //        FieldInfo[] fieldInfos = instanceType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
    //        if (fieldInfos != null && fieldInfos.Length > 0)
    //        {
    //            for (int i = 0; i < fieldInfos.Length; i++)
    //            {
    //                bool isDataDriven = false;
    //                FieldInfo fieldInfo = fieldInfos[i];
    //                object[] objs = fieldInfo.GetCustomAttributes(_dataDrivenType, false);
    //                //DataDrivenAttribute 特性长度大于0 则这个属性需要数据驱动，否则则不需要数据驱动
    //                if (objs.Length > 0)
    //                {
    //                    isDataDriven = isNeedReactive;
    //                }
    //                TSProperty tsProperty = CreateProperty(instance, fieldInfo, isDataDriven);
    //                tsProperty.DontCopy = fieldInfo.GetCustomAttributes(_dontCopyType, false).Length > 0;
    //                objs = fieldInfo.GetCustomAttributes(_defaultValueType, false);
    //                if (objs.Length > 0)
    //                {
    //                    tsProperty.DefaultValue = (objs[0] as DefaultValueAttribute).Value;
    //                }
    //                else
    //                {
    //                    tsProperty.DefaultValue = fieldInfo.FieldType.IsValueType ? Activator.CreateInstance(fieldInfo.FieldType) : null;
    //                }
    //                tempReturnDic.Add(fieldInfo.Name, tsProperty);
    //            }
    //        }

    //        #endregion
    //        _ilCache.Add(instance.CurrentId, tempReturnDic);
    //        return tempReturnDic;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private static TSProperty CreateProperty(object instance, FieldInfo fieldInfo, bool isDataDriven)
    {
        TSProperty tsProperty = new TSProperty();
        //tsProperty.Name = fieldInfo.Name;
        //tsProperty.PropertyType = fieldInfo.FieldType;
        tsProperty.Setter = CreateSetter(fieldInfo, isDataDriven);
        tsProperty.Getter = CreateGetter(fieldInfo);
        return tsProperty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="property"></param>
    /// <param name="isDataDriven">是否需要数据驱动</param>
    /// <returns></returns>
    public static TSProperty CreateProperty(object instance, PropertyInfo property, bool isDataDriven = false)
    {
        TSProperty tsProperty = new TSProperty();
        //tsProperty.Name = property.Name;
        //tsProperty.PropertyType = property.PropertyType;
        tsProperty.Setter = CreateSetter(property, isDataDriven);
        tsProperty.Getter = CreateGetter(property);
        return tsProperty;
    }

    private static PropertyGetter CreateGetter(PropertyInfo property)
    {
        Type type = property.DeclaringType;

        var dm = new DynamicMethod("", typeof(object), new[] { typeof(object) }, type);
        var il = dm.GetILGenerator();
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Callvirt, property.GetGetMethod());
        il.Emit(OpCodes.Box, property.PropertyType);
        il.Emit(OpCodes.Ret);
        return (PropertyGetter)dm.CreateDelegate(typeof(PropertyGetter));
    }

    private static PropertyGetter CreateGetter(FieldInfo fieldInfo)
    {
        Type type = fieldInfo.DeclaringType;

        var dm = new DynamicMethod("", typeof(object), new[] { typeof(object) }, type);
        var il = dm.GetILGenerator();
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Ldfld, fieldInfo);
        il.Emit(OpCodes.Box, fieldInfo.FieldType);
        il.Emit(OpCodes.Ret);
        return (PropertyGetter)dm.CreateDelegate(typeof(PropertyGetter));
    }

    private static PropertySetter CreateSetter(PropertyInfo property, bool isDataDriven = false)
    {
        Type type = property.DeclaringType;
        var dm = new DynamicMethod("", null, new Type[] { typeof(Entity), typeof(NormalComponent), typeof(object) }, type);
        //2.声明il编译器
        var il = dm.GetILGenerator();
        il.Emit(OpCodes.Nop);
        il.Emit(OpCodes.Ldarg_1);
        il.Emit(OpCodes.Callvirt, typeof(NormalComponent).GetProperty("CurrentComponent").GetGetMethod());
        il.Emit(OpCodes.Ldarg_2);
        if (property.PropertyType.IsValueType)//判断属性类型是否是值类型
        {
            il.Emit(OpCodes.Unbox_Any, property.PropertyType);//如果是值类型就拆箱
        }
        else
        {
            il.Emit(OpCodes.Castclass, property.PropertyType);//否则强转
        }
        il.Emit(OpCodes.Callvirt, property.GetSetMethod());
        il.Emit(OpCodes.Nop);
        if (isDataDriven && !string.IsNullOrEmpty(_methodName))
        {
            il.Emit(OpCodes.Call, typeof(Observer).GetProperty("Instance").GetGetMethod());
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Callvirt, typeof(Observer).GetMethod(_methodName));
            il.Emit(OpCodes.Nop);
        }
        il.Emit(OpCodes.Ret);
        //5.由il编译器创建指定类型的动态方法委托
        return (PropertySetter)dm.CreateDelegate(typeof(PropertySetter));

    }

    private static PropertySetter CreateSetter(FieldInfo fieldInfo, bool isDataDriven = false)
    {
        Type type = fieldInfo.DeclaringType;
        var dm = new DynamicMethod("", null, new Type[] { typeof(Entity), typeof(NormalComponent), typeof(object) }, type);
        //2.声明il编译器
        var il = dm.GetILGenerator();
        //3.执行MyClass类的Name属性的Get方法 这句对应刚才的L_0001
        il.Emit(OpCodes.Nop);
        il.Emit(OpCodes.Ldarg_1);
        il.Emit(OpCodes.Callvirt, typeof(NormalComponent).GetProperty("CurrentComponent").GetGetMethod());
        il.Emit(OpCodes.Ldarg_2);
        if (fieldInfo.FieldType.IsValueType)//判断属性类型是否是值类型
        {
            il.Emit(OpCodes.Unbox_Any, fieldInfo.FieldType);//如果是值类型就拆箱
        }
        else
        {
            il.Emit(OpCodes.Castclass, fieldInfo.FieldType);//否则强转
        }
        il.Emit(OpCodes.Stfld, fieldInfo);
        il.Emit(OpCodes.Nop);
        if (isDataDriven && !string.IsNullOrEmpty(_methodName))
        {
            il.Emit(OpCodes.Call, typeof(Observer).GetProperty("Instance").GetGetMethod());
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Callvirt, typeof(Observer).GetMethod(_methodName));
            il.Emit(OpCodes.Nop);
        }
        il.Emit(OpCodes.Ret);
        //5.由il编译器创建指定类型的动态方法委托
        return (PropertySetter)dm.CreateDelegate(typeof(PropertySetter));
    }
}


