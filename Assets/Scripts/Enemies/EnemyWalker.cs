using UnityEngine;

public class EnemyWalker : EnemyBaseClass
{

    private Sprite[] sprites;
    public GameObject head;
    private SpriteRenderer sr;
    private HeadTurn ht;
    private Vector3 walkDirection;

    protected override void Start()
    {
        sprites = enemyData.sprite;
        ht = head.GetComponent<HeadTurn>();
        sr = head.GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];

        base.Start();
    }

    public override void Neutral()
    {
        walkDirection = (transform.position - Player.transform.position).normalized;
        ht.TurnHeadTo(transform.position + walkDirection);
        transform.Translate(walkDirection * speed * Time.fixedDeltaTime * 2);
        base.Neutral();
    }

    public override void MovementLogic()
    {
        walkDirection = (Player.transform.position - transform.position).normalized;
        ht.TurnHeadTo(Player.transform.position);
        transform.Translate(walkDirection * speed * Time.fixedDeltaTime);
    }
    
    public override void GotKilled()
    {
        base.GotKilled();
        sr.color = Color.black;
        Destroy(gameObject, 0.5f);
    }
}
