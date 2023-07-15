using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class PoolManager : MonoBehaviour
{
    Dictionary<string, ObjectPool<GameObject>> poolDic;
    Dictionary<string, Transform> poolContainer;
    Transform poolRoot;
    // UI 전용 풀 구현은 필요하다. 왜냐하면 Canvas에 귀속되는 녀석들 이니까 
    Canvas canvasRoot; 

    private void Awake()
    {
        poolDic = new Dictionary<string, ObjectPool<GameObject>>();
        poolContainer = new Dictionary<string, Transform>();
        poolRoot = new GameObject("PoolRoot").transform;
        canvasRoot = GameManager.Resource.Instantiate<Canvas>("UI/Canvas"); 
    }

    /// <summary>
    /// Without set parent; 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="original"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>

    public T Get<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
    {
        // GameObject 일때
        if (original is GameObject)
        {
            GameObject prefab = original as GameObject;
            if (!poolDic.ContainsKey(prefab.name))
            {
                CreatePool(prefab.name, prefab);
            }

            ObjectPool<GameObject> pool = poolDic[prefab.name]; // 해당 string 값을 키값으로 하여 Dict에서 찾는 오브젝트 풀을 찾은 이후에, 
            GameObject go = pool.Get();
            go.transform.position = position;
            go.transform.rotation = rotation;
            if (parent == null)
                go.transform.SetParent(transform); 
            else 
                go.transform.SetParent(parent);
            return go as T;
        }
        // Component 일때 
        if (original is Component)
        {
            Component component = original as Component; // T 형변환 => Componenent
            string key = component.gameObject.name;
            if (!poolDic.ContainsKey(key))
            {
                CreatePool(key, component.gameObject); // 해당 컴포넌트가 붙어있는 GameObj 를 Dict 에 추가하여주며, 
            }                                   // 
            GameObject go = poolDic[key].Get(); // 게임오브젝트를 불러온뒤, 
            go.transform.position = position;
            go.transform.rotation = rotation;
            if (parent == null)
                go.transform.SetParent(transform); 
            else 
                go.transform.SetParent(parent);
            return go.GetComponent<T>(); // 해당 게임오브젝트의 컴포넌트를 반환하는 형식으로 해주면 된다. 
        }
        // GameObj 도, Componenent가 아니면 뱉어라. 먹는거 아니다. 
        else
        {
            return null;
        }
    }

    public T Get<T>(T original, Vector3 position, Quaternion rotation) where T : Object
    {
        return Get<T>(original, position, rotation, null);
    }

    public T Get<T>(T original, Transform parent) where T : Object
    {
        return Get<T>(original, Vector3.zero, Quaternion.identity, parent);
    }

    public T Get<T>(T prefab) where T: Object
    {
        return Get(prefab, Vector3.zero, Quaternion.identity); // 오버로딩은 이렇게 하는것이 정배다. 
    }

    public bool IsContain<T>(T original) where T: Object
    {
        if (original is GameObject)
        {
            GameObject prefab = original as GameObject;
            string key = prefab.name;

            if (poolDic.ContainsKey(key))
                return true;
            else
                return false;

        }
        else if (original is Component)
        {
            Component component = original as Component;
            string key = component.gameObject.name;

            if (poolDic.ContainsKey(key))
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    public bool Release(GameObject go)
    {
        if (!poolDic.ContainsKey(go.name))
        {
            return false; // 애초에 해당 반납하려는 대상이 대상.name이 dict 에서 없는 키값이라면 
        }
        ObjectPool<GameObject> pool = poolDic[go.name];
        pool.Release(go); // 동일하게 반환한다. 
        return true; // 반납이 성공한 경우 return true 
    }

    public void CreateAndSaveInPool(GameObject go)
    {
        CreatePool(go.name, go); 
    }
    private void CreatePool(string key, GameObject prefab)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(

            //만들때에 적용할 Func, where Func does not require any Parameter values 
            createFunc: () =>
            {
                GameObject obj = Instantiate(prefab);
                obj.name = key; // 생성되는 모든 객체에서의 이름은 키값으로 동일시하게 생성해준다. 
                return obj;
            },
            // 가져올때 Get() 적용할 Action 
            actionOnGet: (GameObject go) =>
            {
                go.SetActive(true);
                go.transform.parent = null;
            },
            // 반납할때 Release() 적용할 Action 
            actionOnRelease: (GameObject go) =>
            {
                go.SetActive(false);
                go.transform.SetParent(transform);
            },
            // 지워줄때 적용할 Action 
            actionOnDestroy: (GameObject go) =>
            {
                Destroy(go);
            }, 
            true, 
            10, // default size 
            200 // Max size
            );
        poolDic.Add(key, pool); // 마지막으로 새로 추가된 
    }
    #region UI Pool Region 
    //UI 전용 
    public T GetUI<T>(T original, Vector3 position, Transform parent = null) where T : Object
    {
        if (original is GameObject)
        {
            GameObject prefab = original as GameObject;
            string key = prefab.name;

            if (!poolDic.ContainsKey(key))
            {
                CreateUIPool(key, prefab, parent);
            }


            GameObject obj = poolDic[key].Get();
            obj.transform.position = position;
            return obj as T;
        }
        else if (original is Component)
        {
            Component component = original as Component;
            string key = component.gameObject.name;

            if (!poolDic.ContainsKey(key))
                CreateUIPool(key, component.gameObject, parent);

            GameObject obj = poolDic[key].Get();
            obj.transform.position = position;
            return obj.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }
    public T GetUI<T>(T original, Transform parent = null) where T : Object
    {
        if (original is GameObject)
        {
            GameObject prefab = original as GameObject;
            string key = prefab.name;

            if (!poolDic.ContainsKey(key))
                CreateUIPool(key, prefab, parent);

            GameObject obj = poolDic[key].Get();
            return obj as T;
        }
        else if (original is Component)
        {
            Component component = original as Component;
            string key = component.gameObject.name;

            if (!poolDic.ContainsKey(key))
                CreateUIPool(key, component.gameObject, parent);

            GameObject obj = poolDic[key].Get();
            return obj.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }
    public bool ReleaseUI<T>(T instance) where T : Object
    {
        if (instance is GameObject)
        {
            GameObject go = instance as GameObject;
            string key = go.name;

            if (!poolDic.ContainsKey(key))
                return false;

            poolDic[key].Release(go);
            return true;
        }
        else if (instance is Component)
        {
            Component component = instance as Component;
            string key = component.gameObject.name;

            if (!poolDic.ContainsKey(key))
                return false;

            poolDic[key].Release(component.gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }
    private void CreateUIPool(string key, GameObject prefab, Transform parent = null)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject obj = Instantiate(prefab);
                obj.gameObject.name = key;
                return obj;
            },
            actionOnGet: (GameObject obj) =>
            {
                obj.gameObject.SetActive(true);
            },
            actionOnRelease: (GameObject obj) =>
            {
                obj.gameObject.SetActive(false);
                if (parent == null)
                {
                    obj.transform.SetParent(canvasRoot.transform, false);
                }
                else
                    obj.transform.SetParent(parent, false); 

            },
            actionOnDestroy: (GameObject obj) =>
            {
                Destroy(obj);
            }
            );
        poolDic.Add(key, pool);
    }
}
#endregion