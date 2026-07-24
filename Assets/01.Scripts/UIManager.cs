using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TextMeshProUGUI centerText;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private GameObject optionPanel;
    private GameObject option;
    [SerializeField] private GameObject dim;
    private GameObject dimObject;

    [SerializeField] private Slider bossHpSlider;

    [SerializeField] private Button startBtn;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button restartBtn;

    private Canvas uiCanvas;
    private Canvas worldCanvas;
    private Canvas menuCanvas;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }    

    public void Init()
    {
        centerText = GameObject.Find("CenterText").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        centerText.text = "";
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        worldCanvas = GameObject.Find("WorldCanvas").GetComponent<Canvas>();
        if (bossHpSlider == null)
        {
            bossHpSlider = GameObject.Find("BossHPSlider").GetComponent<Slider>();
            bossHpSlider.gameObject.SetActive(false);
        }

        restartBtn = GameObject.Find("RestartBtn").GetComponent<Button>();
        restartBtn.onClick.AddListener(ClickRestartBtn);
        restartBtn.gameObject.SetActive(false);

        if (option != null)
            Destroy(option);

        if (dimObject != null)
            Destroy(dimObject);
    }
    public void Init_Menu()
    {
        menuCanvas = GameObject.Find("MenuCanvas").GetComponent<Canvas>();
        startBtn = GameObject.Find("StartBtn").GetComponent<Button>();
        loadBtn = GameObject.Find("LoadBtn").GetComponent<Button>();
        startBtn.onClick.AddListener(FadeScene);
        loadBtn.onClick.AddListener(FadeScene_Load);
    }

    private void FadeScene()
    {
        if (dimObject == null)
        {
            dimObject = Instantiate(dim, menuCanvas.transform);
            Image dimImg = dimObject.GetComponent<Image>();
            Color color = dimImg.color;
            color.a = 0f;
            dimImg.color = color;

            dimImg.DOFade(1f, 5f)
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

            dimImg.DOFade(1f, 5f)
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

        if (dimObject == null)
        {
            dimObject = Instantiate(dim, uiCanvas.transform);
        }
        else
        {
            dimObject.SetActive(true);
        }

        dimObject.transform.SetAsLastSibling();
        option.transform.SetAsLastSibling();
    }

    public void CloseOptionPanel()
    {
        option.SetActive(false);
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
        centerText.text = "Game Clear!";
        restartBtn.gameObject.SetActive(true);
    }

    private void ClickRestartBtn()
    {
        GameManager.instance.RestartAfterClear();
    }
}
