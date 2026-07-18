using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public float CheckPointX;
    public float CheckPointY;
    public float CheckPointZ;

    public float elapsedTime;

    public List<int> collectTrophy = new List<int>();
}
