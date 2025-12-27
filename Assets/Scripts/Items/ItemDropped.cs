using System.Collections;
using UnityEngine;

public class ItemDropped : MonoBehaviour
{
    private CircleCollider2D itemCollider;
    public ItemData itemData;

    void Start()
    {
        itemCollider = GetComponent<CircleCollider2D>();
        GetComponent<SpriteRenderer>().sprite = itemData.sprite;
        StartCoroutine(ColliderTurnOff(Time.time + 0.1f));
    }
    
    public void SpawnItem(ItemData currentDrop, Vector2 spawnPos)
    {
        itemData = currentDrop;
        Instantiate(gameObject, spawnPos, transform.rotation);
    }

    IEnumerator ColliderTurnOff(float endTime)
    {
        while (Time.time < endTime)
            yield return new WaitForFixedUpdate();
        itemCollider.isTrigger = true;
        Destroy(GetComponent<Rigidbody2D>());
    }
}
