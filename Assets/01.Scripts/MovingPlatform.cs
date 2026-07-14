using DG.Tweening;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private bool isQuitting = false;
    
    // 이동 거리, 이동 시간, 이동 방식
    [SerializeField] private Vector3 dir;
    [SerializeField] private float delay;
    [SerializeField] private Ease ease;

    [SerializeField] private bool isPassive = false;

    private Vector3 startPos;
    private Vector3 endPos;

    private Tween moveTween;
    private Tween moveTween2;

    void Start()
    {
        startPos = transform.position;
        endPos = transform.position + dir;
        if (!isPassive)
        {
            transform.DOMove(dir, delay).SetRelative()
                .SetLoops(-1, loopType: LoopType.Yoyo)
                .SetLink(gameObject)
                .SetEase(ease);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector2 contact = collision.GetContact(0).normal;
            if (contact.y < -0.9f)
            {
                collision.gameObject.transform.SetParent(transform);
                if (isPassive)
                {

                }

            }
        }
    }
}
