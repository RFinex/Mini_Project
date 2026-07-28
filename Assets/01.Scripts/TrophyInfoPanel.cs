using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrophyInfoPanel : MonoBehaviour
{
    [Header("Panel UI")]
    [SerializeField] private Image trophyImage;
    [SerializeField] private TextMeshProUGUI trophyName;

    [Header("Image Color")]
    [SerializeField] private Color collectColor;
    [SerializeField] private Color notCollectColor;

    [SerializeField] private float popupSize;
    [SerializeField] private float openSize;
    [SerializeField] private float closeSize;
    [SerializeField] private float popupDelay;

    private Sequence seq;

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
        seq.Append(transform.DOScale(popupSize, popupDelay))
            .Append(transform.DOScale(openSize, popupDelay))
            .SetLink(gameObject, LinkBehaviour.KillOnDisable);
    }

    private void PopupClose()
    {
        seq?.Kill();

        seq = DOTween.Sequence();
        seq.Append(transform.DOScale(popupSize, popupDelay))
            .Append(transform.DOScale(closeSize, popupDelay))
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
