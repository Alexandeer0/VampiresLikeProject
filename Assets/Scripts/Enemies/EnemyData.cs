using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Game/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float health;
    public float speed;
    public float damage;
    public float exp;
    public string[] dropList;
    public float[] dropChance;
    public float spawnWeight;
    public float jumpLength;
    public float stillTime;
    public Sprite[] sprite;
    public GameObject prefab;
}
