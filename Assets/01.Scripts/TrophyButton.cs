using UnityEngine;
using UnityEngine.UI;

public class TrophyButton : MonoBehaviour
{
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();

        btn.onClick.AddListener(OpenTrophyInv);
    }

    private void OpenTrophyInv()
    {
        UIManager.instance.OpenTrophyInv();
    }

}
