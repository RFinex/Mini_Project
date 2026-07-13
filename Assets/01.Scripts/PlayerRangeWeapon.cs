using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRangeWeapon : PlayerWeapon
{
    protected override void Awake()
    {
        base.Awake();
        damage = 1;
    }

    protected override void Attack()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {

        }
    }
}
