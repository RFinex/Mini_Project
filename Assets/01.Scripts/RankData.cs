using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RankData
{
    public float clearTime;
}

[Serializable]
public class BestRankingList
{
    public List<RankData> bestRank = new List<RankData>();
}