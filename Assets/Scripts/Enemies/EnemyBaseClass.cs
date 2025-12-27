using System;
using System.Collections;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    
    [SerializeField] protected EnemyData enemyData;

    public GameObject Player, expObj;
    protected GameController gameController;
    
    protected float health, speed, damage, exp;
    protected bool alive, isInvincible;
    string[] dropItems; 
    float[] dropChances;

    protected PlayerClass pc;
    protected Rigidbody2D rigidBody;
    protected DropManager dropManager;
    protected Action movement;

    protected virtual void Start()
    {
        health = enemyData.health;
        speed = enemyData.speed;
        damage = enemyData.damage;

        exp = enemyData.exp;
        dropItems = enemyData.dropList;
        dropChances = enemyData.dropChance;

        alive = true;
        isInvincible = false;
        movement = MovementLogic;

        Player = GameObject.FindWithTag("Player");
        pc = Player.GetComponent<PlayerClass>();

        rigidBody = GetComponent<Rigidbody2D>();
        
        gameController = GameObject.FindWithTag("Spawner").GetComponent<GameController>();
        dropManager = GameObject.FindWithTag("DropManager").GetComponent<DropManager>();
    }

    public GameObject setDataAndSpawn(EnemyData newEnemyData, Vector2 spawnPos)
    {
        enemyData = newEnemyData;
        return Instantiate(gameObject, spawnPos, gameObject.transform.rotation);
    }



    void FixedUpdate()
    {
        if (alive)
            movement();
    }


    public virtual void Neutral()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) > 50f)
        {
            gameController.EnemyDefeated(gameObject, false);
            Destroy(gameObject);
        }
    }

    public virtual void MovementLogic() { }

    public virtual void GotKilled()
    {
        if (alive)
        {
            alive = false;
            StopAllCoroutines();
            gameController.EnemyDefeated(gameObject);
            rigidBody.simulated = false;
            SpawnExp(exp);
            SpawnItem();
        }
    }



    protected virtual void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
            HitPlayer(damage);
    }


    protected virtual void HitPlayer(float value)
    {
        pc.ChangeHealth(value, new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y).normalized);
    }


    public virtual void ChangeHealth(float value, Vector3 direction, float power, bool stun = true)
    {
        if (!isInvincible)
        {
            health -= value;
            if (health <= 0f)
                GotKilled();
            else
            {
                StartCoroutine(GotDamaged(direction, power, 5));
                if (stun)
                    StartCoroutine(MakeInvincible(5)); 
            }
        }
    }


    public virtual void ChangeMovement()
    {
        movement = Neutral;
    }


    public IEnumerator MakeInvincible(int frames)
    {
        isInvincible = true;
        for (int i = 0; i < frames; i++)
            yield return new WaitForFixedUpdate();
        isInvincible = false;
    }

    public IEnumerator GotDamaged(Vector2 direction, float power, float frames)
    {
        for (int i = 0; i < frames; i++)
        {
            transform.Translate(direction * power * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    public void SpawnExp(float value)
    {
        Vector2 spawnPos = new Vector2(transform.position.x + UnityEngine.Random.Range(-1f, 1f), transform.position.y + UnityEngine.Random.Range(-1f, 1f));
        GameObject experience = Instantiate(expObj, spawnPos, transform.rotation);
        experience.name = value.ToString();
    }

    public void SpawnItem()
    {
        Vector2 spawnPos = new Vector2(transform.position.x + UnityEngine.Random.Range(-1f, 1f), transform.position.y + UnityEngine.Random.Range(-1f, 1f));
        dropManager.SpawnItem(dropItems, dropChances, spawnPos);
    }
}
