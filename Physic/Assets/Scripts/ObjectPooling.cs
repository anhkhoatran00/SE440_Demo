using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private static ObjectPooling _instance;
    public static ObjectPooling Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<ObjectPooling>();
            }

            return _instance;
        }
    }

    private Queue<GameObject> _poolQueue = new Queue<GameObject>();

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int poolSize = 10;

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(prefab, transform) as GameObject;
            obj.SetActive(false);
            obj.name = prefab.name + $" {i}";
            _poolQueue.Enqueue(obj);
        }
    }
    public bool CanSpawn()
    {
        return _poolQueue.Count > 0;
    }
    public GameObject PickOne(Transform parent)
    {
        var obj = _poolQueue.Dequeue();
        obj.transform.parent = parent;
        obj.SetActive(true);
        return obj;
    }
    public void ReturnOne(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        _poolQueue.Enqueue(obj);
    }
}
