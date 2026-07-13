using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    void ReturnPool();
}

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    private int poolSize;
    [SerializeField] private List<GameObject> objList = new List<GameObject>();

    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        poolSize = 5;

        foreach(GameObject obj in objList)
        {
            pools[obj.name] = new Queue<GameObject>();
            GameObject parentPool = new GameObject($"{obj.name}_Pool");
            parentPool.transform.SetParent(this.transform);

            for (int i = 0; i < poolSize; i++)
            {
                GameObject go = Instantiate(obj, parentPool.transform);
                go.SetActive(false);
                pools[obj.name].Enqueue(go);
            }
        }
    }
    
    public GameObject GetObject(string name)
    {
        if (!pools.ContainsKey(name))
            return null;

        if (pools[name].Count > 0)
        {
            GameObject go = pools[name].Dequeue();
            go.SetActive(true);
            return go;
        }
        else
        {
            GameObject go = Instantiate(objList.Find(obj => obj.name == name));
            return go;
        }        
    }

    public T GetObject<T>(string name) where T : Component
    {
        if (!pools.ContainsKey(name))
            return null;

        if (pools[name].Count > 0)
        {
            GameObject go = pools[name].Dequeue();
            go.SetActive(true);
            return go.GetComponent<T>();
        }
        else
        {
            GameObject go = Instantiate(objList.Find(obj => obj.name == name));
            return go.GetComponent<T>();
        }
    }

    public void ReturnObject(string name, GameObject obj)
    {
        if (!pools.ContainsKey(name))
        {
            Destroy(obj);
            return;
        }
        obj.SetActive(false);
        pools[name].Enqueue(obj);
    }
}
