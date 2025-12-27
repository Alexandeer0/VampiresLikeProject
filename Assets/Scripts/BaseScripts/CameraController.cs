using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public bool following;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        following = true;
    }
    
    void LateUpdate()
    {
        if (following)
            if (Input.GetKey(KeyCode.LeftAlt))
                transform.position = (Player.transform.position + Camera.main.ScreenToWorldPoint(Input.mousePosition)) / 2 + new Vector3(0f, 0f, -10f);
            else
                transform.position = Player.transform.position + new Vector3(0f, 0f, -10f);
    }
}
