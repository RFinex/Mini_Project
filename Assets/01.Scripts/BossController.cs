using UnityEngine;

public class BossController : EnemyController
{   
    [SerializeField] private Transform attackPos;
    private Vector2 baseAttackPos;

    private StateMachine<BossController> stateMachine;
    private BossPhase1State bossPhase1;
    private BossPhase2State bossPhase2;

    protected override void Awake()
    {
        base.Awake();
        speed = 3f;
        maxHp = 100;
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

    protected override void CheckFlip()
    {
        sr.flipX = transform.position.x > target.position.x ? true : false;
        Vector2 currentPos = attackPos.localPosition;
        currentPos.x = sr.flipX? -baseAttackPos.x : baseAttackPos.x;
        attackPos.localPosition = currentPos;
    }
}
