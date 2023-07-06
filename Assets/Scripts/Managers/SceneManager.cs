using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDeprecatedSceneManager = UnityEngine.SceneManagement.SceneManager; 

public class SceneManager : MonoBehaviour
{
    //Scene Manager�� �ٸ� Scene���� ������ �����ϴ� ��� 
    // Ư�� Scene �� ���ؼ� GameObject �� �ùٸ� Componenent �ν��� Scene ������Ʈ�� �����д�. 
    private BaseScene currentScene;
    private LoadingUI loadingUI;

    private void Awake()
    {
        //LoadingUI ui = Resources.Load<LoadingUI>("UI/LoadingUI");
        //loadingUI = Instantiate(ui);
        //loadingUI.transform.SetParent(transform, false); 
        //UI �� ���� ��°���� false���� �ʿ䰡 ���� 
    }
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

    public void LoadScene(string sceneName)
    {
        //Scene Loading 
        StartCoroutine(LoadingRoutine(sceneName)); 
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        loadingUI.FadeOut();
        yield return new WaitForSeconds(0.5f);
        //�ε��� ������� ������ �����ϴ°��� �����ϱ� ���ؼ� Time �� �����ִ°��� �����. 
        //�߰��� �÷��̾� �Է¶��� ���ִ°��� �����. 
        //Time.timeScale = 0f; 

        AsyncOperation oper = UnityDeprecatedSceneManager.LoadSceneAsync(sceneName); //3��
        //oper.allowSceneActivation = false; // true: �ε��� �ٳ����� �ٷ� ��ȯ �ϴ� �� false: true�� ��ȯ�ϴ°��� �ʿ���  
        while (!oper.isDone)
        {
            
            loadingUI.SetProgress(Mathf.Lerp(0f, 0.5f, oper.progress));
            yield return null; 
        }
        //���� Scene���� �ʿ��� �ٸ� �ε� �۾����� �������ش�. 
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
