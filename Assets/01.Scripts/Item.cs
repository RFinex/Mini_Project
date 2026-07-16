using UnityEngine;
using System.Collections;
using DG.Tweening;

public abstract class Item : MonoBehaviour
{
    protected float delay = 5f;
    protected WaitForSeconds wait;
    protected Collider2D col;
    protected SpriteRenderer sr;

    protected void Start()
    {
        transform.DOMoveY(0.1f, 2f)
            .SetRelative()
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(gameObject)
            .SetEase(Ease.InOutCubic);
    }

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    protected virtual IEnumerator ItemRespawn()
    {
        col.enabled = false;
        sr.color = new Color(1, 1, 1, 0.2f);

        yield return wait;

        col.enabled = true;
        sr.color = new Color(1, 1, 1, 1);
    }
}
