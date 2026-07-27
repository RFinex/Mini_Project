using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<Trophy> trophys;

    [SerializeField] private Transform inventory;
    [SerializeField] private Slot[] slots;

    private void OnValidate()
    {
        slots = inventory.GetComponentsInChildren<Slot>();
    }

    private void Awake()
    {
        SlotClear();
    }

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

    public void AddTrophy(Trophy trophy)
    {
        if (trophys.Count < slots.Length)
        {
            trophys.Add(trophy);
            SlotClear();
        }
        else
        {
            Debug.Log("Error");
        }
    }
}
