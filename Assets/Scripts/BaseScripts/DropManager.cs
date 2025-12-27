using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{

    private Dictionary<string, ItemData> dropList;

    void Start()
    {
        dropList = new Dictionary<string, ItemData>();
        LoadDropList();
    }

    private void LoadDropList()
    {
        foreach (ItemData item in Resources.LoadAll<ItemData>("Items"))
        {
            dropList.Add(item.itemName, item);
        }
    }

    public GameObject GetEnemyDrop(string dropName)
    {
        return dropList[dropName].prefab;
    }

    public void SpawnItem(string[] items, float[] chances, Vector2 spawnPos)
    {
        for (int i = 0; i < items.Length; i++)
            if (Random.value <= chances[i])
            {
                ItemData currentDrop = dropList[items[i]];
                currentDrop.prefab.GetComponent<ItemDropped>().SpawnItem(currentDrop, spawnPos);
            }
    }
}
