using UnityEngine;
using System.Collections.Generic;

public class RankingPopup : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject rankPanel;

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
