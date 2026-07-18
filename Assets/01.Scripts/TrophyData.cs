using UnityEngine;

public enum TrophyType
{
    Bronze,
    Silver,
    Gold
}

[CreateAssetMenu(fileName =  "TrophyData", menuName = "Data/Trophy")]
public class TrophyData : ScriptableObject
{
    public string trophyID;
    public TrophyType type;
    public bool isCollect;
}