using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class RankingPopup : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject rankPanel;
    [SerializeField] private Button closeBtn;

    private void Awake()
    {
        closeBtn.onClick.AddListener(UIManager.instance.CloseRankPopup);
    }
    public void UpdateRankPanel()
    {
        foreach (Transform panel in content)
        {
            Destroy(panel.gameObject);
        }

        List<RankData> rankList = DataManager.instance.GetRank();

        for (int i = 0; i < rankList.Count; i++)
        {
            GameObject panel = Instantiate(rankPanel, content);

            RankSlot slot = panel.GetComponent<RankSlot>();
            if (slot != null)
            {
                slot.SetRankText(i + 1, rankList[i].clearTime);
            }

        }
    }
}
