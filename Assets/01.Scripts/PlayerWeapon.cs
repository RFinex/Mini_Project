using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
    [SerializeField] protected int damage;
    protected Vector2 baseAttackPos;

    [SerializeField] protected float dir;
    [SerializeField] protected Transform attackPos;
    
    protected void Awake()
    {
        baseAttackPos = attackPos.localPosition;
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
