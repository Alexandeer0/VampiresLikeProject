using UnityEngine;

public class InputController : MonoBehaviour
{

    public GameObject Player, Head;
    public InventoryController inventoryController;
    private ActiveCellController currentItem;
    private PlayerClass pc;
    private HeadTurn ht;
    private bool invShown;
    private int currentCell;

    void Start()
    {
        pc = Player.GetComponent<PlayerClass>();
        ht = Head.GetComponent<HeadTurn>();
        invShown = false;
        currentCell = 0;
        currentItem = inventoryController.GetCurrentCell(0);
    }

    void Update()
    {
        MovementLogic();
        WeaponLogic();
        InventoryLogic();
        ht.TurnHeadTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }


    private void MovementLogic()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            pc.Dash();
        pc.Move();
    }

    private void WeaponLogic()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            currentItem.UseItem();
        }
    }

    private void InventoryLogic()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            invShown = !invShown;
            inventoryController.ShowInventory(invShown);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            ChangeCurrentCell((int)Input.GetAxis("Mouse ScrollWheel"), true);

        if (Input.GetAxis("SwitchInventoryCell") != 0)
            if (!string.IsNullOrEmpty(Input.inputString))
                ChangeCurrentCell(Input.inputString[0] - 48);
    }

    private void ChangeCurrentCell(int value, bool scrollWheel = false)
    {
        if (scrollWheel)
        {
            currentCell += value;
            if (currentCell > 7)
                currentCell -= 8;
            else if (currentCell < 0)
                currentCell += 8;
        }
        else
            currentCell = value - 1;
        currentItem = inventoryController.GetCurrentCell(currentCell);
    }
}
