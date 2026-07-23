using UnityEngine;

public abstract class BossPatternBase : MonoBehaviour
{
    protected int randPattern;
    public bool isFinish;

    public void StartRandomPattern()
    {
        isFinish = false;
        randPattern = Random.Range(1, 5);

        switch (randPattern)
        {
            case 1:
                Pattern_1();
                break;
            case 2:
                Pattern_2();
                break;
            case 3:
                Pattern_3();
                break;
            case 4:
                Pattern_4();
                break;
        }
    }

    public abstract void Pattern_1();
    public abstract void Pattern_2();
    public abstract void Pattern_3();
    public abstract void Pattern_4();
}
