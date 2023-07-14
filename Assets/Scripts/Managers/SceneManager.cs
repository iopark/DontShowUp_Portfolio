using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityDeprecatedSceneManager = UnityEngine.SceneManagement.SceneManager; 

public class SceneManager : MonoBehaviour
{
    //Scene Manager�� �ٸ� Scene���� ������ �����ϴ� ��� 
    // Ư�� Scene �� ���ؼ� GameObject �� �ùٸ� Componenent �ν��� Scene ������Ʈ�� �����д�. 

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
        // Scene�� �ѱ涧����, �ش� ���� �ֽ�ȭ �Ǳ⿡, �̶��� ������Ƽ�� �̿��ϸ� �ϳ��� Scene�� ã�� �̿��ϴ°Ϳ� ����ȭ�� ���ټ� �ִ�. 
    }
    private LoadingUI loadingUI;

    private void Awake()
    {
        loadUI = Resources.Load<LoadingUI>("UI/LoadingUI");
        loadUI = Instantiate(loadUI);
        loadUI.transform.SetParent(transform, false); 
        //UI �� ���� ��°���� false���� �ʿ䰡 ���� 
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

        AsyncOperation oper = UnityDeprecatedSceneManager.LoadSceneAsync(sceneName); //3��
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
