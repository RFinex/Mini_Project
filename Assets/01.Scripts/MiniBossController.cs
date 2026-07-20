using UnityEngine;

public class MiniBossController : EnemyController
{
    [SerializeField] private Transform attackPos;
    private Vector2 baseAttackPos;

    private StateMachine<MiniBossController> stateMachine;
    private MiniBossNormalAttackState normalAttackState;
    private MiniBossHeavyAttackState heavyAttackState;

    protected override void Awake()
    {
        base.Awake();
        speed = 3f;
        maxHp = 100;
        nowHp = maxHp;
        baseAttackPos = attackPos.localPosition;

        stateMachine = new StateMachine<MiniBossController>();
        normalAttackState = new MiniBossNormalAttackState();
        heavyAttackState = new MiniBossHeavyAttackState();
    }

    private void OnEnable()
    {
        maxHp = 100;
        nowHp = maxHp;
    }

    protected override void CheckFlip()
    {
        base.CheckFlip();
        Vector2 currentPos = attackPos.localPosition;
        currentPos.x = sr.flipX ? -baseAttackPos.x : baseAttackPos.x;
        attackPos.localPosition = currentPos;
    }
}
