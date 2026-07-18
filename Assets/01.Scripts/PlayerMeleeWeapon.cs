using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeWeapon : PlayerWeapon
{
    private float motionDelay;
    private float coolTime;
    private float angle = -120;

    bool canAttack;
    private Tween swingTween;

    WaitForSeconds waitCool;

    protected override void Awake()
    {
        base.Awake();
        damage = 5;
        canAttack = true;
        motionDelay = 0.2f;
        coolTime = 0.5f;
        waitCool = new WaitForSeconds(coolTime);
    }

    protected void OnEnable()
    {
        canAttack = true;
    }

    protected override void Attack()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            Swing();
        }
    }

    protected void Swing()
    {
        if (!canAttack)
            return;

        swingTween?.Kill();

        attackPos.localRotation = Quaternion.identity;

        float rAngle = attackPos.localPosition.x < 0 ? -angle : angle;

        swingTween = attackPos.DORotate(new Vector3(0f, 0f, rAngle), motionDelay)
            .SetEase(Ease.OutExpo)
            .SetLink(gameObject)
            .OnStart(() =>
            {
                canAttack = false;
                StopAllCoroutines();
                StartCoroutine(AttackCoolTime());
            })
            .OnComplete(() => attackPos.localRotation = Quaternion.identity);
    }

    private IEnumerator AttackCoolTime()
    {
        yield return waitCool;

        canAttack = true;        
    }

}
