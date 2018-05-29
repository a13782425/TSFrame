using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Entity<T> : Entity where T : Entity<T>
{
    protected T _instance = null;

    public T Instance { get { return _instance; } }

}



public class Entity
{
    class ComponentDto
    {
        public IComponent CurrentComponent { get; set; }
        private Dictionary<string, FieldInfoDto> _fieldInfoDic = new Dictionary<string, FieldInfoDto>();
        public Dictionary<string, FieldInfoDto> FieldInfoDic { get { return _fieldInfoDic; } }
    }
    class FieldInfoDto
    {
        public FieldInfo CurrentFieldInfo { get; set; }
        public bool IsReactive { get; set; }
    }

    #region 静态字段
    private static Type _interfaceType = typeof(IReactiveComponent);
    private static Type _dataDrivenType = typeof(DataDrivenAttribute);
    #endregion

    #region 字段和属性
    /// <summary>
    /// 组件字典
    /// </summary>
    private Dictionary<Int64, IComponent> _componentDic = new Dictionary<Int64, IComponent>();

    private Dictionary<Int64, ComponentDto> _allComponenDtoDic = new Dictionary<Int64, ComponentDto>();

    /// <summary>
    /// 组件标签
    /// </summary>
    private ComponentFlag _currentFlag;
    /// <summary>
    /// 最后操作的component
    /// </summary>
    private ComponentDto _lastOperateComponent = null;

    private Int32 _id = 0;
    /// <summary>
    /// 实例的GUID
    /// </summary>
    private String _guid = "";
    /// <summary>
    /// 组件列表发生改变时候的回调
    /// </summary>
    private ComponentCallBack _changeComponentCallBack = null;

    /// <summary>
    /// 组件值发生改变时候的回调
    /// </summary>
    private ValueChangeCallBack _valueChangeCallBack = null;

    private List<Group> _allGroup;

    #endregion

    #region 构造

    public Entity()
    {
        _guid = Guid.NewGuid().ToString();
        _id = _guid.GetHashCode();
    }

    #endregion

    #region 公共方法

    /// <summary>
    /// 获取该组件的GUID
    /// </summary>
    /// <returns></returns>
    public String GetGUID()
    {
        return _guid;
    }

    /// <summary>
    /// 获取该组件的ID
    /// </summary>
    /// <returns></returns>
    public Int32 GetId()
    {
        return _id;
    }

    /// <summary>
    /// 获取该组件的标签
    /// </summary>
    /// <returns></returns>
    public ComponentFlag GetComponentFlag()
    {
        return _currentFlag;
    }

    /// <summary>
    /// 设置组件改变时候的回调
    /// </summary>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public Entity SetChangeComponent(ComponentCallBack callBack)
    {
        _changeComponentCallBack = callBack;
        return this;
    }

    /// <summary>
    /// 设置组件改变时候的回调
    /// </summary>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public Entity SetValueChange(ValueChangeCallBack callBack)
    {
        _valueChangeCallBack = callBack;
        return this;
    }

    /// <summary>
    /// 增加组件，会保留最后增加的组件
    /// </summary>
    /// <param name="componentId"></param>
    /// <returns></returns>
    public Entity AddComponent(Int64 componentId)
    {
        if (_componentDic.ContainsKey(componentId) || _allComponenDtoDic.ContainsKey(componentId))
        {
            throw new Exception("增加组件失败,组件已存在,组件类型:" + componentId.ToString());
        }
        if (!ComponentIds.ComponentTypeDic.ContainsKey(componentId))
        {
            throw new Exception("增加组件失败,组件ID查询失败,组件类型:" + componentId.ToString());
        }
        IComponent component = Activator.CreateInstance(ComponentIds.ComponentTypeDic[componentId]) as IComponent;
        RegisteComponent(component);
        this._componentDic.Add(componentId, component);
        _currentFlag.SetFlag(componentId);
        if (_changeComponentCallBack != null)
        {
            _changeComponentCallBack.Invoke(this);
        }
        return this;
    }

    /// <summary>
    /// 删除组件
    /// </summary>
    /// <param name="componentId"></param>
    /// <returns></returns>
    public Entity RemoveComponent(Int64 componentId)
    {
        if (!_componentDic.ContainsKey(componentId) || !_allComponenDtoDic.ContainsKey(componentId))
        {
            throw new Exception("删除组件失败,组件不存在,组件类型:" + componentId.ToString());
        }
        _allComponenDtoDic.Remove(componentId);
        _componentDic.Remove(componentId);
        _currentFlag.RemoveFlag(componentId);
        if (_lastOperateComponent.CurrentComponent.CurrentId == componentId)
        {
            _lastOperateComponent = null;
        }
        if (_changeComponentCallBack != null)
        {
            _changeComponentCallBack.Invoke(this);
        }
        return this;
    }

    /// <summary>
    /// 设置组件值，对最后一个添加的组件操作
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Entity SetValue(string fieldName, object value)
    {
        if (_lastOperateComponent != null)
        {
            SetValue(_lastOperateComponent.CurrentComponent.CurrentId, fieldName, value);
        }
        else
        {
            Debug.LogError("请先添加组件!");
        }
        //foreach (KeyValuePair<Int64, ComponentDto> item in _allComponenDtoDic)
        //{
        //    if (_lastOperateComponent != null)
        //    {
        //        if (item.Key == _lastOperateComponent.CurrentComponent.CurrentId)
        //        {
        //            continue;
        //        }
        //    }
        //    SetValue(item.Key, fieldName, value);
        //}
        return this;
    }

    /// <summary>
    /// 设置组件值
    /// </summary>
    /// <param name="componentId"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Entity SetValue(Int64 componentId, string fieldName, object value)
    {
        string name = fieldName.ToLower();
        if (this._allComponenDtoDic.ContainsKey(componentId))
        {
            ComponentDto componentDto = this._allComponenDtoDic[componentId];
            if (componentDto.FieldInfoDic.ContainsKey(name))
            {
                try
                {
                    FieldInfoDto dto = this._allComponenDtoDic[componentId].FieldInfoDic[name];
                    if (dto.IsReactive)
                    {
                        object lastValue = dto.CurrentFieldInfo.GetValue(componentDto.CurrentComponent);
                        dto.CurrentFieldInfo.SetValue(componentDto.CurrentComponent, value);
                        if (!value.Equals(lastValue))
                        {
                            //todo 数据驱动回调
                            if (_valueChangeCallBack != null)
                            {
                                _valueChangeCallBack.Invoke(this, componentId);
                            }
                        }
                    }
                    else
                    {
                        dto.CurrentFieldInfo.SetValue(componentDto.CurrentComponent, value);
                    }
                    _lastOperateComponent = componentDto;
                }
                catch (Exception ex)
                {

                    Debug.LogError(ex.Message);
                }
            }
            else
            {
                Debug.LogErrorFormat("组件ID:{0},字段名:{1},不存在!!!", componentId, fieldName);
            }
        }
        else
        {
            Debug.LogErrorFormat("组件ID:{0}不存在!!!", componentId);
        }
        return this;
    }
    /// <summary>
    /// 获取值,对最后一个添加的组件操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public T GetValue<T>(string fieldName)
    {
        if (_lastOperateComponent != null)
        {
            return GetValue<T>(_lastOperateComponent.CurrentComponent.CurrentId, fieldName);
        }
        else
        {
            Debug.LogError("请先添加组件!");
        }
        //foreach (KeyValuePair<Int64, ComponentDto> item in _allComponenDtoDic)
        //{
        //    if (_lastOperateComponent != null)
        //    {
        //        if (item.Key == _lastOperateComponent.CurrentComponent.CurrentId)
        //        {
        //            continue;
        //        }
        //    }
        //    SetValue(item.Key, fieldName, value);
        //}
        return default(T);
    }
    /// <summary>
    /// 获取值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="componentId"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public T GetValue<T>(Int64 componentId, string fieldName)
    {
        string name = fieldName.ToLower();
        T t = default(T);
        if (this._allComponenDtoDic.ContainsKey(componentId))
        {
            ComponentDto componentDto = this._allComponenDtoDic[componentId];
            if (componentDto.FieldInfoDic.ContainsKey(name))
            {
                try
                {
                    FieldInfoDto dto = this._allComponenDtoDic[componentId].FieldInfoDic[name];
                    object value = dto.CurrentFieldInfo.GetValue(componentDto.CurrentComponent);
                    t = (T)value;
                    _lastOperateComponent = componentDto;
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
            else
            {
                Debug.LogErrorFormat("组件ID:{0},字段名:{1},不存在!!!", componentId, fieldName);
            }
        }
        return t;
    }
    #endregion

    #region 私有方法

    private void RegisteComponent(IComponent component)
    {
        ComponentDto componentDto = new ComponentDto();
        componentDto.CurrentComponent = component;
        Type type = component.GetType();
        bool isNeedReactive = false;
        if (_interfaceType.IsAssignableFrom(type))
        {
            isNeedReactive = true;
        }
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfoDto fieldInfoDto = new FieldInfoDto();
            FieldInfo field = fields[i];
            if (isNeedReactive)
            {
                object[] objs = field.GetCustomAttributes(_dataDrivenType, false);
                fieldInfoDto.IsReactive = objs != null && objs.Length > 0;
            }
            fieldInfoDto.CurrentFieldInfo = field;
            if (componentDto.FieldInfoDic.ContainsKey(field.Name.ToLower()))
            {
                Debug.LogError("字段不区分大小写，请检查" + type.Name + "类中的字段：" + field.Name.ToLower());
            }
            else
            {
                componentDto.FieldInfoDic.Add(field.Name.ToLower(), fieldInfoDto);
            }
        }
        _lastOperateComponent = componentDto;
        _allComponenDtoDic.Add(component.CurrentId, componentDto);
    }

    #endregion

    #region 操作符重载

    public static bool operator ==(Entity e1, Entity e2)
    {
        if (e1 == null && e2 == null)
        {
            return true;
        }
        if (e1 == null || e2 == null)
        {
            return false;
        }
        return e1.GetId() == e2.GetId() && e1.GetGUID() == e2.GetGUID();
    }
    public static bool operator !=(Entity e1, Entity e2)
    {
        return !(e1 == e2);
    }
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return GetId();
    }

    #endregion
}