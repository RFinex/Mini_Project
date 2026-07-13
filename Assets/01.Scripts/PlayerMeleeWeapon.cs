using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeWeapon : PlayerWeapon
{
    private float delay;
    private float angle = -120;

    bool isAttack;
    private Tween swingTween;

    private TrailRenderer tr;

    protected override void Awake()
    {
        base.Awake();
        damage = 5;
        isAttack = false;
        delay = 0.2f;
        tr = GetComponent<TrailRenderer>();
        tr.emitting = false;
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
        swingTween?.Kill();

        attackPos.localRotation = Quaternion.identity;

        float rAngle = attackPos.localPosition.x < 0 ? -angle : angle;

        swingTween = attackPos.DORotate(new Vector3(0f, 0f, rAngle), delay)
            .SetEase(Ease.OutExpo)
            .SetLink(gameObject)
            .OnStart(() => tr.emitting = true)
            .OnComplete(() =>
            {
                attackPos.localRotation = Quaternion.identity;
                tr.emitting = false;
            });

    }
}
