using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] List<ActiveCellController> inventoryCells;
    private ActiveCellController lastEmptyCell;

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
            inventoryCells.Add(transform.GetChild(i).GetChild(0).GetComponent<ActiveCellController>());
    }

    public bool InventoryAddItem(ItemData item)
    {
        foreach (ActiveCellController cell in inventoryCells)
        {
            if (cell.IsFilled())
            {
                if (cell.GetCurrentItemData() == item)
                {
                    if (cell.GetCurrentItemAmount() > 1)
                    {
                        cell.AddItemToItem(item, 1); // 1 - это количество предметов, которое добавляем. Пока что всегда 1
                        return true;
                    }
                }
            }
            else
                lastEmptyCell = cell;
        }

        if (lastEmptyCell is not null)
        {
            lastEmptyCell.AddItemToCell(item);
            return true;
        }
        return false;
    }

    public ActiveCellController GetCurrentCell(int value)
    {
        return inventoryCells[value];
    }


    public void ShowInventory(bool value)
    {
        for (int i = 8; i < 32; i++)
            inventoryCells[i].GetComponent<BaseCellController>().SetActive(value);
    }
}
