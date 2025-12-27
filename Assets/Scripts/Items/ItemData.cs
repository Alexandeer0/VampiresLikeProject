using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public int itemAmount;
    public Sprite sprite;
    public GameObject prefab;
}
