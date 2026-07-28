using UnityEngine;
using UnityEngine.UI;

public class MenuInit : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;

    [SerializeField] private Button startBtn;
    [SerializeField] private Button loadBtn;

    private void Start()
    {
        UIManager.instance.Init_Menu(menuCanvas, startBtn, loadBtn);
    }
}
