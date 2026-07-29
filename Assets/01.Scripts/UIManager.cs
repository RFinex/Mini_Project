using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI centerText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Object")]
    [SerializeField] private RectTransform eventArrow;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject trophyInv;
    [SerializeField] private GameObject dim;
    [SerializeField] private GameObject trophyInfo;

    public RectTransform EventArrow
    {
        get
        {
            return eventArrow;
        }
    }
    private TrophyInfoPanel trophyInfoPanel;
    private GameObject option;
    private GameObject trophy;
    private GameObject dimObject;

    [Header("UI")]
    [SerializeField] private Slider bossHpSlider;
    [SerializeField] private Button startBtn;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button restartBtn;

    [Header("Canvas")]
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private Canvas worldCanvas;
    [SerializeField] private Canvas menuCanvas;

    [Header("Setting")]
    [SerializeField] private Vector2 trophyGetPanelPos;
    [SerializeField] private float fadeDelay;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void Init_Menu(Canvas menu, Button start, Button load, RectTransform arrow)
    {
        eventArrow = arrow;
        menuCanvas = menu;
        startBtn = start;
        loadBtn = load;
        if (startBtn != null)
        {
            startBtn.onClick.RemoveAllListeners();
            startBtn.onClick.AddListener(FadeScene);
        }

        if (loadBtn != null)
        {
            loadBtn.onClick.RemoveAllListeners();
            loadBtn.onClick.AddListener(FadeScene_Load);
        }
    }

    public void Init_InGame(Canvas ui, Canvas world, TextMeshProUGUI center, TextMeshProUGUI timer, Button restart, Slider boss, GameObject infoPanel)
    {
        uiCanvas = ui;
        worldCanvas = world;
        centerText = center;
        timerText = timer;
        restartBtn = restart;
        bossHpSlider = boss;
        trophyInfo = infoPanel;

        if (centerText != null)
        {
            centerText.text = "";
        }

        if (restartBtn != null)
        {
            restartBtn.onClick.RemoveAllListeners();
            restartBtn.onClick.AddListener(ClickRestartBtn);
            restartBtn.gameObject.SetActive(false);
        }

        if (bossHpSlider != null)
        {
            bossHpSlider.gameObject.SetActive(false);
        }

        if (option != null)
            Destroy(option);
        if(dimObject != null)
            Destroy(dimObject);
    }

    //public void Init()
    //{
    //    centerText = GameObject.Find("CenterText").GetComponent<TextMeshProUGUI>();
    //    timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
    //    centerText.text = "";
    //    uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
    //    worldCanvas = GameObject.Find("WorldCanvas").GetComponent<Canvas>();
    //    if (bossHpSlider == null)
    //    {
    //        bossHpSlider = GameObject.Find("BossHPSlider").GetComponent<Slider>();
    //        bossHpSlider.gameObject.SetActive(false);
    //    }

    //    restartBtn = GameObject.Find("RestartBtn").GetComponent<Button>();
    //    restartBtn.onClick.AddListener(ClickRestartBtn);
    //    restartBtn.gameObject.SetActive(false);

    //    if (option != null)
    //        Destroy(option);

    //    if (dimObject != null)
    //        Destroy(dimObject);
    //}
    
    private void FadeScene()
    {
        if (dimObject == null)
        {
            dimObject = Instantiate(dim, menuCanvas.transform);
            Image dimImg = dimObject.GetComponent<Image>();
            Color color = dimImg.color;
            color.a = 0f;
            dimImg.color = color;

            dimImg.DOFade(1f, fadeDelay)
                .SetLink(gameObject)
                .OnComplete(() => GameManager.instance.StartGame());
        }
    }

    private void FadeScene_Load()
    {        
        if (!SaveLoadManager.instance.SaveFileCheck())
        {
            Debug.Log("세이브 파일이 존재하지 않습니다.");
            return;
        }

        if (dimObject == null)
        {
            dimObject = Instantiate(dim, menuCanvas.transform);
            Image dimImg = dimObject.GetComponent<Image>();
            Color color = dimImg.color;
            color.a = 0f;
            dimImg.color = color;

            dimImg.DOFade(1f, fadeDelay)
                .SetLink(gameObject)
                .OnComplete(() => GameManager.instance.LoadGame());
        }
    }

    public void OnGameOverText()
    {
        centerText.text = "Game Over Press 'R' Key";
    }

    public void OffCenterText()
    {
        centerText.text = "";
    }

    public void UpdateTimerText(float sec)
    {
        timerText.text = $"{(int)sec / 3600:D2} : {(int)sec / 60 % 60:D2} : {(int)sec % 60:D2}";
    }

    public void OpenOptionPanel()
    {
        if (option == null)
        {
            option = Instantiate(optionPanel, uiCanvas.transform);
        }
        else
        {
            option.SetActive(true);
        }

        SetDimUI();

        dimObject.transform.SetAsLastSibling();
        option.transform.SetAsLastSibling();
    }

    public void CloseOptionPanel()
    {
        option.SetActive(false);
        dimObject.SetActive(false);
    }

    public void OpenTrophyInv()
    {
        if (trophy == null)
        {
            trophy = Instantiate(trophyInv, uiCanvas.transform);
        }
        else
        {
            trophy.SetActive(true);
        }

        SetDimUI();

        dimObject.transform.SetAsLastSibling();
        trophy.transform.SetAsLastSibling();
    }

    public void CloseTrophyInv()
    {
        trophy.SetActive(false);
        dimObject.SetActive(false);
    }

    public void SaveTextOn(Vector3 save)
    {
        GameObject text = ObjectPoolManager.instance.GetObject("SaveCheckText");
        if (text == null)
            return;

        SaveText saveText = text.GetComponent<SaveText>();
        if (saveText != null)
        {
            saveText.FadeText(save, worldCanvas.transform);
        }        
    }

    public void SetBossHPSlider(int maxHp)
    {
        bossHpSlider.gameObject.SetActive(true);
        bossHpSlider.maxValue = maxHp;
        bossHpSlider.value = maxHp;
    }

    public void BossHpSlider(int hp)
    {
        bossHpSlider.value = hp;
    }

    public void OffBossHPSlider()
    {
        bossHpSlider.gameObject.SetActive(false);
    }

    public void OnClearUI()
    {
        SetDimUI();
        centerText.text = "Game Clear!";
        restartBtn.gameObject.SetActive(true);

        dimObject.transform.SetAsLastSibling();
        centerText.transform.SetAsLastSibling();
        restartBtn.transform.SetAsLastSibling();
    }

    private void SetDimUI()
    {
        if (dimObject == null)
        {
            dimObject = Instantiate(dim, uiCanvas.transform);
        }
        else
        {
            dimObject.SetActive(true);
        }
    }

    private void ClickRestartBtn()
    {
        GameManager.instance.RestartAfterClear();
    }

    public void OpenTrophyPanel(int trophyId)
    {
        GameObject trophyGetUI = ObjectPoolManager.instance.GetObject(ConstString.TrophyGetPanel);
        
        if (trophyGetUI == null)
            return;

        trophyGetUI.transform.SetParent(uiCanvas.transform);
        trophyGetUI.transform.SetAsLastSibling();

        RectTransform rect = trophyGetUI.GetComponent<RectTransform>();
        if (rect != null)
        {
            rect.anchoredPosition = trophyGetPanelPos;
        }

        TrophyUIPanel panel = trophyGetUI.GetComponent<TrophyUIPanel>();
        if (panel != null)
        {
            Trophy trophy = DataManager.instance.GetTrophyData(trophyId);
            string name = trophy != null ? trophy.name : "Null Trophy";

            panel.OpenPanel($"Get Trophy : {name}");
        }        
    }

    public void OpenTrophyInfo(int id)
    {
        if (trophyInfoPanel == null)
        {
            trophyInfoPanel = trophyInfo.GetComponent<TrophyInfoPanel>();
        }
        
        trophyInfoPanel.gameObject.SetActive(true);
        trophyInfoPanel.OpenPanel(id);
    }

    public void CloseTrophyInfo()
    {
        if (trophyInfoPanel == null)
        {
            trophyInfoPanel = trophyInfo.GetComponent<TrophyInfoPanel>();
        }

        trophyInfoPanel.ClosePanel();
    }
}
