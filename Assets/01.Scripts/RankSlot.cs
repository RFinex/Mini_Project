using TMPro;
using UnityEngine;

public class RankSlot : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI rankNumText;
    [SerializeField] private TextMeshProUGUI rankTimeText;

    public void SetRankText(int rankNum, float time)
    {
        rankNumText.text = rankNum.ToString();

        rankTimeText.text = $"{(int)time / 3600:D2} : {(int)time / 60 % 60:D2} : {(int)time % 60:D2}";
    }
}
