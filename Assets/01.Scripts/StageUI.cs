using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private Canvas worldCanvas;

    [SerializeField] private TextMeshProUGUI centerText;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private Button restartBtn;
    [SerializeField] private Slider bossHpSlider;

    private void Start()
    {
        UIManager.instance.Init_InGame(uiCanvas, worldCanvas, centerText, timerText, restartBtn, bossHpSlider);
    }

}
