using UnityEngine;

public class ItemBaseClass : MonoBehaviour
{

    [SerializeField] protected ItemData itemData;
    protected int itemAmount;
    protected bool isUsed;

    protected virtual void Start()
    {
        itemAmount = itemData.itemAmount;
    }

    public virtual void UseItem(ItemData itemData) { }

    public virtual float GetReloadTime()
    {
        return 0;
    }
}
