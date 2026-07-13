using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeWeapon : PlayerWeapon
{
    private float motionDelay;
    private float coolTime;
    [SerializeField] private float timer;
    private float angle = -120;

    bool canAttack;
    private Tween swingTween;

    WaitForSeconds waitCool;

    private TrailRenderer tr;

    protected override void Awake()
    {
        base.Awake();
        damage = 5;
        canAttack = true;
        motionDelay = 0.2f;
        tr = GetComponent<TrailRenderer>();
        tr.emitting = false;
        coolTime = 0.5f;
        timer = 0;
        waitCool = new WaitForSeconds(coolTime);
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
                tr.emitting = true;
                canAttack = false;
                StopAllCoroutines();
                StartCoroutine(AttackCoolTime());
            })
            .OnComplete(() =>
            {
                attackPos.localRotation = Quaternion.identity;
                tr.emitting = false;
            });
    }

    private IEnumerator AttackCoolTime()
    {
        yield return waitCool;

        canAttack = true;        
    }

}
