using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager
{

    protected static EntityManager _instance = null;

    public static EntityManager Instance { get { if (_instance == null) { _instance = new EntityManager(); } return _instance; } }

    public Dictionary<int, GameObjectEntity> _entityDic;

    private EntityManager()
    {
        _entityDic = new Dictionary<int, GameObjectEntity>();
    }


    public Entity<T> CreateEntity<T>() where T : Entity<T>, new()
    {
        Entity<T> entity = new T();

        GameObject obj = new GameObject();
        GameObjectEntity gameObjectEntity = obj.AddComponent<GameObjectEntity>();
        gameObjectEntity.AddEntity<T>(entity);
        _entityDic.Add(obj.GetInstanceID(), gameObjectEntity);
        gameObjectEntity.Load();
        return entity;
    }

    public void RemoveEntity(GameObject obj)
    {
        if (_entityDic.ContainsKey(obj.GetInstanceID()))
        {
            _entityDic[obj.GetInstanceID()].UnLoad();
        }
    }
}

public static class ExpandClass
{
    public static void RemoveEntity(this GameObject obj)
    {
        EntityManager.Instance.RemoveEntity(obj);
    }

}
