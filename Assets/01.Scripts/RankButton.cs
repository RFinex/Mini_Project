using UnityEngine;
using UnityEngine.UI;

public class RankButton : MonoBehaviour
{
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();

        btn.onClick.AddListener(OpenRankPopup);
    }

    private void OpenRankPopup()
    {
        UIManager.instance.OpenRankPopup();
    }
}
