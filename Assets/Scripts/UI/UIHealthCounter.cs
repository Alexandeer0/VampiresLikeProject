using UnityEngine;
using UnityEngine.UI;

public class UIHealthCounter : MonoBehaviour
{

    PlayerClass pc;
    Text text;

    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerClass>();
        text = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        text.text = pc.health.ToString();
    }
}
