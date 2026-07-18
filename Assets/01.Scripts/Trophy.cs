using UnityEngine;

public class Trophy
{
    public int trophyID;
    public TrophyType type;
    public bool isCollect;

    public Trophy(int trophyID, TrophyType type, bool isCollect)
    {
        this.trophyID = trophyID;
        this.type = type;
        this.isCollect = isCollect;
    }

    public Trophy Clone()
    {
        return new Trophy(trophyID, type, isCollect);
    }
}
