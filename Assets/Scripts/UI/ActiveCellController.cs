using UnityEngine;
using UnityEngine.UI;

public class ActiveCellController : MonoBehaviour
{

    protected PlayerClass pc;
    protected Image image;
    protected Text text;
    protected int amount;
    protected float reloadTime = 0;
    [SerializeField] protected bool isCellFilled;
    [SerializeField] protected ItemData currentItemData;
    protected ItemBaseClass currentItemClass;

    protected virtual void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerClass>();
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        if (isCellFilled)
        {
            image.sprite = currentItemData.sprite;
            image.enabled = true;
            currentItemClass = currentItemData.prefab.GetComponent<ItemBaseClass>();
        }
    }

    public GameObject GetCurrentItem()
    {
        return currentItemData.prefab;
    }

    public ItemData GetCurrentItemData()
    {
        return currentItemData;
    }

    public int GetCurrentItemAmount()
    {
        return currentItemData.itemAmount;
    }

    public bool IsFilled()
    {
        return isCellFilled;
    }

    public virtual void AddItemToCell(ItemData item)
    {
        isCellFilled = true;
        currentItemData = item;
        image.sprite = currentItemData.sprite;
        image.enabled = true;

        amount = 1;
        text.text = amount.ToString();
    }

    public void AddItemToItem(ItemData item, int amountToAdd)
    {
        amount += amountToAdd;
        text.text = amount.ToString();
    }

    public void UseItem()
    {
        if(Time.time >= reloadTime)
        {
            currentItemClass.UseItem(currentItemData);
            if (currentItemData is WeaponData weaponData)
                reloadTime = Time.time + weaponData.reloadTime;
        }
    }
}