using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;

public class TrophyUIPanel : MonoBehaviour, IPoolable
{
    [SerializeField] private float popupSize;
    [SerializeField] private float openSize;
    [SerializeField] private float closeSize;

    [SerializeField] private float popupDelay;
    [SerializeField] private float closeDelay;
    [SerializeField] private TextMeshProUGUI trophyText;

    private void OnEnable()
    {
        transform.localScale = Vector3.one * closeSize;
    }

    public void OpenPanel(string text)
    {
        SetUIText(text);

        PopupOpenNClose();
    }

    public void PopupOpenNClose()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(popupSize, popupDelay))
            .Append(transform.DOScale(openSize, popupDelay))
            .AppendInterval(closeDelay)
            .Append(transform.DOScale(popupSize, popupDelay))
            .Append(transform.DOScale(closeSize, popupDelay))        
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
