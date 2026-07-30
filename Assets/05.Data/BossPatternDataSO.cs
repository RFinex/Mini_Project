using UnityEngine;

[CreateAssetMenu(fileName = "BossPatternData", menuName = "Data/BossPattern")]
public class BossPatternDataSO : ScriptableObject
{
    [Header("Base Setting")]
    public float delayFrame = (45f / 60f);
    public float delay2Frame = (20f / 60f);

    [Header("Pattern_1 Setting")]
    public int p1_AttackCount = 10;

    [Header("Pattern_2 Setting")]
    public int p2_AttackCount = 5;
    public float p2_Angle = 30f;

    [Header("Pattern_3 Setting")]
    public int p3_AttackCount = 5;
    public int p3_BulCount = 5;
    public float p3_RandAngle = 30f;

    [Header("Pattern_4 Setting")]
    public int p4_AttackCount = 40;
    public float p4_Angle = 40f;
    public float p4_PatternDelay = 0.05f;

    [Header("Pattern_5 Setting")]
    public int p5_AttackCount = 6;
    public float p5_PatternDelay = 0.5f;

    [Header("Pattern_6 Setting")]
    public int p6_AttackCount = 2;
    public int p6_BulCount = 5;
    public float p6_Angle = 15;
    public float p6_PatternDelay = 0.5f;

    [Header("Pattern_7 Setting")]
    public int p7_AttackCount = 15;

    [Header("Pattern_8 Setting")]
    public int p8_AttackCount = 30;
    public int p8_BulCount = 4;
    public float p8_AngleOffset = 10;

    [Header("Pattern_9 Setting")]
    public int p9_AttackCount = 50;
    public float p9_PatternDelay = 0.1f;

    [Header("Pattern_10 Setting")]
    public int p10_AttackCount = 30;
    public int p10_BulCount = 4;
    public float p10_AngleOffset = 10f;
    public float p10_PatternDelay = 0.2f;

    [Header("Pattern_11 Setting")]
    public int p11_AttackCount = 60;
    public float p11_BulSpeed = 16f;
    public float p11_PatternDelay = 2f;
    public float p11_PatternDelay2 = 0.05f;

    [Header("Pattern_12 Setting")]
    public int p12_AttackCount = 20;
    public int p12_BulCount = 12;
    public int p12_BulCount2 = 6;
    public float p12_Angle = 30f;
    public float p12_LaserAngle = -90f;
    public float p12_AngleOffset = 15f;
    public float p12_PatternDelay = 0.05f;
    public float p12_PatternDelay2 = 0.2f;
    public float p12_PatternDelay3 = 0.8f;
}
