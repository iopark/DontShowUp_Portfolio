using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static ResourceManager resource;
    private static PoolManager pool;
    //private static UIManager uiManager; 
    private static PathManager pathManager;
    private static MapManager mapManager; 

    public static GameManager Instance
    {
        get { return instance; }
    }
    public static PoolManager Pool
    {
        get { return pool; }
    }
    public static ResourceManager Resource
    {
        get { return resource; }
    }

    //public static UIManager UIManager
    //{
    //    get { return uiManager; }
    //}

    public static PathManager PathManager
    {
        get { return pathManager; }
    }

    public static MapManager MapManager
    {
        get { return mapManager; }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Following has been established Already"); 
            Destroy(this);
            return; 
        }
        instance = this;
        DontDestroyOnLoad(this);
        InitManagers(); 
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
        //확실하게 static 값에 대해서 삭제해주는 최소한의 장치 
    }
    private void InitManagers()
    {
        GameObject resourceObj = new GameObject() { name = "Resource Manager" };
        resourceObj.transform.SetParent(transform);
        resource = resourceObj.AddComponent<ResourceManager>();

        GameObject poolObj = new GameObject() { name = "Pool Manager" };
        poolObj.transform.SetParent(transform); 
        pool = poolObj.AddComponent<PoolManager>();

        //GameObject uiObj = new GameObject() { name = "UI Manager" };
        //uiObj.transform.SetParent(transform);
        //uiManager = uiObj.AddComponent<UIManager>();

        GameObject pathObj = new GameObject() { name = "Path Manager" };
        pathObj.transform.SetParent(transform);
        pathManager = pathObj.AddComponent<PathManager>();
    }
}
