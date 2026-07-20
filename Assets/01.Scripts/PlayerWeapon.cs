using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
    protected int damage;
    protected Vector2 baseAttackPos;

    protected float dir;
    [SerializeField] protected Transform attackPos;
    
    protected virtual void Awake()
    {
        baseAttackPos = attackPos.localPosition;
        dir = 1f;
    }

    protected void Update()
    {
        Attack();
    }

    public void AttackPosDirection(bool isFlip)
    {
        Vector2 currentPos = attackPos.localPosition;
        currentPos.x = isFlip ? -baseAttackPos.x : baseAttackPos.x;
        attackPos.localPosition = currentPos;
    }

    public void GetDirection(float dir)
    {
        this.dir = dir;
    }

    protected abstract void Attack();
}
