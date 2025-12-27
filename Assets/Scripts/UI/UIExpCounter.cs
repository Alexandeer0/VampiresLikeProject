using UnityEngine;
using UnityEngine.UI;

public class UIExpCounter : MonoBehaviour
{

    private PlayerClass pc;
    private Text text;

    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerClass>();
        text = GetComponent<Text>();
    }


    void LateUpdate()
    {
        text.text = pc.GetPlayerExp().ToString();
    }
}
