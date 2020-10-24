using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPool<T> where T : MonoBehaviour
{
    GameObject parent = null;
    GameObject template = null;
    public List<GameObject> GameObjectList { get; } = new List<GameObject>();
    public List<T> ComponentList { get; } = new List<T>();
    bool isExtendable = false;

    public ComponentPool(GameObject _template, int _poolSize, bool _isExtendable = false, string _parentName = "ObjectPool")
    {
        parent = new GameObject();
        parent.name = _parentName;
        isExtendable = _isExtendable;
        template = _template;
        for (int i = 0; i < _poolSize; ++i)
        {
            AddComponent(ExtendPool());
        }
    }

    GameObject ExtendPool()
    {
        GameObject _newGO = Object.Instantiate(template);
        _newGO.transform.parent = parent.transform;
        _newGO.SetActive(false);
        GameObjectList.Add(_newGO);
        return _newGO;
    }

    T AddComponent(GameObject _toAddTo)
    {
        T _return = _toAddTo.AddComponent<T>();
        ComponentList.Add(_return);
        return _return;
    }

    public T GetComponent()
    {
        foreach (var _component in ComponentList)
        {
            if (!_component.gameObject.activeSelf)
                return _component;
        }
        if (isExtendable)
            return AddComponent(ExtendPool());
        return null;
    }

    public GameObject GetGameObject()
    {
        foreach (var _gameObject in GameObjectList)
        {
            if (!_gameObject.activeSelf)
                return _gameObject;
        }
        if(isExtendable)
        {
            GameObject _newGo = ExtendPool();
            _newGo.AddComponent<T>();
            return _newGo;
        }
        return null;
    }
}
