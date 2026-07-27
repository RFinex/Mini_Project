using System;
using UnityEngine;

[Serializable]
public class Trophy
{
    public int id;
    public string name;
    public TrophyType type;
    public bool isCollect;
    public Sprite trophyImg;

    public Trophy(int id, string name, TrophyType type, bool isCollect, Sprite trophyImg)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.isCollect = isCollect;
        this.trophyImg = trophyImg;
    }

    public Trophy Clone()
    {
        return new Trophy(id, name, type, isCollect, trophyImg);
    }
}
