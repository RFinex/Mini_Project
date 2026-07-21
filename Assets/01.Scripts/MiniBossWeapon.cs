using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MiniBossWeapon : MonsterWeapon
{
    private Vector2 bulDir;

    [SerializeField] protected List<Rect> laserArea;

    private Tween laserTween;

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

    public void HeavyAttack(int pattern)
    {
        switch (pattern)
        {
            case 0:
                StopAllCoroutines();
                StartCoroutine(Heavy_Pattern_0());
                break;
            case 1:
                StopAllCoroutines();
                StartCoroutine(Heavy_Pattern_1());
                break;
            case 2:
                StopAllCoroutines();
                StartCoroutine(Heavy_Pattern_2());
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

    private IEnumerator Heavy_Pattern_0()
    {
        Debug.Log("강력 패턴0 실행");
        for (int i = 0; i < 2; i++)
        {
            GameObject laser = ObjectPoolManager.instance.GetObject(ConstString.laser);
            laser.transform.position = attackPos.position;
            laser.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            yield return new WaitForSeconds(2f + (10f / 60f));
        }
        
    }
    private IEnumerator Heavy_Pattern_1()
    {
        Debug.Log("강력 패턴1 실행");
        angle = 180f;
        for (int i = 0; i < laserArea.Count; i++)
        {
            GameObject laser = ObjectPoolManager.instance.GetObject(ConstString.laser);
            laser.transform.position = laserArea[i].position;
            laser.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            yield return new WaitForSeconds(2f + (20f / 60f));
        }
    }
    private IEnumerator Heavy_Pattern_2()
    {
        Debug.Log("강력 패턴2 실행");
        GameObject laser = ObjectPoolManager.instance.GetObject(ConstString.laser);
        laser.transform.position = attackPos.position;

        yield return new WaitForSeconds(1f + (20f / 60f));

        laserTween = laser.transform.DORotate(new Vector3(0f, 0f, 30f), (50f/60f))
            .SetLink(laser)
            .SetEase(Ease.Linear);
    }

    protected void OnDrawGizmos()
    {
        if (laserArea == null)
            return;

        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

        foreach (var area in laserArea)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);

            Gizmos.DrawCube(center, size);
        }
    }
}
