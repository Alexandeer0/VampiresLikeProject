using UnityEngine.UI;

public class BaseCellController : ActiveCellController
{
    protected override void Start()
    {
        base.Start();
        SetActive(false);
    }

    public void SetActive(bool value)
    {
        if (isCellFilled)
        {
            image.enabled = value;
            text.enabled = value;
        }
        transform.parent.GetComponent<Image>().enabled = value;
    }

    public override void AddItemToCell(ItemData item)
    {
        isCellFilled = true;
        currentItemData = item;
        image.sprite = currentItemData.sprite;
        if (transform.parent.GetComponent<Image>().enabled)
        {
            image.enabled = true;
            text.enabled = true;
        }

        amount = 1;
        text.text = amount.ToString();
    }
}
