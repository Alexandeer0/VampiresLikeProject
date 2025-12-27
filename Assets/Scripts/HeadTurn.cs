using UnityEngine;

public class HeadTurn : MonoBehaviour
{

    public void TurnHeadTo(Vector3 lookVector)
    {
        transform.up = new Vector3(lookVector.x - transform.position.x, lookVector.y - transform.position.y, 0);
    }

}
