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
    private static string _codeDirectory = Application.dataPath + "/TSFrame/Core/Generate";
    private static string _codePath = _codeDirectory + "/ComponentVariable.cs";
    [MenuItem("TSFrame/GenerateComponent")]
    private static void GenerateComponent()
    {
        if (Application.isPlaying)
        {
            return;
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
            StringBuilder sb = BeginGenerate();
            StreamWriter sw = new StreamWriter(_codePath, false, new UTF8Encoding());
            sw.Write(sb.ToString());
            sw.Close();
            sw.Dispose();
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

    private static StringBuilder BeginGenerate()
    {
        EditorUtility.DisplayProgressBar("processing", "statistics file count...", 0);
        Type type = typeof(IComponent);
        PropertyInfo propertyInfo = type.GetProperty("CurrentId");
        Assembly assembly = type.Assembly;
        Type[] types = assembly.GetTypes();
        List<Type> tempList = new List<Type>();
        for (int i = 0; i < types.Length; i++)
        {
            if (type.IsAssignableFrom(types[i]) && !types[i].IsAbstract)
            {
                tempList.Add(types[i]);
            }
        }
        EditorUtility.DisplayProgressBar("processing", "file count " + tempList.Count, 0);
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("//------------------------------------------------------------------------------------------------------------");
        sb.AppendLine("//-----------------------------------generate file " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "----------------------------------------");
        sb.AppendLine("//------------------------------------------------------------------------------------------------------------");
        sb.AppendLine("");
        for (int i = 0; i < tempList.Count; i++)
        {
            Type temp = tempList[i];
            EditorUtility.DisplayProgressBar("processing", "file: " + temp.Name + " " + (i + 1) + "/" + tempList.Count, i * 1.0f / (tempList.Count + 1));
            sb.AppendLine("public class " + temp.Name + "Variable");
            sb.AppendLine("{");
            object obj = Activator.CreateInstance(temp);
            Int64 num = (Int64)propertyInfo.GetValue(obj, null);
            #region Property

            PropertyInfo[] props = temp.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (props != null && props.Length > 0)
            {
                for (int j = 0; j < props.Length; j++)
                {
                    PropertyInfo property = props[j];
                    if (property.Name == "CurrentId")
                    {
                        continue;
                    }
                    sb.AppendLine("    public static ComponentValue " + property.Name + " = new ComponentValue() { ComponentId = " + num + ", TSPropertyName = \"" + property.Name + "\" };");
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
                    sb.AppendLine("    public static ComponentValue " + fieldInfo.Name + " = new ComponentValue() { ComponentId = " + num + ", TSPropertyName = \"" + fieldInfo.Name + "\" };");
                }
            }

            #endregion

            sb.AppendLine("}");
            sb.AppendLine("");
            System.Threading.Thread.Sleep(100);
        }
        EditorUtility.DisplayProgressBar("processing", "Success!!!", 1);
        System.Threading.Thread.Sleep(100);
        return sb;
    }
}

