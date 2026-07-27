using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;

public class TrophyUIPanel : MonoBehaviour, IPoolable
{
    [SerializeField] private float popupDelay;
    [SerializeField] private float closeDelay;
    [SerializeField] private TextMeshProUGUI trophyText;

    private void OnEnable()
    {
        transform.localScale = Vector3.one * 0.1f;
    }

    public void OpenPanel(string text)
    {
        SetUIText(text);

        PopupOpenNClose();
    }

    public void PopupOpenNClose()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(1.1f, popupDelay))
            .Append(transform.DOScale(1f, popupDelay))
            .AppendInterval(closeDelay)
            .Append(transform.DOScale(1.1f, popupDelay))
            .Append(transform.DOScale(0.1f, popupDelay))        
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
