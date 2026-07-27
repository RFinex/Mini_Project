using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image slotImg;

    private Trophy trophy;
    /*
    트로피 소유 시, 트로피 이미지로 교체 후 a값 1로 변경
    트로피 미 소유 시, 이미지 a값 0으로 변경하여 숨김
    */
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
}