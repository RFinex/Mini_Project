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
            SoundManager.instance.PlaySFX(SFXType.Shoot);
            GameObject bullet = ObjectPoolManager.instance.GetObject("playerBullet");
            bullet.transform.position = attackPos.position;
            PlayerBullet bCom = bullet.GetComponent<PlayerBullet>();
            bCom.SetDirection(dir);
            bCom.SetDamage(damage);
        }
    }
}
