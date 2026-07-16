using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] TextMeshProUGUI centerText;
    [SerializeField] TextMeshProUGUI timerText;

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
}
