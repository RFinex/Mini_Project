using UnityEngine;

public abstract class BossEnemyController : EnemyController
{    

    [SerializeField] protected float baseIdleTimer = 3f;

    [SerializeField] protected float idleTimer;
    public float IdleTimer
    {
        get
        {
            return idleTimer;
        }
        private set
        {
            idleTimer = value;
        }
    }
}
