using UnityEngine;

public enum MonsterState
{
    Idle,
    Trace,
    Attack,
    Die
}

public abstract class EnemyController : MonoBehaviour, IPoolable
{
    [SerializeField] protected int maxHp;
    public int MaxHp
    {
        get
        {
            return maxHp;
        }
    }

    [SerializeField] protected int nowHp;

    public int NowHp
    {
        get
        {
            return nowHp;
        }
        private set
        {
            nowHp = value;
        }
    }

    [SerializeField] protected float speed;
    public float Speed
    {
        get
        {
            return speed;
        }
        private set
        {
            speed = value;
        }
    }

    public Transform Target
    {
        get
        {
            return StageManager.instance != null ? StageManager.instance.PlayerPos : null;
        }
    }

    [SerializeField] protected float range;
    public float Range
    {
        get
        {
            return range;
        }
    }

    [SerializeField] protected float dieDelay;
    public float DieDelay
    {
        get
        {
            return dieDelay;
        }
    }

    [SerializeField] protected float attackDelay;
    public float AttackDelay
    {
        get
        {
            return attackDelay;
        }
    }

    [SerializeField] protected bool canMove;
    public bool CanMove
    {
        get
        {
            return canMove;
        }
    }

    protected virtual void ChangeState(IState<EnemyController> state)
    {

    }

    public virtual void ChangeState(MonsterState state)
    {

    }

    public void FlipSprite()
    {
        CheckFlip();
    }

    public virtual void SetAttackAnim(bool isAttacking)
    {

    }

    public virtual void Attack()
    {

    }

    protected abstract void CheckFlip();

    public abstract void TakeDamage();

    protected abstract void Die();

    public abstract Vector2 GetDirection();

    public abstract void ReturnPool();

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstString.Player))
        {
            collision.transform.GetComponent<PlayerController>().TakeDamage();
        }
    }

}
