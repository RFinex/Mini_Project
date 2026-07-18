using System;
using UnityEngine;

[Serializable]
public class Trophy
{
    public int id;
    public string name;
    public TrophyType type;
    public bool isCollect;

    public Trophy(int id, string name, TrophyType type, bool isCollect)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.isCollect = isCollect;
    }

    public Trophy Clone()
    {
        return new Trophy(id, name, type, isCollect);
    }
}
