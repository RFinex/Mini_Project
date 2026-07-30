using UnityEngine;

public class BossPatternDataSO : ScriptableObject
{
    [Header("Base Setting")]
    [SerializeField] protected float delayFrame = (45f / 60f);
    [SerializeField] protected float delay2Frame = (20f / 60f);
    [SerializeField] protected float angle;
    [SerializeField] protected int attackCount;
}
