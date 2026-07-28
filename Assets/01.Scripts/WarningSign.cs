using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningSign : MonoBehaviour, IPoolable
{
    [SerializeField] private float delay;
    [SerializeField] private float goalSize;
    [SerializeField] private float attackDelay;

    private Collider2D col;
    private GameObject warning;

    private Tween scaleTween;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        warning = transform.GetChild(0).gameObject;
    }
    private void OnEnable()
    {
        col.enabled = false;
        StopAllCoroutines();

        scaleTween?.Kill();

        warning.transform.localScale = Vector3.zero;

        SceneManager.sceneLoaded += ReturnPool;

        WarningDelay();
    }

    protected void ReturnPool(Scene scene, LoadSceneMode mode)
    {
        ReturnPool();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ReturnPool;
        scaleTween?.Kill();
        StopAllCoroutines();
    }

    private void WarningDelay()
    {
        scaleTween?.Kill();

        scaleTween = warning.transform.DOScale(goalSize, delay)
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
        ObjectPoolManager.instance.ReturnObject(ConstString.warningSign, this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            collision.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
