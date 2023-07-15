using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static DataManager dataManager;
    private static ResourceManager resource;
    private static PoolManager pool;
    private static UIManager uiManager;
    private static AudioManager audioManager; 
    private static PathManager pathManager;
    private static MapManager mapManager; 
    private static SceneManager sceneManager;
    private static SpawnManager spawnManager;
    private static CombatManager combatManager; 

    public static GameManager Instance
    {
        get { return instance; }
    }
    public static DataManager DataManager
    {
        get { return dataManager; }
    }
    public static PoolManager Pool
    {
        get { return pool; }
    }
    public static ResourceManager Resource
    {
        get { return resource; }
    }

    public static UIManager UIManager
    {
        get { return uiManager; }
    }

    public static AudioManager AudioManager
    {
        get => audioManager; 
    }

    public static PathManager PathManager
    {
        get { return pathManager; }
    }

    public static MapManager MapManager
    {
        get { return mapManager; }
    }

    public static SpawnManager SpawnManager
    {
        get { return spawnManager;}
    }

    public static SceneManager SceneManager
    {
        get { return sceneManager; }
    }

    public static CombatManager CombatManager
    {
        get {  return combatManager; }
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
        InitGeneralManagers();
        ExitToMain += ReturnToMain; 
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
        //확실하게 static 값에 대해서 삭제해주는 최소한의 장치 
    }
    private void InitGeneralManagers()
    {
        GameObject resourceObj = new GameObject() { name = "Resource Manager" };
        resourceObj.transform.SetParent(transform);
        resource = resourceObj.AddComponent<ResourceManager>();

        GameObject dataObj = new GameObject() { name = "Data Manager" };
        dataObj.transform.SetParent(transform);
        dataManager = dataObj.AddComponent<DataManager>();

        GameObject poolObj = new GameObject() { name = "Pool Manager" };
        poolObj.transform.SetParent(transform);
        pool = poolObj.AddComponent<PoolManager>();

        GameObject uiObj = new GameObject() { name = "UI Manager" };
        uiObj.transform.SetParent(transform);
        uiManager = uiObj.AddComponent<UIManager>();
        
        GameObject sceneObj = new GameObject() { name = "Scene Manager" };
        sceneObj.transform.SetParent(transform);
        sceneManager = sceneObj.AddComponent<SceneManager>();

        GameObject pathObj = new GameObject() { name = "Path Manager" };
        pathObj.transform.SetParent(transform);
        pathManager = pathObj.AddComponent<PathManager>();

        GameObject audioObj = new GameObject() { name = "Audio Manager" }; 
        audioObj.transform.SetParent(transform);
        audioManager = audioObj.AddComponent<AudioManager>();
        
    }

    public void InitInGameManagers()
    {
        if (mapManager != null || combatManager != null || spawnManager != null)
            return;
        GameObject mapObj = new GameObject() { name = "Map Manager" };
        mapObj.transform.SetParent(transform);
        mapManager = mapObj.AddComponent<MapManager>();

        GameObject combatObj = new GameObject() { name = "Combat Manager" };
        combatObj.transform.SetParent(transform);
        combatManager = combatObj.AddComponent<CombatManager>();

        GameObject spawnObj = new GameObject() { name = "Spawn Manager" }; 
        spawnObj.transform.SetParent(transform);
        spawnManager = spawnObj.AddComponent<SpawnManager>();
    }

    public void ReturnToMain()
    {
        Destroy(combatManager.gameObject);
        combatManager = null;
        Destroy(spawnManager.gameObject);
        spawnManager = null;
        Destroy(mapManager.gameObject);
        spawnManager = null;
    }

    public UnityAction ExitToMain; 
    public UnityAction GameSetup; 
}
