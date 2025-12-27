using UnityEngine;

public class Explosion : WeaponBaseClass
{

    Color origColor;
    SpriteRenderer sr;

    protected override void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        origColor = sr.color;
    }


    void FixedUpdate()
    {
        if (origColor.a > 0)
        {
            origColor = new Color(origColor.r, origColor.g, origColor.b, origColor.a - 0.1f);
            sr.color = origColor;
        }
        else
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Enemy")
        {
            EnemyBaseClass enemyClass = other.GetComponent<EnemyBaseClass>();
            enemyClass.ChangeHealth(damage, Vector2.zero, 10f);
        }
    }
}
