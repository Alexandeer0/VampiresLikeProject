using System.Collections;
using UnityEngine;

public class Boss1 : EnemyBaseClass
{
    
    private Sprite[] sprites;
    public GameObject head;
    private SpriteRenderer sr;
    private HeadTurn ht;

    bool inMove;
    private float jumpLength, stillTime;
    Vector2 direction;

    protected override void Start()
    {
        health = enemyData.health;
        speed = enemyData.speed;
        damage = enemyData.damage;
        jumpLength = enemyData.jumpLength;
        stillTime = enemyData.stillTime;
        inMove = false;
        sprites = enemyData.sprite;

        ht = head.GetComponent<HeadTurn>();
        sr = head.GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];

        base.Start();
    }


    public override void MovementLogic()
    {
        if (!inMove)
        {
            if (Random.Range(0, 2) == 1)
                StartCoroutine(Running());
            else
                StartCoroutine(Jumping());
        }
    }

    public IEnumerator Running()
    {
        inMove = true;
        for (int i = 0; i < 120; i++)
        {
            direction = (Player.transform.position - transform.position).normalized;
            ht.TurnHeadTo(Player.transform.position);
            transform.Translate(direction * speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        yield return StartCoroutine(WaitForNextMove());
    }

    public IEnumerator Jumping()
    {
        inMove = true;
        ht.TurnHeadTo(Player.transform.position);
        for (int i = 0; i < 10; i++)
            yield return new WaitForFixedUpdate();
        direction = (Player.transform.position - transform.position).normalized;
        for (int i = 0; i < jumpLength; i++)
        {
            transform.Translate(direction * speed*3 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        yield return StartCoroutine(WaitForNextMove());
    }


    public IEnumerator WaitForNextMove()
    {
        for (int i = 0; i < stillTime; i++)
        {
            ht.TurnHeadTo(Player.transform.position);
            yield return new WaitForFixedUpdate();
        }
        inMove = false;
        yield return null;
    }


    public override void ChangeHealth(float value, Vector3 direction, float power, bool stun = true)
    {
        base.ChangeHealth(value, Vector3.zero, 0, stun);
    }


    public override void GotKilled()
    {
        base.GotKilled();
        sr.color = Color.black;
        rigidBody.simulated = false;
        Destroy(gameObject, 3f);
    }
}
