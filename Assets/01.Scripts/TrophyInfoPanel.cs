using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrophyInfoPanel : PopupBase
{
    [Header("Panel UI")]
    [SerializeField] private Image trophyImage;
    [SerializeField] private TextMeshProUGUI trophyName;

    [Header("Image Color")]
    [SerializeField] private Color collectColor;
    [SerializeField] private Color notCollectColor;

    private Trophy trophy;

    public void OpenPanel(int id)
    {
        PopupOpen();
        GetTrophyData(id);
    }

    public void ClosePanel()
    {
        PopupClose();
    }

    private void PopupOpen()
    {
        seq?.Kill();

        seq = DOTween.Sequence();
        seq.Append(transform.DOScale(data.popupSize, data.popupDelay))
            .Append(transform.DOScale(data.openSize, data.popupDelay))
            .SetLink(gameObject, LinkBehaviour.KillOnDisable);
    }

    private void PopupClose()
    {
        seq?.Kill();

        seq = DOTween.Sequence();
        seq.Append(transform.DOScale(data.popupSize, data.popupDelay))
            .Append(transform.DOScale(data.closeSize, data.popupDelay))
            .SetLink(gameObject, LinkBehaviour.KillOnDisable)
            .OnComplete(() => gameObject.SetActive(false));
    }
    
    public void GetTrophyData(int id)
    {
        trophy = DataManager.instance.GetTrophyData(id);

        if (trophy == null)
        {
            Debug.Log("Null Data");
            return;
        }

        UpdateTrophyInfo();
    }

    private void UpdateTrophyInfo()
    {
        if (trophyImage != null)
        {
            if (trophy.trophyImg != null)
            {
                trophyImage.sprite = trophy.trophyImg;

                trophyImage.color = trophy.isCollect ? collectColor : notCollectColor;
            }
        }
        
        if (trophyName != null)
        {
            trophyName.text = trophy.isCollect ? trophy.name : "???";
        }
    }
}
