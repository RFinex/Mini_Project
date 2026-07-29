using DG.Tweening;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private bool isQuitting = false;

    [Header("Setting")]
    [SerializeField] private float topContactCollisionValue = -0.9f;
    
    [Header("Tween Setting")]
    [SerializeField] private Vector3 dir;
    [SerializeField] private float delay;
    [SerializeField] private Ease ease;
    [SerializeField] private LoopType loopType;


    private Vector3 startPos;
    private Vector3 endPos;

    [Header("Passive Platform Check")]
    [SerializeField] private bool isPassive = false;

    [Header("Only Passive Platform Setting")]
    // 올라탈 때 속도, 돌아갈 때 속도
    [SerializeField] private float passiveSpeed;
    [SerializeField] private float passiveSpeed2;

    private Tween moveTween;
    private Tween moveTween2;

    private Rigidbody2D rb;

    private Transform playerPos;
    private Vector2 lastPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        startPos = transform.position;
        endPos = transform.position + dir;

        lastPos = rb.position;

        if (!isPassive)
        {
            rb.DOMove(dir, delay).SetRelative()
                .SetLoops(-1, loopType)
                .SetLink(gameObject)
                .SetEase(ease);
        }
    }

    private void FixedUpdate()
    {        
        Vector2 currentPos = rb.position;
                
        Vector2 aPos = currentPos - lastPos;

        // 플랫폼 위치 변화량 만큼 플레이어도 이동
        if (playerPos != null && aPos != Vector2.zero)
        {
            playerPos.position += (Vector3)aPos;
        }

        lastPos = currentPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            // 충돌체 -> 자신에게 뻗는 벡터 정보 가져오기
            Vector2 contact = collision.GetContact(0).normal;
            if (contact.y < topContactCollisionValue)
            {
                //collision.transform.SetParent(transform);
                playerPos = collision.transform;

                if (isPassive)
                {
                    moveTween?.Kill();
                    moveTween2?.Kill();

                    moveTween = rb.DOMove(endPos, passiveSpeed)
                        .SetSpeedBased()
                        .SetLink(gameObject)
                        .SetEase(ease);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isQuitting)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (gameObject.activeInHierarchy)
            {
                if(playerPos == collision.transform)
                    playerPos = null;
            }

            if (isPassive)
            {
                moveTween?.Kill();
                moveTween2?.Kill();

                moveTween2 = rb.DOMove(startPos, passiveSpeed2)
                    .SetSpeedBased()
                    .SetLink(gameObject)
                    .SetEase(ease);
            }
        }
    }

    private void OnDestroy()
    {
        isQuitting = true;
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
}
