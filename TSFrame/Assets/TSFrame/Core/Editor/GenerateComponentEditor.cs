using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;


public class GenerateComponentEditor : Editor
{
    //private static string _codeDirectory = Application.dataPath + "/TSFrame/Core/Generate";
    private static string _codeDirectory;
    private static string _codePath = _codeDirectory + "/ComponentVariable.cs";
    private static string _codeIdsPath = _codeDirectory + "/ComponentIdsExtension.cs";

    private static Type _interfaceType = typeof(IReactiveComponent);

    private static Type _dataDrivenType = typeof(DataDrivenAttribute);

    private static Type _dontCopyType = typeof(DontCopyAttribute);

    [MenuItem("TSFrame/GenerateComponent")]
    private static void GenerateComponent()
    {
        if (Application.isPlaying)
        {
            return;
        }
        string[] guids = AssetDatabase.FindAssets(typeof(GenerateComponentEditor).Name);
        if (guids.Length != 1)
        {
            Debug.LogError("guids存在多个");
        }
        else
        {
            //Assets/ThirdPlug/TSFrame/Core/Editor/GenerateComponentEditor.cs
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            path = Path.GetFullPath(path);
            path = Path.GetDirectoryName(path);
            _codeDirectory = Path.GetFullPath(path + "/../Generate");
            _codePath = _codeDirectory + "/ComponentVariable.cs";
            _codeIdsPath = _codeDirectory + "/ComponentIdsExtension.cs";
        }
        EditorApplication.LockReloadAssemblies();
        if (!Directory.Exists(_codeDirectory))
        {
            Directory.CreateDirectory(_codeDirectory);
        }
        if (!File.Exists(_codePath))
        {
            File.Create(_codePath);
        }
        try
        {
            BeginGenerate();
            //StringBuilder sb = BeginGenerate();
            //StreamWriter sw = new StreamWriter(_codePath, false, new UTF8Encoding());
            //sw.Write(sb.ToString());
            //sw.Close();
            //sw.Dispose();
            EditorUtility.ClearProgressBar();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            EditorUtility.ClearProgressBar();
        }
        finally
        {
            AssetDatabase.Refresh();
            EditorApplication.UnlockReloadAssemblies();
        }
    }
    private static List<Type> _componentTypeList = new List<Type>();
    private static PropertyInfo idPropertyInfo = null;
    private static void BeginGenerate()
    {
        EditorUtility.DisplayProgressBar("processing", "statistics file count...", 0);
        Type type = typeof(IComponent);
        idPropertyInfo = type.GetProperty("CurrentId");
        Assembly assembly = type.Assembly;
        Type[] types = assembly.GetTypes();
        _componentTypeList.Clear();
        for (int i = 0; i < types.Length; i++)
        {
            if (type.IsAssignableFrom(types[i]) && !types[i].IsAbstract)
            {
                _componentTypeList.Add(types[i]);
            }
        }

        EditorUtility.DisplayProgressBar("processing", "file count " + _componentTypeList.Count, 0);

        GenerateComponentValue();
        GenerateComponentIds();
    }

    private static void GenerateComponentValue()
    {
        StringBuilder codeSb = new StringBuilder();
        codeSb.AppendLine("//------------------------------------------------------------------------------------------------------------");
        codeSb.AppendLine("//-----------------------------------generate file " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "----------------------------------------");
        codeSb.AppendLine("//------------------------------------------------------------------------------------------------------------");
        codeSb.AppendLine("");
        for (int i = 0; i < _componentTypeList.Count; i++)
        {
            Type temp = _componentTypeList[i];
            EditorUtility.DisplayProgressBar("processing code", "file: " + temp.Name + " " + (i + 1) + "/" + _componentTypeList.Count, (i * 1.0f / (_componentTypeList.Count + 1)) * 0.5f);
            codeSb.AppendLine("public class " + temp.Name + "Variable");
            codeSb.AppendLine("{");
            object obj = Activator.CreateInstance(temp);
            Int64 num = (Int64)idPropertyInfo.GetValue(obj, null);
            bool isNeedReactive = false;
            if (_interfaceType.IsAssignableFrom(temp))
            {
                isNeedReactive = true;
            }
            int count = 0;
            #region Property

            PropertyInfo[] props = temp.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (props != null && props.Length > 0)
            {
                for (int j = 0; j < props.Length; j++)
                {
                    PropertyInfo property = props[j];
                    if (property.GetSetMethod() == null || property.GetGetMethod() == null)
                    {
                        continue;
                    }
                    bool isDataDriven = false;
                    object[] objs = property.GetCustomAttributes(_dataDrivenType, false);
                    //DataDrivenAttribute 特性长度大于0 则这个属性需要数据驱动，否则则不需要数据驱动
                    if (objs.Length > 0)
                    {
                        isDataDriven = isNeedReactive;
                    }
                    bool dontCopy = property.GetCustomAttributes(_dontCopyType, false).Length > 0;
                    codeSb.AppendLine("    public static ComponentValue " + property.Name + " = new ComponentValue() { ComponentId = " + i + ", PropertyId = " + count + ", OperatorId = " + num + ", DontCopy = " + (dontCopy ? "true" : "false") + ", NeedReactive = " + (isDataDriven ? "true" : "false") + " };");
                    count++;
                }
            }

            #endregion

            #region FieldInfo

            FieldInfo[] fieldInfos = temp.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (fieldInfos != null && fieldInfos.Length > 0)
            {
                for (int j = 0; j < fieldInfos.Length; j++)
                {
                    FieldInfo fieldInfo = fieldInfos[j];
                    if (fieldInfo.Name.StartsWith("<"))
                    {
                        continue;
                    }
                    bool isDataDriven = false;
                    object[] objs = fieldInfo.GetCustomAttributes(_dataDrivenType, false);
                    //DataDrivenAttribute 特性长度大于0 则这个属性需要数据驱动，否则则不需要数据驱动
                    if (objs.Length > 0)
                    {
                        isDataDriven = isNeedReactive;
                    }
                    bool dontCopy = fieldInfo.GetCustomAttributes(_dontCopyType, false).Length > 0;
                    codeSb.AppendLine("    public static ComponentValue " + fieldInfo.Name + " = new ComponentValue() { ComponentId = " + i + ", PropertyId = " + count + ", OperatorId = " + num + ", DontCopy = " + (dontCopy ? "true" : "false") + ", NeedReactive = " + (isDataDriven ? "true" : "false") + " };");
                    count++;
                }
            }

            #endregion
            codeSb.AppendLine("    public static int Count { get { return " + count + "; } }");
            codeSb.AppendLine("}");
            codeSb.AppendLine("");
            System.Threading.Thread.Sleep(100);
        }
        System.Threading.Thread.Sleep(100);
        StreamWriter sw = new StreamWriter(_codePath, false, new UTF8Encoding());
        sw.Write(codeSb.ToString());
        sw.Close();
        sw.Dispose();
    }


    private static void GenerateComponentIds()
    {
        StringBuilder codeIdSb = new StringBuilder();

        codeIdSb.AppendLine("//------------------------------------------------------------------------------------------------------------");
        codeIdSb.AppendLine("//-----------------------------------generate file " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "----------------------------------------");
        codeIdSb.AppendLine("//------------------------------------------------------------------------------------------------------------");
        codeIdSb.AppendLine("using System;");
        codeIdSb.AppendLine("");
        codeIdSb.AppendLine("public static partial class ComponentIds");
        codeIdSb.AppendLine("{");

        FieldInfo[] idsFields = typeof(OperatorIds).GetFields(BindingFlags.Static | BindingFlags.Public);

        #region Field

        for (int i = 0; i < _componentTypeList.Count; i++)
        {
            Type temp = _componentTypeList[i];

            object obj = Activator.CreateInstance(temp);
            Int64 num = (Int64)idPropertyInfo.GetValue(obj, null);

            for (int j = 0; j < idsFields.Length; j++)
            {
                if (idsFields[j].FieldType == typeof(Int64))
                {
                    if ((Int64)idsFields[j].GetValue(null) == num)
                    {
                        codeIdSb.AppendLine("    public const int " + idsFields[j].Name + " = " + i + ";");
                    }
                }
            }
        }
        codeIdSb.AppendLine("");
        codeIdSb.AppendLine("    public const int COMPONENT_MAX_COUNT = " + _componentTypeList.Count + ";");
        #endregion

        #region Int64
        //codeIdSb.AppendLine("");
        //codeIdSb.AppendLine("    public static NormalComponent GetComponent(Int64 componentId)");
        //codeIdSb.AppendLine("    {");
        //codeIdSb.AppendLine("        switch (componentId)");
        //codeIdSb.AppendLine("        {");
        //for (int i = 0; i < _componentTypeList.Count; i++)
        //{
        //    Type temp = _componentTypeList[i];
        //    EditorUtility.DisplayProgressBar("processing ids", "file: " + temp.Name + " " + (i + 1) + "/" + _componentTypeList.Count, 0.5f + (i * 1.0f / (_componentTypeList.Count + 1)) * 0.5f);

        //    object obj = Activator.CreateInstance(temp);
        //    Int64 num = (Int64)idPropertyInfo.GetValue(obj, null);
        //    for (int j = 0; j < idsFields.Length; j++)
        //    {
        //        if (idsFields[j].FieldType == typeof(Int64))
        //        {
        //            if ((Int64)idsFields[j].GetValue(null) == num)
        //            {
        //                codeIdSb.AppendLine("            case ComponentIds." + idsFields[j].Name + ":");
        //                codeIdSb.AppendLine("                return new NormalComponent(new " + temp.Name + "());");
        //            }
        //        }
        //    }
        //    System.Threading.Thread.Sleep(100);
        //}

        //codeIdSb.AppendLine("            default:");
        //codeIdSb.AppendLine("                return null;");
        //codeIdSb.AppendLine("        }");
        //codeIdSb.AppendLine("    }");

        #endregion

        #region Int32
        codeIdSb.AppendLine("");
        codeIdSb.AppendLine("    public static NormalComponent GetComponent(Int32 componentId)");
        codeIdSb.AppendLine("    {");
        codeIdSb.AppendLine("        switch (componentId)");
        codeIdSb.AppendLine("        {");
        for (int i = 0; i < _componentTypeList.Count; i++)
        {
            Type temp = _componentTypeList[i];
            EditorUtility.DisplayProgressBar("processing ids", "file: " + temp.Name + " " + (i + 1) + "/" + _componentTypeList.Count, 0.5f + (i * 1.0f / (_componentTypeList.Count + 1)) * 0.5f);
            object obj = Activator.CreateInstance(temp);
            Int64 num = (Int64)idPropertyInfo.GetValue(obj, null);
            for (int j = 0; j < idsFields.Length; j++)
            {
                if (idsFields[j].FieldType == typeof(Int64))
                {
                    if ((Int64)idsFields[j].GetValue(null) == num)
                    {
                        codeIdSb.AppendLine("            case ComponentIds." + idsFields[j].Name + ":");
                        codeIdSb.AppendLine("                return new NormalComponent(new " + temp.Name + "(), ComponentIds." + idsFields[j].Name + ");");
                    }
                }
            }
            System.Threading.Thread.Sleep(100);
        }

        codeIdSb.AppendLine("            default:");
        codeIdSb.AppendLine("                return null;");
        codeIdSb.AppendLine("        }");
        codeIdSb.AppendLine("    }");
        #endregion

        #region ctor
        codeIdSb.AppendLine("");
        codeIdSb.AppendLine("    static ComponentIds()");
        codeIdSb.AppendLine("    {");
        for (int i = 0; i < _componentTypeList.Count; i++)
        {
            Type temp = _componentTypeList[i];

            object obj = Activator.CreateInstance(temp);
            Int64 num = (Int64)idPropertyInfo.GetValue(obj, null);

            for (int j = 0; j < idsFields.Length; j++)
            {
                if (idsFields[j].FieldType == typeof(Int64))
                {
                    if ((Int64)idsFields[j].GetValue(null) == num)
                    {
                        codeIdSb.AppendLine("        ComponentTypeArray[" + i + "] = typeof(" + temp.Name + ");");
                        codeIdSb.AppendLine("        ILHelper.RegisteComponent(ComponentTypeArray[" + i + "], " + i + ");;");
                    }
                }
            }
        }
        codeIdSb.AppendLine("    }");
        codeIdSb.AppendLine("");
        #endregion

        codeIdSb.AppendLine("}");

        System.Threading.Thread.Sleep(100);
        EditorUtility.DisplayProgressBar("processing", "Success!!!", 1);
        StreamWriter sw = new StreamWriter(_codeIdsPath, false, new UTF8Encoding());
        sw.Write(codeIdSb.ToString());
        sw.Close();
        sw.Dispose();
    }


}