using UnityEngine;
using DG.Tweening;
using TMPro;

public class TrophyUIPanel : PopupBase, IPoolable
{
    [SerializeField] private float closeDelay;
    [SerializeField] private TextMeshProUGUI trophyText;
    
    public void OpenPanel(string text)
    {
        SetUIText(text);

        PopupOpenNClose();
    }

    public void PopupOpenNClose()
    {
        seq?.Kill();

        seq = DOTween.Sequence();
        seq.Append(transform.DOScale(data.popupSize, data.popupDelay))
            .Append(transform.DOScale(data.openSize, data.popupDelay))
            .AppendInterval(closeDelay)
            .Append(transform.DOScale(data.popupSize, data.popupDelay))
            .Append(transform.DOScale(data.closeSize, data.popupDelay))        
            .SetLink(gameObject, LinkBehaviour.KillOnDisable)
            .OnComplete(ReturnPool);
    }

    public void SetUIText(string text)
    {
        trophyText.text = text;
    }

    public void ReturnPool()
    {
        transform.SetParent(ObjectPoolManager.instance.transform);
        ObjectPoolManager.instance.ReturnObject(ConstString.TrophyGetPanel, this.gameObject);
    }
}
