using System.Collections;
using UnityEngine;

public class WeaponMelee : WeaponBaseClass
{

    private IEnumerator attackCoroutine;

    protected override void Start()
    {
        base.Start();

        weaponSpawn = GameObject.FindWithTag("Player").transform;
        transform.parent = weaponSpawn;
        transform.localPosition = Vector3.zero;

        transform.rotation = weaponSpawn.GetChild(0).rotation;
        transform.Rotate(0, 0, -60);
        GetComponentInChildren<SpriteRenderer>().sprite = itemData.sprite;

        attackCoroutine = Attacking();
        StartCoroutine(attackCoroutine);
    }

    public IEnumerator Attacking()
    {
        float rotatedAngle = 0f;

        while (rotatedAngle < 120f)
        {
            float rotateStep = speed * Time.fixedDeltaTime;
            transform.Rotate(0, 0, rotateStep);
            rotatedAngle += rotateStep;

            yield return new WaitForFixedUpdate();
        }
        
        yield return StartCoroutine(WaitForNextHit(Time.time + lifeTime));
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Enemy")
        {
            EnemyBaseClass enemyClass = other.GetComponent<EnemyBaseClass>();
            enemyClass.ChangeHealth(damage, new Vector2(other.transform.position.x - transform.position.x, other.transform.position.y - transform.position.y).normalized, 10f);
        }
    }


    private IEnumerator WaitForNextHit(float endTime)
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        while (Time.time < endTime)
            yield return new WaitForFixedUpdate();
        Destroy(gameObject);
    }
}
