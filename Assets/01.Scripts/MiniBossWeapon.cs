using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniBossWeapon : MonsterWeapon
{
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

    // 나중에 리스트로 정리 해볼 예정
    private IEnumerator Pattern_0()
    {
        Debug.Log("패턴0 실행");
        angle = 30f;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitUntil(() => canAttack);
            for (int j = -1; j <= 1; j++)
            {
                GameObject bullet = ObjectPoolManager.instance.GetObject(ConstString.minibossBullet);
                bullet.transform.position = attackPos.position;
                if (bullet.TryGetComponent<MiniBossBullet>(out MiniBossBullet bul))
                {
                    Quaternion rotate = Quaternion.Euler(0, 0, j * angle);
                    dir = dirFunc.Invoke();
                    bulDir = rotate * dir;
                    bul.SetDirection(bulDir);
                }
                yield return null;
            }
            canAttack = false;
            StartCoroutine(AttackCoolTime());
        }
    }
    private IEnumerator Pattern_1()
    {
        Debug.Log("패턴1 실행");
        angle = 15f;

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 360 / angle; j++)
            {
                GameObject bullet = ObjectPoolManager.instance.GetObject(ConstString.minibossBullet);
                bullet.transform.position = attackPos.position;
                if (bullet.TryGetComponent<MiniBossBullet>(out MiniBossBullet bul))
                {
                    Quaternion rotate = Quaternion.Euler(0f, 0f, j * angle);
                    bulDir = rotate * dir;
                    bul.SetDirection(bulDir);
                }
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
        
    }
    private IEnumerator Pattern_2()
    {
        Debug.Log("패턴2 실행");
        for (int i = 0; i < 20; i++)
        {
            GameObject bullet = ObjectPoolManager.instance.GetObject(ConstString.minibossBullet);
            bullet.transform.position = attackPos.position;
            angle = UnityEngine.Random.Range(-45f, 45f);
            if (bullet.TryGetComponent<MiniBossBullet>(out MiniBossBullet bul))
            {
                Quaternion rotate = Quaternion.Euler(0f, 0f, angle);
                dir = dirFunc.Invoke();
                bulDir = rotate * dir;
                bul.SetDirection(bulDir);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
