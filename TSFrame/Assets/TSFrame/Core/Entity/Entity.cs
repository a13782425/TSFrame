﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Entity
{
    class ComponentDto
    {
        public IComponent CurrentComponent { get; set; }

        private Dictionary<string, TSProperty> _propertyDic = new Dictionary<string, TSProperty>();
        public Dictionary<string, TSProperty> PropertyDic { get { return _propertyDic; } set { _propertyDic = value; } }
    }

    #region 字段和属性
    /// <summary>
    /// 所有组件字典
    /// </summary>
    private Dictionary<Int64, ComponentDto> _allComponenDtoDic = new Dictionary<Int64, ComponentDto>();
    /// <summary>
    /// 共享关联
    /// </summary>
    private Dictionary<Int64, int> _sharedRelationDic = new Dictionary<Int64, int>();

    /// <summary>
    /// 组件标签
    /// </summary>
    private ComponentFlag _currentFlag;

    private Int32 _id = 0;
    /// <summary>
    /// 组件列表发生改变时候的回调
    /// </summary>
    private ComponentCallBack _changeComponentCallBack = null;
    /// <summary>
    /// 获取组件方法
    /// </summary>
    private GetComponentFunc _getComponentFunc = null;


    /// <summary>
    /// 父亲
    /// </summary>
    private Entity _parent = null;
    /// <summary>
    /// 父亲
    /// </summary>
    public Entity Parent { get { return _parent; } set { _parent = value; } }
    /// <summary>
    /// 儿子们
    /// </summary>
    private List<Entity> _childList = null;
    /// <summary>
    /// 儿子们
    /// </summary>
    public List<Entity> ChildList { get { return _childList; } private set { _childList = value; } }

    #endregion

    #region 构造

    public Entity(ComponentCallBack componentCallBack, GetComponentFunc getFunc)
    {
        _id = Utils.GetId();
        ChildList = new List<Entity>();
        _changeComponentCallBack = componentCallBack;
        _getComponentFunc = getFunc;
    }

    #endregion

    #region 公共方法

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

    ///// <summary>
    ///// 设置组件改变时候的回调
    ///// </summary>
    ///// <param name="callBack"></param>
    ///// <returns></returns>
    //public Entity SetChangeComponent(ComponentCallBack callBack)
    //{
    //    _changeComponentCallBack = callBack;
    //    return this;
    //}

    /// <summary>
    /// 增加组件，会保留最后增加的组件
    /// </summary>
    /// <param name="componentId"></param>
    /// <returns></returns>
    public Entity AddComponent(Int64 componentId)
    {
        //_componentDic.ContainsKey(componentId) ||
        if (_allComponenDtoDic.ContainsKey(componentId))
        {
            throw new Exception("增加组件失败,组件已存在,组件类型:" + componentId.ToString());
        }
        if (!ComponentIds.ComponentTypeDic.ContainsKey(componentId))
        {
            throw new Exception("增加组件失败,组件ID查询失败,组件类型:" + componentId.ToString());
        }
        if (_getComponentFunc == null)
        {
            throw new Exception("无法获取组件!!!");
        }
        //IComponent component = Activator.CreateInstance(ComponentIds.ComponentTypeDic[componentId]) as IComponent;
        IComponent component = _getComponentFunc(componentId);
        ComponentDto dto = new ComponentDto();
        dto.CurrentComponent = component;
        dto.PropertyDic = ILHelper.RegisteComponent(component);
        SetDefaultValue(dto);
        this._allComponenDtoDic.Add(component.CurrentId, dto);
        //this._componentDic.Add(componentId, component);
        this._currentFlag.SetFlag(componentId);
        if (_changeComponentCallBack != null)
        {
            _changeComponentCallBack.Invoke(this, component);
        }
        return this;
    }

    /// <summary>
    /// 添加共享组件
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    public Entity AddSharedCompoennt(SharedComponent shared)
    {
        if (_allComponenDtoDic.ContainsKey(shared.CurrentId))
        {
            throw new Exception("增加组件失败,组件已存在,组件类型:" + shared.CurrentId.ToString());
        }
        ComponentDto dto = new ComponentDto();
        dto.CurrentComponent = shared.CurrentComponent;
        dto.PropertyDic = ILHelper.RegisteComponent(shared.CurrentComponent);

        this._allComponenDtoDic.Add(shared.CurrentId, dto);
        this._sharedRelationDic.Add(shared.CurrentId, shared.SharedId);
        this._currentFlag.SetFlag(shared.CurrentId);
        if (_changeComponentCallBack != null)
        {
            _changeComponentCallBack.Invoke(this, shared.CurrentComponent);
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
        //!_componentDic.ContainsKey(componentId) || 
        if (!_allComponenDtoDic.ContainsKey(componentId))
        {
            throw new Exception("删除组件失败,组件不存在,组件类型:" + componentId.ToString());
        }
        ComponentDto dto = _allComponenDtoDic[componentId];
        _allComponenDtoDic.Remove(componentId);
        //_componentDic.Remove(componentId);
        _currentFlag.RemoveFlag(componentId);
        if (_changeComponentCallBack != null)
        {
            _changeComponentCallBack.Invoke(this, dto.CurrentComponent);
        }
        if (_sharedRelationDic.ContainsKey(componentId))
        {
            _sharedRelationDic.Remove(componentId);
        }
        return this;
    }
    /// <summary>
    /// 删除组件
    /// </summary>
    /// <returns></returns>
    public Entity RemoveComponentAll()
    {
        foreach (KeyValuePair<Int64, ComponentDto> dto in this._allComponenDtoDic)
        {
            //_allComponenDtoDic.Remove(dto.Key);
            _currentFlag.RemoveFlag(dto.Key);
            if (_changeComponentCallBack != null)
            {
                _changeComponentCallBack.Invoke(this, dto.Value.CurrentComponent);
            }
        }
        _allComponenDtoDic.Clear();
        _sharedRelationDic.Clear();
        return this;
    }

    /// <summary>
    /// 设置组件值，对最后一个添加的组件操作
    /// </summary>
    /// <param name="component"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Entity SetValue(ComponentValue component, object value)
    {
        SetValue(component.ComponentId, component.TSPropertyName, value);
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
        //string name = fieldName.ToLower();
        if (this._allComponenDtoDic.ContainsKey(componentId))
        {
            ComponentDto componentDto = this._allComponenDtoDic[componentId];
            if (componentDto.PropertyDic.ContainsKey(fieldName))
            {
                try
                {
                    componentDto.PropertyDic[fieldName].Setter(this, componentDto.CurrentComponent, value);
                }
                catch (Exception ex)
                {
                    Debug.LogErrorFormat("设置属性{0},的时候出错,错误信息{1}", fieldName, ex.Message);
                    //Debug.LogError(ex.Message);
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
    /// <param name="component"></param>
    /// <returns></returns>
    public T GetValue<T>(ComponentValue component)
    {
        return GetValue<T>(component.ComponentId, component.TSPropertyName);
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
        //string name = fieldName.ToLower(); 
        T t = default(T);
        if (this._allComponenDtoDic.ContainsKey(componentId))
        {
            ComponentDto componentDto = this._allComponenDtoDic[componentId];
            if (componentDto.PropertyDic.ContainsKey(fieldName))
            {
                try
                {
                    object value = componentDto.PropertyDic[fieldName].Getter(componentDto.CurrentComponent); ;
                    t = (T)value;
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

    /// <summary>
    /// 获取共享Id
    /// </summary>
    /// <param name="componentId"></param>
    /// <returns></returns>
    public int GetSharedId(Int64 componentId)
    {
        if (_sharedRelationDic.ContainsKey(componentId))
        {
            return _sharedRelationDic[componentId];
        }
        return -1;
    }

    /// <summary>
    /// 从目标实体拷贝到实体
    /// </summary>
    /// <returns></returns>
    public Entity CopyComponent(Entity entity)
    {
        foreach (KeyValuePair<Int64, ComponentDto> componentDto in entity._allComponenDtoDic)
        {
            if (entity.GetSharedId(componentDto.Key) > 0)
            {
                continue;
            }
            if (this.GetComponentFlag().HasFlag(componentDto.Key))
            {
                continue;
            }
            else
            {
                this.AddComponent(componentDto.Key);
            }
        }
        foreach (KeyValuePair<Int64, ComponentDto> componentDto in entity._allComponenDtoDic)
        {
            if (entity.GetSharedId(componentDto.Key) > 0)
            {
                continue;
            }
            ComponentDto dto = componentDto.Value;
            foreach (KeyValuePair<string, TSProperty> item in dto.PropertyDic)
            {
                if (item.Value.DontCopy)
                {
                    continue;
                }
                this.SetValue(componentDto.Key, item.Key, item.Value.Getter(dto.CurrentComponent));
            }
        }
        return this;
    }

    /// <summary>
    /// 拷贝共享组件
    /// 从目标实体拷贝到实体
    /// </summary>
    /// <returns></returns>
    public Entity CopySharedComponent(Entity entity)
    {
        foreach (KeyValuePair<Int64, ComponentDto> componentDto in entity._allComponenDtoDic)
        {
            int sharedId = entity.GetSharedId(componentDto.Key);
            if (sharedId > 0)
            {
                if (this.GetComponentFlag().HasFlag(componentDto.Key))
                {
                    continue;
                }
                else
                {
                    if (_allComponenDtoDic.ContainsKey(componentDto.Key))
                    {
                        throw new Exception("增加组件失败,组件已存在,组件类型:" + componentDto.Key.ToString());
                    }
                    ComponentDto dto = new ComponentDto();
                    dto.CurrentComponent = componentDto.Value.CurrentComponent;
                    dto.PropertyDic = ILHelper.RegisteComponent(componentDto.Value.CurrentComponent);

                    this._allComponenDtoDic.Add(componentDto.Key, dto);
                    this._sharedRelationDic.Add(componentDto.Key, sharedId);
                    this._currentFlag.SetFlag(componentDto.Key);
                    if (_changeComponentCallBack != null)
                    {
                        _changeComponentCallBack.Invoke(this, componentDto.Value.CurrentComponent);
                    }
                }
            }

        }
        return this;
    }

    #endregion

    #region 私有方法

    private void SetDefaultValue(ComponentDto dto)
    {
        foreach (KeyValuePair<string, TSProperty> item in dto.PropertyDic)
        {
            item.Value.Setter(null, dto.CurrentComponent, item.Value.DefaultValue);
            //item.Value.Setter(null, dto.CurrentComponent, item.Value.PropertyType.IsValueType ? Activator.CreateInstance(item.Value.PropertyType) : null);
        }
    }


    #endregion

    #region 操作符重载

    public static bool operator ==(Entity e1, Entity e2)
    {
        object o1 = e1;
        object o2 = e2;
        if (o1 == null && o2 == null)
        {
            return true;
        }
        if (o1 == null || o2 == null)
        {
            return false;
        }
        return e1.GetId() == e2.GetId();
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