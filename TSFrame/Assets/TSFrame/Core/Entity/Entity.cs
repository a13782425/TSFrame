using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Entity
{

    #region 字段和属性

    /// <summary>
    /// 所有组件字典
    /// </summary>
    public NormalComponent[] _allComponenArray;

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
        _id = Utils.GetEntityId();
        ChildList = new List<Entity>();
        _allComponenArray = new NormalComponent[ComponentIds.COMPONENT_MAX_COUNT];
        _changeComponentCallBack = componentCallBack;
        _getComponentFunc = getFunc;
        _currentFlag = new ComponentFlag();
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

    public Entity AddComponent(int componentId)
    {
        if (componentId > ComponentIds.COMPONENT_MAX_COUNT)
        {
            throw new Exception("组件Id有误" + componentId.ToString());
        }
        NormalComponent component = _allComponenArray[componentId];
        if (component != null)
        {
            throw new Exception("增加组件失败,组件已存在,组件类型:" + componentId.ToString());
        }
        if (_getComponentFunc == null)
        {
            throw new Exception("无法获取组件!!!");
        }
        component = _getComponentFunc(componentId);
        if (component == null)
        {
            throw new Exception("增加组件失败,组件ID查询失败,组件类型:" + componentId.ToString());
        }
        component.PropertyArray = ILHelper.GetComponentProperty(componentId);
        SetDefaultValue(component);
        _allComponenArray[componentId] = component;
        this._currentFlag.SetFlag(component.OperatorId);
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
        int componentId = shared.ComponentId;
        if (ComponentIds.COMPONENT_MAX_COUNT > componentId)
        {
            if (_allComponenArray[componentId] != null)
            {
                throw new Exception("增加组件失败,组件已存在,组件类型:" + shared.OperatorId.ToString());
            }
            _allComponenArray[componentId] = shared.CurrentComponent;
            this._currentFlag.SetFlag(shared.OperatorId);
            if (_changeComponentCallBack != null)
            {
                _changeComponentCallBack.Invoke(this, shared.CurrentComponent);
            }
        }
        return this;
    }

    /// <summary>
    /// 删除组件
    /// </summary>
    /// <param name="componentId"></param>
    /// <returns></returns>
    public Entity RemoveComponent(int componentId)
    {
        if (ComponentIds.COMPONENT_MAX_COUNT > componentId)
        {
            NormalComponent normal = _allComponenArray[componentId];
            if (normal == null)
            {
                goto Log;
            }
            _allComponenArray[componentId] = null;
            _currentFlag.RemoveFlag(normal.OperatorId);
            if (_changeComponentCallBack != null)
            {
                _changeComponentCallBack.Invoke(this, normal);
            }
            return this;
        }
        Log: Debug.LogError("删除组件失败,组件Id不存在,组件Id:" + componentId);

        return this;
    }
    /// <summary>
    /// 删除组件
    /// </summary>
    /// <returns></returns>
    public Entity RemoveComponentAll()
    {
        for (int i = 0; i < _allComponenArray.Length; i++)
        {
            if (i == ComponentIds.ACTIVE || i == ComponentIds.LIFE_CYCLE || i == ComponentIds.POOL)
            {
                continue;
            }

            NormalComponent normalComponent = _allComponenArray[i];
            if (normalComponent != null)
            {
                _allComponenArray[i] = null;
                _currentFlag.RemoveFlag(normalComponent.OperatorId);
                if (_changeComponentCallBack != null)
                {
                    _changeComponentCallBack.Invoke(this, normalComponent);
                }
            }
        }
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
        int componentId = component.ComponentId;
        if (ComponentIds.COMPONENT_MAX_COUNT > componentId)
        {
            NormalComponent normalComponent = this._allComponenArray[componentId];
            if (normalComponent == null)
            {
                Debug.LogError("组件不存在,组件Id:" + componentId);
                return this;
            }
            try
            {
                normalComponent.PropertyArray[component.PropertyId].Setter(this, normalComponent, value);
                return this;
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("设置属性{0},的时候出错,错误信息{1}", component.PropertyId, ex.Message);
                return this;
            }
        }
        Debug.LogError("组件Id过大无法设置,组件Id:" + componentId);
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
        T t = default(T);
        int componentId = component.ComponentId;
        if (ComponentIds.COMPONENT_MAX_COUNT > componentId)
        {
            NormalComponent normalComponent = this._allComponenArray[componentId];
            if (normalComponent == null)
            {
                Debug.LogError("组件不存在,组件Id:" + componentId);
                return t;
            }
            try
            {
                object o = normalComponent.PropertyArray[component.PropertyId].Getter(normalComponent.CurrentComponent);
                t = (T)o;
                return t;
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("获取属性{0},的时候出错,错误信息{1}", component.PropertyId, ex.Message);
                return t;
            }
        }
        Debug.LogError("组件Id过大无法获取,组件Id:" + componentId);
        return t;
    }

    /// <summary>
    /// 从目标实体拷贝到实体
    /// </summary>
    /// <returns></returns>
    public Entity CopyComponent(Entity entity)
    {
        for (int i = 0; i < ComponentIds.COMPONENT_MAX_COUNT; i++)
        {
            NormalComponent component = entity._allComponenArray[i];
            if (component == null)
            {
                continue;
            }
            if (component.SharedId > 0)
            {
                continue;
            }
            if (this._allComponenArray[i] != null)
            {
                continue;
            }
            this.AddComponent(i);
        }
        for (int i = 0; i < ComponentIds.COMPONENT_MAX_COUNT; i++)
        {
            NormalComponent component = entity._allComponenArray[i];
            if (component == null)
            {
                continue;
            }
            if (component.SharedId > 0)
            {
                continue;
            }
            if (this._allComponenArray[i] != null)
            {
                continue;
            }
            NormalComponent thisComponent = this._allComponenArray[i];
            for (int j = 0; j < component.PropertyArray.Length; j++)
            {
                if (component.PropertyArray[i].DontCopy)
                    continue;
                thisComponent.PropertyArray[i].Setter(this, thisComponent, component.PropertyArray[i].Getter(component.CurrentComponent));
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
        for (int i = 0; i < ComponentIds.COMPONENT_MAX_COUNT; i++)
        {
            NormalComponent component = entity._allComponenArray[i];
            if (component == null)
            {
                continue;
            }
            if (component.SharedId < 0)
            {
                continue;
            }
            if (this._allComponenArray[i] != null)
            {
                continue;
            }
            this._allComponenArray[i] = component;
            this._currentFlag.SetFlag(component.OperatorId);
            if (_changeComponentCallBack != null)
            {
                _changeComponentCallBack.Invoke(this, component);
            }
        }
        return this;
    }

    #endregion

    #region 私有方法

    private void SetDefaultValue(NormalComponent dto)
    {
        for (int i = 0; i < dto.PropertyArray.Length; i++)
        {
            TSProperty tSProperty = dto.PropertyArray[i];
            tSProperty.Setter(null, dto, tSProperty.DefaultValue);
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