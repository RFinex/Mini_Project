using UnityEngine;
using System.Collections.Generic;

public enum TrophyType
{
    Bronze,
    Silver,
    Gold
}

[CreateAssetMenu(fileName =  "TrophyData", menuName = "Data/Trophy")]
public class TrophyData : ScriptableObject
{
    public List<Trophy> trophy = new List<Trophy>();
}