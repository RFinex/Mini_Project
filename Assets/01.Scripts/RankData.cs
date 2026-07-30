using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RankData
{
    public float clearTime;
}

[Serializable]
public class Ranking
{
    public List<RankData> bestRank = new List<RankData>();
}