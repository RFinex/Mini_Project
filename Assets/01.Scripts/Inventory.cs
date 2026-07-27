using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Trophy> trophys;

    [SerializeField] private Transform inventory;
    [SerializeField] private Slot[] slots;

    [SerializeField] private Button closeBtn;

    private void Awake()
    {
        closeBtn.onClick.AddListener(UIManager.instance.CloseTrophyInv);
    }

    // 인스펙터 값 변경 시 호출
    private void OnValidate()
    {
        slots = inventory.GetComponentsInChildren<Slot>();
    }

    private void OnEnable()
    {
        GetTrophyData();
    }

    private void GetTrophyData()
    {
        trophys.Clear();

        List<Trophy> trophyData = DataManager.instance.GetTrophyInfo();
        foreach (Trophy trophy in trophyData)
        {
            if (trophy.isCollect)
            {
                trophys.Add(trophy);
            }
        }

        SlotClear();
    }

    // 얻은 트로피 만큼 슬롯에 트로피 배치
    private void SlotClear()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < trophys.Count)
            {
                slots[i]._trophy = trophys[i];
            }
            else
            {
                slots[i]._trophy = null;
            }
        }
    }

    //public void AddTrophy(Trophy trophy)
    //{
    //    if (trophys.Count < slots.Length)
    //    {
    //        trophys.Add(trophy);
    //        SlotClear();
    //    }
    //    else
    //    {
    //        Debug.Log("Error");
    //    }
    //}
}
