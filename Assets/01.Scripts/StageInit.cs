using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageInit : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private Canvas worldCanvas;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI centerText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("UI")]
    [SerializeField] private Button restartBtn;
    [SerializeField] private Slider bossHpSlider;

    [Header("Object")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform miniBossExit;

    private void Start()
    {
        UIManager.instance.Init_InGame(uiCanvas, worldCanvas, centerText, timerText, restartBtn, bossHpSlider);

        GameManager.instance.PlayerInit(player);

        StageManager.instance.Init(miniBossExit);
    }

}
