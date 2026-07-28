using DG.Tweening;
using TMPro;
using UnityEngine;

public class SaveText : MonoBehaviour, IPoolable
{
    private TextMeshProUGUI text;
    private Sequence seq;

    [Header("Text Alpha Set")]
    [SerializeField] private float baseAlpha;
    [SerializeField] private float resultAlpha;

    [Header("DOTween Set")]
    [SerializeField] private float moveDistance;
    [SerializeField] private float fadeDelay;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    
    public void FadeText(Vector3 pos, Transform canvas)
    {
        transform.SetParent(canvas);
        transform.position = pos;
        text.alpha = baseAlpha;

        seq?.Kill();

        seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(transform.position.y + moveDistance, fadeDelay))
            .Join(text.DOFade(resultAlpha, fadeDelay))
            .SetLink(gameObject, LinkBehaviour.KillOnDisable)
            .OnComplete(() => ReturnPool());
    }

    public void ReturnPool()
    {
        transform.SetParent(ObjectPoolManager.instance.transform);
        ObjectPoolManager.instance.ReturnObject(ConstString.SaveCheckText, gameObject);
    }    
}
