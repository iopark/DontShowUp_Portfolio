using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private EventSystem eventSystem;
    // 이렇게 하나의 EventSystem이 생성되도록 집중해 줄수 있겠다. 
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
        //이벤트 시스템에 대해서, 예를들어 Input System Package를 이용하는 경우, 이용에 필요한 컴포넌트를 저장한 프리팹을 사용하여, 게임 내에서 단한번 생성되는 싱글턴과 생성하게 된다면, 
        // 최소한의 생성은 보장을 할수 있겠다.ㅏ 
        eventSystem.transform.SetParent(transform);

        popUpCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        //캔버스로 팝업 UI 를 관리하는것은 예상 가능한 위치에 UI 가 팝업될수 있도록 설치해야하는 최소한의 장치이다. 
        popUpCanvas.gameObject.name = "PopUpCanvas";
        popUpCanvas.sortingOrder = 100; // make sure 어떤 UI 보다 마지막으로 프린트 되어 해당 Canvas가 가장 앞에 비치, 보일수 있도록 한다. 
        popUpCanvas.transform.SetParent(transform);
        popUpStack = new Stack<PopUpUI>();

        windowCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        windowCanvas.gameObject.name = "WindowCanvas";
        windowCanvas.transform.SetParent(transform);
        windowCanvas.sortingOrder = 10; // 팝업창보다는 뒤에 배치될수 있도록 설정해 준 값이다. 
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

        T ui = GameManager.Pool.GetUI<T>(popUpUI, popUpCanvas.transform); // 없다면 새로 생성, 아니라면 그냥 꺼내올것 
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

        //이전 창이 있던 상황이라면, 
        if (popUpStack.Count > 0)
        {
            PopUpUI curUI = popUpStack.Peek();
            curUI.gameObject.SetActive(true); // 그 이전창을 다시 보이게 한다. 
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
        windowUI.transform.SetAsLastSibling(); // 막내 선정 
        //Unity UI를 이용하는한 Hierarchy창에서 우선순위를 재정렬하여 주어 값에 대해서 재정립을 하여주면 된다. 
        //다만 C#, Unity 가 아닌 상황에 대해서는 LinkedList 를 이용하여 우선순위를 재구성, 추가로 head-> tail 기준으로 Sortinglayer를 정렬하여 보여지는 순서에 대해서 정의하여주는것이 Best 이겠다. 
    }
    public void CloseWindowUI(WindowUI windowUI)
    {
        GameManager.Pool.ReleaseUI(windowUI.gameObject);
    }
    public T ShowInGameUI<T>(T inGameUI) where T : InGameUI
    {
        T ui = GameManager.Pool.GetUI(inGameUI); // 없으면 생성해서 반납한다. 
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