using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDeprecatedSceneManager = UnityEngine.SceneManagement.SceneManager; 

public class SceneManager : MonoBehaviour
{
    //Scene Manager가 다른 Scene들의 정보에 접근하는 방법 
    // 특정 Scene 에 대해서 GameObject 에 올바른 Componenent 로써의 Scene 컴포넌트를 붙혀둔다. 
    private BaseScene currentScene;
    private LoadingUI loadingUI;

    private void Awake()
    {
        //LoadingUI ui = Resources.Load<LoadingUI>("UI/LoadingUI");
        //loadingUI = Instantiate(ui);
        //loadingUI.transform.SetParent(transform, false); 
        //UI 는 딱히 둘째값은 false해줄 필요가 없다 
    }
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

    public void LoadScene(string sceneName)
    {
        //Scene Loading 
        StartCoroutine(LoadingRoutine(sceneName)); 
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        loadingUI.FadeOut();
        yield return new WaitForSeconds(0.5f);
        //로딩과 상관없이 게임이 진행하는것을 방지하기 위해서 Time 을 멈춰주는것이 정배다. 
        //추가로 플레이어 입력또한 꺼주는것이 정배다. 
        //Time.timeScale = 0f; 

        AsyncOperation oper = UnityDeprecatedSceneManager.LoadSceneAsync(sceneName); //3초
        //oper.allowSceneActivation = false; // true: 로딩이 다끝나면 바로 전환 하는 값 false: true로 전환하는것이 필요함  
        while (!oper.isDone)
        {
            
            loadingUI.SetProgress(Mathf.Lerp(0f, 0.5f, oper.progress));
            yield return null; 
        }
        //현재 Scene에서 필요한 다른 로딩 작업들을 진행해준다. 
        CurrentScene.LoadAsync(); 
        while (currentScene.progress < 1f)
        {
            loadingUI.SetProgress(Mathf.Lerp(0.5f, 1f, CurrentScene.progress));
            yield return null;
        }
        Time.timeScale = 1f; 
        oper.allowSceneActivation = true; 
        //TODO: Player position re-position; 
        loadingUI.FadeIn();
        yield return new WaitForSecondsRealtime(0.5f); 
    }
}
