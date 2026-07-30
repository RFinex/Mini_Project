using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Player Base Stats")]
    public float baseSpeed = 5f;
    public float jumpPower = 12f;
    public int jumpCountMax = 2;

    [Header("Tempory Stats")]
    public float dashSpeed = 5f;
    public float dashDelay = 0.2f;
    public float launchSpeed = 3f;

    [Header("Ground Check Setting")]
    public LayerMask groundLayer;
    public Vector2 boxSize = new Vector2(0.85f, 0.1f);
    public float boxOffset = -0.08f;
    public float boxDistance = 0.55f;

    [Header("Criterion")]
    public float criterionVelocity = 0.1f;
}
