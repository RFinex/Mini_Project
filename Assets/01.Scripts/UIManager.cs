using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TextMeshProUGUI centerText;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private GameObject optionPanel;
    private GameObject option;

    private Canvas uiCanvas;

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
            option = Instantiate(optionPanel, )
        }
    }
}
