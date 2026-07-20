using System;
using System.Collections;
using UnityEngine;

public abstract class MonsterWeapon : MonoBehaviour
{
    [SerializeField] public Transform attackPos;
    protected Vector2 baseAttackPos;
    protected Vector2 dir;

    protected float coolTime;
    protected WaitForSeconds wait;
    protected bool canAttack;
    public Func<Vector2> dirFunc;

    protected void Awake()
    {
        coolTime = 0.5f;
        wait = new WaitForSeconds(coolTime);
        canAttack = true;
        baseAttackPos = attackPos.localPosition;
    }

    public void AttackPosFlip(bool isFlip)
    {
        Vector2 currentPos = attackPos.localPosition;
        currentPos.x = isFlip ? baseAttackPos.x : -baseAttackPos.x;
        attackPos.localPosition = currentPos;
    }

    public void SetDirection(Vector2 dir)
    {
        this.dir = dir;
    }

    public abstract void Attack(int pattern);
    public abstract void StopAttack();

    protected IEnumerator AttackCoolTime()
    {
        yield return wait;

        canAttack = true;
    }

}
