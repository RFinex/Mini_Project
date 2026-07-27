using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image slotImg;

    private Trophy trophy;
    public Trophy _trophy
    {
        get
        {
            return trophy;
        }
        set
        {
            trophy = value;
            if (trophy != null)
            {
                slotImg.sprite = trophy.trophyImg;
                slotImg.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                slotImg.color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
