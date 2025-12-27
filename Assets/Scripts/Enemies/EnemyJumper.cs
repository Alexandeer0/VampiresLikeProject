using System.Collections;
using UnityEngine;

public class EnemyJumper : EnemyBaseClass
{

    public GameObject head;
    private SpriteRenderer sr;
    private HeadTurn ht;

    Vector3 jumpDirection;
    private bool isJumping;
    private float jumpLength, stillTime;
    private IEnumerator jumpingCoroutine;
    private Sprite[] sprites;

    protected override void Start()
    {
        jumpLength = enemyData.jumpLength;
        stillTime = enemyData.stillTime;
        sprites = enemyData.sprite;

        ht = head.GetComponent<HeadTurn>();
        sr = head.GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];

        base.Start();
    }
    

    public override void Neutral()
    {   
        jumpDirection = (transform.position - Player.transform.position).normalized;
        if (!isJumping)
        {
            ht.TurnHeadTo(transform.position + jumpDirection);
            jumpingCoroutine = Jumping(jumpDirection * 2);
            StartCoroutine(jumpingCoroutine);
        }
        base.Neutral();
    }


    public override void MovementLogic()
    {
        jumpDirection = (Player.transform.position - transform.position).normalized;
        if (!isJumping)
        {
            ht.TurnHeadTo(Player.transform.position);
            jumpingCoroutine = Jumping(jumpDirection);
            StartCoroutine(jumpingCoroutine);
        }
    }


    private IEnumerator Jumping(Vector3 direction)
    {
        isJumping = true;
        sr.sprite = sprites[1];
        for (int i = 0; i < jumpLength; i++)
        {
            transform.Translate(direction * speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        sr.sprite = sprites[0];
        StartCoroutine(WaitForNextJump(stillTime));
    }


    public override void ChangeHealth(float value, Vector3 direction, float power, bool stun = true)
    {
        if (!isInvincible)
        {
            health -= value;
            if (health <= 0f)
                GotKilled();
            else
            {
                StopAllCoroutines();
                sr.sprite = sprites[0];

                StartCoroutine(GotDamaged(direction, power, 5));
                StartCoroutine(MakeInvincible(5));

                StartCoroutine(WaitForNextJump(stillTime));
            }
        }
    }

    public IEnumerator WaitForNextJump(float frames)
    {
        for (int i = 0; i < frames; i++)
        {
            if (movement == MovementLogic)
                ht.TurnHeadTo(Player.transform.position);
            else
                ht.TurnHeadTo(transform.position + jumpDirection);
            yield return new WaitForFixedUpdate();
        }
        isJumping = false;
    }
    

    public override void GotKilled()
    {
        base.GotKilled();
        sr.color = Color.black;
        Destroy(gameObject, 0.5f);
    }
}
