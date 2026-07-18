using System;
using UnityEngine;

[Serializable]
public class Trophy
{
    public int trophyID;
    public string name;
    public TrophyType type;
    public bool isCollect;

    public Trophy(int trophyID, string name, TrophyType type, bool isCollect)
    {
        this.trophyID = trophyID;
        this.name = name;
        this.type = type;
        this.isCollect = isCollect;
    }

    public Trophy Clone()
    {
        return new Trophy(trophyID, name, type, isCollect);
    }
}
