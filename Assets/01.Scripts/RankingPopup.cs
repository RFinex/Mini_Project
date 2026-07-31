using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingPopup : PopupBase
{
    [Header("UI")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject rankPanel;
    [SerializeField] private Button closeBtn;

    private void Awake()
    {
        closeBtn.onClick.AddListener(ClosePopup);
    }

    public void Open()
    {
        OpenPopup();
        UpdateRankPanel();
    }

    public void OpenPopup()
    {
        seq?.Kill();

        seq = DOTween.Sequence();
        seq.Append(transform.DOScale(data.popupSize, data.popupDelay))
            .Append(transform.DOScale(data.openSize, data.popupDelay))
            .SetLink(gameObject, LinkBehaviour.KillOnDisable);
    }

    public void ClosePopup()
    {
        seq?.Kill();

        seq = DOTween.Sequence();
        seq.Append(transform.DOScale(data.popupSize, data.popupDelay))
            .Append(transform.DOScale(data.closeSize, data.popupDelay))
            .SetLink(gameObject, LinkBehaviour.KillOnDisable)
            .OnComplete(() => UIManager.instance.CloseRankPopup());
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
