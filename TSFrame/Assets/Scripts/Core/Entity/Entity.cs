using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity<T> : Entity where T : Entity<T>
{
    protected T _instance = null;

    public T Instance { get { return _instance; } }



}



public class Entity
{

    protected Dictionary<Int64, IComponent> componentDic = new Dictionary<Int64, IComponent>();
    protected ComponentFlag currentFlag;

    private Int32 _id = 0;
    private String _guid = "";

    private ComponentCallBack _addComponentCallBack = null;
    private ComponentCallBack _removeComponentCallBack = null;

    private ComponentCallBack _changeComponentCallBack = null;

    private List<Group> _allGroup;

    public String GetGUID()
    {
        return _guid;
    }

    public Int32 GetId()
    {
        return _id;
    }

    public Entity()
    {
        _guid = Guid.NewGuid().ToString();
        _id = _guid.GetHashCode();
    }

    public ComponentFlag GetComponentFlag()
    {
        return currentFlag;
    }

    public Entity SetChangeComponent(ComponentCallBack callBack)
    {
        _changeComponentCallBack = callBack;
        return this;
    }

    public Entity AddComponent(Int64 componentId)
    {
        if (componentDic.ContainsKey(componentId))
        {
            throw new Exception("增加组件失败,组件已存在,组件类型:" + componentId.ToString());
        }
        if (ComponentIds.ComponentTypeDic.ContainsKey(componentId))
        {
            throw new Exception("增加组件失败,组件ID查询失败,组件类型:" + componentId.ToString());
        }
        IComponent component = Activator.CreateInstance(ComponentIds.ComponentTypeDic[componentId]) as IComponent;
        this.componentDic.Add(componentId, component);
        currentFlag.SetFlag(componentId);
        if (_changeComponentCallBack != null)
        {
            _changeComponentCallBack.Invoke(this);
        }
        return this;
    }
    public Entity AddComponent(IComponent component)
    {
        if (componentDic.ContainsKey(component.CurrentId))
        {
            throw new Exception("增加组件失败,组件已存在,组件类型:" + component.CurrentId.ToString());
        }
        this.componentDic.Add(component.CurrentId, component);
        currentFlag.SetFlag(component.CurrentId);
        if (_changeComponentCallBack != null)
        {
            _changeComponentCallBack.Invoke(this);
        }
        return this;
    }
    public Entity RemoveComponent(Int64 componentId)
    {
        if (!componentDic.ContainsKey(componentId))
        {
            throw new Exception("删除组件失败,组件不存在,组件类型:" + componentId.ToString());
        }
        componentDic.Remove(componentId);
        currentFlag.RemoveFlag(componentId);
        if (_changeComponentCallBack != null)
        {
            _changeComponentCallBack.Invoke(this);
        }
        return this;
    }
    public Entity RemoveComponent(IComponent component)
    {
        return RemoveComponent(component.CurrentId);
    }

    public IComponent GetComponent(Int64 componentId)
    {
        if (!componentDic.ContainsKey(componentId))
        {
            return null;
        }
        return componentDic[componentId];
    }

    public T GetComponent<T>() where T : class, IComponent
    {
        foreach (KeyValuePair<Int64, IComponent> item in componentDic)
        {
            if (item.Value.GetType() == typeof(T))
            {
                return item as T;
            }
        }
        return null;
    }

    public IEnumerable<IComponent> GetComponents()
    {
        return new List<IComponent>(componentDic.Values);
    }

    public virtual void Load()
    {
    }

    public virtual void UnLoad()
    {
    }

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
}