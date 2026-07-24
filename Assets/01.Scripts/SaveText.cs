using DG.Tweening;
using TMPro;
using UnityEngine;

public class SaveText : MonoBehaviour, IPoolable
{
    private TextMeshProUGUI text;
    private Sequence seq;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    
    public void FadeText(Vector3 pos, Transform canvas)
    {
        transform.SetParent(canvas);
        transform.position = pos;
        text.alpha = 1f;

        seq?.Kill();

        seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(transform.position.y + 1f, 1.5f))
            .Join(text.DOFade(0f, 1.5f))
            .SetLink(gameObject, LinkBehaviour.KillOnDisable)
            .OnComplete(() => ReturnPool());
    }

    public void ReturnPool()
    {
        transform.SetParent(ObjectPoolManager.instance.transform);
        ObjectPoolManager.instance.ReturnObject(ConstString.SaveCheckText, gameObject);
    }    
}
