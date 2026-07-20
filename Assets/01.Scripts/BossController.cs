using UnityEngine;

public class BossController : MonoBehaviour
{
    private int maxHp = 100;
    private int nowHp;

    private float moveSpeed;
    private Transform target;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        moveSpeed = 3f;
        nowHp = maxHp;

        target = GameObject.Find("Player").transform;
    }

    private void OnEnable()
    {
        maxHp = 100;
        nowHp = maxHp;
    }

    private void Update()
    {
        CheckFlip();
    }

    private void CheckFlip()
    {
        sr.flipX = transform.position.x > target.position.x ? true : false;
    }
}
