using UnityEngine;

public class WeaponBullets : WeaponBaseClass
{
    protected override void Start()
    {
        weaponSpawn = GameObject.FindWithTag("Player").transform.GetChild(0).GetChild(0);
        transform.position = weaponSpawn.position;
        transform.rotation = weaponSpawn.rotation;
        base.Start();
        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBaseClass enemyClass = other.GetComponent<EnemyBaseClass>();
            enemyClass.ChangeHealth(damage, new Vector2(other.transform.position.x - transform.position.x, other.transform.position.y - transform.position.y).normalized, 1f, false);
            Destroy(gameObject);
        }
    }
}
