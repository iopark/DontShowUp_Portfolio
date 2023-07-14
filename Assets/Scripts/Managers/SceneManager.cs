using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityDeprecatedSceneManager = UnityEngine.SceneManagement.SceneManager; 

public class SceneManager : MonoBehaviour
{
    //Scene Manager가 다른 Scene들의 정보에 접근하는 방법 
    // 특정 Scene 에 대해서 GameObject 에 올바른 Componenent 로써의 Scene 컴포넌트를 붙혀둔다. 

    LoadingUI loadUI;
    private BaseScene currentScene;
    public BaseScene CurrentScene
    {
        get
        {
            if (currentScene == null)
                currentScene = GameObject.FindObjectOfType<BaseScene>();
            return currentScene;
        }
        // Scene을 넘길때마다, 해당 씬이 최신화 되기에, 이때에 프로퍼티를 이용하며 하나의 Scene만 찾고 이용하는것에 최적화를 해줄수 있다. 
    }
    private LoadingUI loadingUI;

    private void Awake()
    {
        loadUI = Resources.Load<LoadingUI>("UI/LoadingUI");
        loadUI = Instantiate(loadUI);
        loadUI.transform.SetParent(transform, false); 
        //UI 는 딱히 둘째값은 false해줄 필요가 없다 
    }


    WaitForSeconds loadingInterval = new WaitForSeconds(0.5f); 
    public void LoadScene(string sceneName)
    {
        //Scene Loading 
        StartCoroutine(LoadingRoutine(sceneName)); 
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        loadingUI.FadeOut();
        yield return loadingInterval; 

        AsyncOperation oper = UnityDeprecatedSceneManager.LoadSceneAsync(sceneName); //3초
        while (!oper.isDone)
        {
            
            loadingUI.SetProgress(Mathf.Lerp(0f, 0.5f, oper.progress));
            yield return null; 
        }
        CurrentScene.LoadAsync(); 
        while (currentScene.progress < 1f)
        {
            loadingUI.SetProgress(Mathf.Lerp(0.5f, 1f, CurrentScene.progress));
            yield return null;
        }
        Time.timeScale = 1f; 
        oper.allowSceneActivation = true; 

        loadingUI.FadeIn();
        yield return loadingInterval; 
    }
}
