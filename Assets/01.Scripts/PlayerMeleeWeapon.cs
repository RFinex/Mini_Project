using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeWeapon : PlayerWeapon
{
    private float delay;
    private float startAngle = 60;
    private float endAngle = -60;

    bool isAttack;

    protected override void Awake()
    {
        base.Awake();
        isAttack = false;
        delay = 1f;
    }

    void Update()
    {
        
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
        isAttack = true;

        transform.localRotation = Quaternion.Euler(0f, 0f, startAngle);

        transform.DORotate(new Vector3(0f, 0f, endAngle), delay)
            .SetEase(Ease.OutCubic)
            .SetLink(gameObject);

    }
}
