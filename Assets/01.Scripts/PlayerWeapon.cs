using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
    protected int damage;
    protected Vector2 basePos;

    protected float dir;
    [SerializeField] protected Transform attackPos;
    
    protected virtual void Awake()
    {
        basePos = attackPos.localPosition;
        dir = 1;
    }

    void Update()
    {
        Attack();
    }

    public void AttackPosDirection(bool isFlip)
    {
        Vector2 currentPos = attackPos.localPosition;
        currentPos.x = isFlip ? -basePos.x : basePos.x;
        attackPos.localPosition = currentPos;
    }

    public void GetDirection(float dir)
    {
        this.dir = dir;
    }

    protected abstract void Attack();
}
