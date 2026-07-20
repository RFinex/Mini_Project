using UnityEngine;

public abstract class MonsterWeapon : MonoBehaviour
{
    [SerializeField] private Transform attackPos;
    private Vector2 dir;

    public void SetDirection(Vector2 dir)
    {
        this.dir = dir;
    }

    public abstract void Attack();
}
