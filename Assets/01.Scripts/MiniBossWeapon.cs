using UnityEngine;
using System.Collections;

public class MiniBossWeapon : MonsterWeapon
{
    private float angle;
    private Vector2 bulDir;

    public override void Attack(int pattern)
    {
        switch (pattern)
        {
            case 0:
                StopAllCoroutines();
                StartCoroutine(Pattern_0());
                break;
            case 1:
                StopAllCoroutines();
                StartCoroutine(Pattern_1());
                break;
            case 2:
                StopAllCoroutines();
                StartCoroutine(Pattern_2());
                break;
            default:
                break;
        }
    }

    public override void StopAttack()
    {
        StopAllCoroutines();
    }

    private IEnumerator Pattern_0()
    {
        angle = 30f;

        for (int i = -1; i <= 1; i++)
        {
            GameObject bullet = ObjectPoolManager.instance.GetObject(ConstString.minibossBullet);
            bullet.transform.position = attackPos.position;
            if (bullet.TryGetComponent<MiniBossBullet>(out MiniBossBullet bul))
            {
                Quaternion rotate = Quaternion.Euler(0, 0, i * angle);
                bulDir = rotate * dir;
                bul.SetDirection(bulDir);
            }
            yield return null;
        }

        Debug.Log("패턴0 실행");
    }
    private IEnumerator Pattern_1()
    {
        angle = 15f;

        for (int i = 0; i < 360 / angle; i++)
        {
            GameObject bullet = ObjectPoolManager.instance.GetObject(ConstString.minibossBullet);
            bullet.transform.position = attackPos.position;
            if (bullet.TryGetComponent<MiniBossBullet>(out MiniBossBullet bul))
            {
                Quaternion rotate = Quaternion.Euler(0f, 0f, i * angle);
                bulDir = rotate * dir;
                bul.SetDirection(bulDir);
            }
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("패턴1 실행");
    }
    private IEnumerator Pattern_2()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject bullet = ObjectPoolManager.instance.GetObject(ConstString.minibossBullet);
            bullet.transform.position = attackPos.position;
            angle = Random.Range(-45f, 45f);
            if (bullet.TryGetComponent<MiniBossBullet>(out MiniBossBullet bul))
            {
                Quaternion rotate = Quaternion.Euler(0f, 0f, i * angle);
                bulDir = rotate * dir;
                bul.SetDirection(bulDir);
            }
            yield return new WaitForSeconds(0.2f);
        }
        Debug.Log("패턴2 실행");
    }
}
