using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [Header("Item Image")]
    [SerializeField] private Image slotImg;

    [Header("Slot Color")]
    [SerializeField] private Color itemColor;
    [SerializeField] private Color emptyColor;

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
                slotImg.color = itemColor;
            }
            else
            {
                slotImg.color = emptyColor;
            }
        }
    }
}