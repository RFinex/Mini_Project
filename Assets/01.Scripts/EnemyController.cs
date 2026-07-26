using UnityEngine;

public abstract class EnemyController : MonoBehaviour
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

    protected Transform target;
    public Transform Target
    {
        get
        {
            return target;
        }
    }

    protected abstract void CheckFlip();

    public abstract void TakeDamage();

    protected abstract void Die();
}
