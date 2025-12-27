using UnityEngine;

public class WeaponMine : WeaponBaseClass
{

    public GameObject explosion;

    protected override void Start()
    {
        weaponSpawn = GameObject.FindWithTag("Player").transform;
        transform.position = weaponSpawn.position;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Enemy")
        {
            GameObject explode = Instantiate(explosion, transform.position, transform.rotation);
            explode.GetComponent<WeaponBaseClass>().damage = damage;
            Destroy(gameObject);
        }
    }
}
