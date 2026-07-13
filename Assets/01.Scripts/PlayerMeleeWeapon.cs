using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeWeapon : PlayerWeapon
{
    private float delay;
    private float startAngle = 60;
    private float endAngle = -60;

    bool isAttack;
    private Tween swingTween;

    protected override void Awake()
    {
        base.Awake();
        isAttack = false;
        delay = 0.2f;
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

        attackPos.localRotation = Quaternion.Euler(0f, 0f, startAngle);

        swingTween = attackPos.DORotate(new Vector3(0f, 0f, endAngle), 0.2f)
            .SetEase(Ease.OutExpo)
            .SetLink(gameObject)
            .OnComplete(() => attackPos.localRotation = Quaternion.identity);

    }
}
