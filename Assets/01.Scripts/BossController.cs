using UnityEngine;

public class BossController : MonoBehaviour
{
    private int maxHp = 100;
    private int nowHp;

    private float speed;
    private Transform target;

    private SpriteRenderer sr;

    [SerializeField] private Transform attackPos;
    private Vector2 baseAttackPos;

    private StateMachine<BossController> stateMachine;
    private BossPhase1State bossPhase1;
    private BossPhase2State bossPhase2;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        speed = 3f;
        nowHp = maxHp;
        baseAttackPos = attackPos.localPosition;

        stateMachine = new StateMachine<BossController>();
        bossPhase1 = new BossPhase1State();
        bossPhase2 = new BossPhase2State();

        target = GameObject.Find("Player").transform;
    }

    private void OnEnable()
    {
        maxHp = 100;
        nowHp = maxHp;
    }

    private void Update()
    {
        CheckFlip();
    }

    private void CheckFlip()
    {
        sr.flipX = transform.position.x > target.position.x ? true : false;
        Vector2 currentPos = attackPos.localPosition;
        currentPos.x = sr.flipX? -baseAttackPos.x : baseAttackPos.x;
        attackPos.localPosition = currentPos;
    }
}
