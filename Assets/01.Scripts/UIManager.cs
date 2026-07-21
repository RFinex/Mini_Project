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

    private Slider bossHpSlider;

    private  Canvas uiCanvas;
    private Canvas worldCanvas;

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
        

        if (option != null)
            Destroy(option);

        if (dimObject != null)
            Destroy(dimObject);
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
        Sequence sequence = DOTween.Sequence();
        TextMeshProUGUI saveText = ObjectPoolManager.instance.GetObject("SaveCheckText").GetComponent<TextMeshProUGUI>();
        saveText.alpha = 1f;
        saveText.transform.SetParent(worldCanvas.transform);
        saveText.transform.position = save;

        sequence.Append(saveText.transform.DOMoveY(saveText.transform.position.y + 1f, 1.5f))
            .Join(saveText.DOFade(0f, 1.5f))
            .OnComplete(() => ObjectPoolManager.instance.ReturnObject("SaveCheckText", saveText.gameObject))
            .SetLink(saveText.gameObject, LinkBehaviour.KillOnDisable);
    }
}
