using System.Collections;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    public InventoryController inventoryController;
    Rigidbody2D rb;
    public float speed, health, resist;
    public bool alive, invincible, isInDash, isDashReady, isAttacking;
    int exp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        alive = true;
        invincible = false;
        isDashReady = true;
        isAttacking = false;
        exp = 0;
    }


    public void Move()
    {
        if (!isInDash)
            rb.MovePosition(transform.position + Vector3.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1f) * speed * Time.fixedDeltaTime);
    }



    public void ChangeHealth(float value, Vector2 direction)
    {
        if (!invincible)
        {
            health -= value * (1f - resist);
            if (health <= 0f)
                alive = false;
            StartCoroutine(MakePlayerInvincible(20));
            StartCoroutine(GotDamaged(direction));
        }
    }

    public IEnumerator MakePlayerInvincible(int frames)
    {
        invincible = true;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < frames; i++)
            yield return new WaitForFixedUpdate();
        invincible = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    public IEnumerator GotDamaged(Vector3 direction)
    {
        for (int i = 0; i < 5; i++)
        {
            rb.MovePosition(transform.position + direction * 5 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }



    public void Dash()
    {
        if (isDashReady)
        {
            float axisH = Input.GetAxis("Horizontal");
            float axisV = Input.GetAxis("Vertical");
            axisH = axisH > 0 ? Mathf.Ceil(axisH) : Mathf.Floor(axisH);
            axisV = axisV > 0 ? Mathf.Ceil(axisV) : Mathf.Floor(axisV);

            Vector2 dashDirection = new Vector2(axisH, axisV).normalized;
            if (dashDirection != Vector2.zero)
                StartCoroutine(InDash(dashDirection, 20));
        }
    }

    public IEnumerator InDash(Vector3 direction, int frames)
    {
        isDashReady = false;
        isInDash = true;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < frames; i++)
        {
            rb.MovePosition(transform.position + direction * speed * 2f * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        isInDash = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);
        yield return StartCoroutine(AfterDash(60));
    }

    public IEnumerator AfterDash(int frames)
    {
        for (int i = 0; i < frames; i++)
            yield return new WaitForFixedUpdate();
        isDashReady = true;
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Experience")
        {
            GetExp(int.Parse(collision.name));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Item")
        {
             if (inventoryController.InventoryAddItem(collision.GetComponent<ItemDropped>().itemData))
                Destroy(collision.gameObject);
        }
    }

    public void GetExp(int value)
    {
        exp += value;
    }

    
    public int GetPlayerExp()
    {
        return exp;
    }
}
