using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
    protected int damage;
    protected Vector2 basePos;

    protected float dir;
    [SerializeField] protected Transform attackPos;
    
    protected virtual void Awake()
    {
        basePos = transform.localPosition;
        dir = 1;
    }

    void Update()
    {
        Attack();
    }

    public void AttackPosDirection(bool isFlip)
    {
        Vector2 currentPos = transform.localPosition;
        currentPos.x = isFlip ? -basePos.x : basePos.x;
        transform.localPosition = currentPos;
    }

    public void GetDirection(float dir)
    {
        this.dir = dir;
    }

    protected abstract void Attack();
}
