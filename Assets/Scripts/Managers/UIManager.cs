using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private EventSystem eventSystem;
    // �̷��� �ϳ��� EventSystem�� �����ǵ��� ������ �ټ� �ְڴ�. 
    private Canvas popUpCanvas; //Where all the UI elements are stored. 
    [SerializeField] private Stack<PopUpUI> popUpStack;
    [SerializeField] private Canvas windowCanvas;
    [SerializeField] private Canvas inGameCanvas;

    private void Awake()
    {
        GameManager.Instance.GameSetup += InitializeGameUI;
        GameManager.Instance.ExitToMain += EraseUponExit; 
        InitializeUI(); 
    }
    public void InitializeUI()
    {
        eventSystem = GameManager.Resource.Instantiate<EventSystem>("UI/EventSystem");
        //�̺�Ʈ �ý��ۿ� ���ؼ�, ������� Input System Package�� �̿��ϴ� ���, �̿뿡 �ʿ��� ������Ʈ�� ������ �������� ����Ͽ�, ���� ������ ���ѹ� �����Ǵ� �̱��ϰ� �����ϰ� �ȴٸ�, 
        // �ּ����� ������ ������ �Ҽ� �ְڴ�.�� 
        eventSystem.transform.SetParent(transform);

        popUpCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        //ĵ������ �˾� UI �� �����ϴ°��� ���� ������ ��ġ�� UI �� �˾��ɼ� �ֵ��� ��ġ�ؾ��ϴ� �ּ����� ��ġ�̴�. 
        popUpCanvas.gameObject.name = "PopUpCanvas";
        popUpCanvas.sortingOrder = 100; // make sure � UI ���� ���������� ����Ʈ �Ǿ� �ش� Canvas�� ���� �տ� ��ġ, ���ϼ� �ֵ��� �Ѵ�. 
        popUpCanvas.transform.SetParent(transform);
        popUpStack = new Stack<PopUpUI>();

        windowCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        windowCanvas.gameObject.name = "WindowCanvas";
        windowCanvas.transform.SetParent(transform);
        windowCanvas.sortingOrder = 10; // �˾�â���ٴ� �ڿ� ��ġ�ɼ� �ֵ��� ������ �� ���̴�. 
    }

    public void InitializeGameUI()
    {
        if (inGameCanvas != null)
            return; 
        inGameCanvas = Resources.Load<Canvas>("UI/InGameUI");
        inGameCanvas.gameObject.name = "InGameUI";
        inGameCanvas.sortingOrder = 0;
    }

    public void EraseUponExit()
    {
        Destroy(inGameCanvas.gameObject); 
    }
    public T ShowPopUpUI<T>(T popUpUI) where T : PopUpUI
    {
        if (popUpStack.Count > 0)
        {
            PopUpUI prevUI = popUpStack.Peek();
            prevUI.gameObject.SetActive(false);
        }

        T ui = GameManager.Pool.GetUI<T>(popUpUI, popUpCanvas.transform); // ���ٸ� ���� ����, �ƴ϶�� �׳� �����ð� 
        ui.transform.SetParent(popUpCanvas.transform, false);
        popUpStack.Push(ui);

        Time.timeScale = 0;

        return ui;
    }
    public T ShowPopUpUI<T>(string path) where T : PopUpUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowPopUpUI(ui);
    }
    public void ClosePopUpUI()
    {
        PopUpUI ui = popUpStack.Pop();
        GameManager.Pool.ReleaseUI(ui.gameObject);

        //���� â�� �ִ� ��Ȳ�̶��, 
        if (popUpStack.Count > 0)
        {
            PopUpUI curUI = popUpStack.Peek();
            curUI.gameObject.SetActive(true); // �� ����â�� �ٽ� ���̰� �Ѵ�. 
        }

        if (popUpStack.Count <= 0)
        {
            Time.timeScale = 1f;
        }
    }
    public void ShowWindowUI(WindowUI windowUI)
    {
        WindowUI ui = GameManager.Pool.GetUI(windowUI);
        ui.transform.SetParent(windowCanvas.transform, false);
    }
    public void ShowWindowUI(string path)
    {
        WindowUI ui = GameManager.Resource.Load<WindowUI>(path);
        ShowWindowUI(ui);
    }
    public void SelectWindowUI(WindowUI windowUI)
    {
        windowUI.transform.SetAsLastSibling(); // ���� ���� 
        //Unity UI�� �̿��ϴ��� Hierarchyâ���� �켱������ �������Ͽ� �־� ���� ���ؼ� �������� �Ͽ��ָ� �ȴ�. 
        //�ٸ� C#, Unity �� �ƴ� ��Ȳ�� ���ؼ��� LinkedList �� �̿��Ͽ� �켱������ �籸��, �߰��� head-> tail �������� Sortinglayer�� �����Ͽ� �������� ������ ���ؼ� �����Ͽ��ִ°��� Best �̰ڴ�. 
    }
    public void CloseWindowUI(WindowUI windowUI)
    {
        GameManager.Pool.ReleaseUI(windowUI.gameObject);
    }
    public T ShowInGameUI<T>(T inGameUI) where T : InGameUI
    {
        T ui = GameManager.Pool.GetUI(inGameUI); // ������ �����ؼ� �ݳ��Ѵ�. 
        ui.transform.SetParent(inGameCanvas.transform, false);
        return ui;
    }
    public T ShowInGameUI<T>(string path) where T : InGameUI
    {
        T ui = GameManager.Resource.Load<T>(path);

        return ShowInGameUI(ui);
    }
    public void CloseInGameUI<T>(T inGameUI) where T : InGameUI
    {
        GameManager.Pool.ReleaseUI(inGameUI);
    }
}