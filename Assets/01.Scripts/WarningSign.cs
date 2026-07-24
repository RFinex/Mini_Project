using DG.Tweening;
using System.Collections;
using UnityEngine;

public class WarningSign : MonoBehaviour, IPoolable
{
    [SerializeField] private float delay = 4f;
    private Collider2D col;
    private GameObject warning;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        warning = transform.GetChild(0).gameObject;
    }
    private void OnEnable()
    {
        col.enabled = false;
        StopAllCoroutines();

        warning.transform.DOKill();

        warning.transform.localScale = Vector3.zero;

        WarningDelay();
    }

    private void OnDisable()
    {
        warning.transform.DOKill();
        StopAllCoroutines();
    }

    private void WarningDelay()
    {
        warning.transform.DOKill();

        warning.transform.DOScale(1f, delay)
            .SetLink(gameObject)
            .SetEase(Ease.Linear)
            .OnComplete(() => StartCoroutine(Attack()));
    }

    private IEnumerator Attack()
    {
        col.enabled = true;

        yield return new WaitForSeconds(0.05f);

        EffectManager.instance.ShowExplodeEffect(transform.position);

        yield return null;

        ReturnPool();
    }

    public void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject("warningSign", this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            collision.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
