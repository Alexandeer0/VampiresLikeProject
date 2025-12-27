using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    GameObject Player;
    [SerializeField] EnemyData[] Enemies;
    [SerializeField] EnemyData[] Bosses;
    float spawnTime, lastSpawnTime, totalSpawnWeight;
    int bossesAlive, maxEnemiesAmount;
    List<GameObject> aliveEnemies;


    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        spawnTime = 1f;
        lastSpawnTime = Time.time + 2;
        aliveEnemies = new List<GameObject>();
        bossesAlive = 0;
        maxEnemiesAmount = 0;
    }


    private void FixedUpdate()
    {
        if (Time.time >= lastSpawnTime)
            EnemySpawner();
    }


    private void EnemySpawner()
    {
        lastSpawnTime += spawnTime;
        if (bossesAlive == 0 && aliveEnemies.Count < maxEnemiesAmount)
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            Vector2 spawnPos = (Vector2)Player.transform.position + new Vector2(direction.x * 30f, direction.y * 30f);
            EnemyData currentEnemy = GetRandomEnemy();
            aliveEnemies.Add(currentEnemy.prefab.GetComponent<EnemyBaseClass>().setDataAndSpawn(currentEnemy, spawnPos));
        }
    }

    private EnemyData GetRandomEnemy()
    {
        float randomValue = Random.Range(0f, totalSpawnWeight);

        foreach (var enemy in Enemies)
        {
            if (randomValue < enemy.spawnWeight)
                return enemy;
            randomValue -= enemy.spawnWeight;
        }

        return Enemies[0];
    }



    public void ChangeEnemyList(EnemyData[] newList, float newSpawnTime, int newMaxEnemiesAmount)
    {
        Enemies = newList;
        spawnTime = newSpawnTime;
        maxEnemiesAmount = newMaxEnemiesAmount;

        totalSpawnWeight = 0;
        foreach (EnemyData enemy in Enemies)
            totalSpawnWeight += enemy.spawnWeight;
    }

    public void DeleteEnemy(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);
        if (enemy.name.Substring(0, 4) == "Boss")
            bossesAlive--;
    }



    public void SpawnBoss(EnemyData bossData)
    {
        bossesAlive++;

        foreach (GameObject enemy in aliveEnemies)
            enemy.GetComponent<EnemyBaseClass>().ChangeMovement();

        StartCoroutine(WhileBossSpawned(bossData, Time.time + 5f));
    }
    
    public IEnumerator WhileBossSpawned(EnemyData bossData, float endTime)
    {
        while (Time.time < endTime)
            yield return new WaitForFixedUpdate();
        
        Vector2 bossSpawnPos = new Vector2(Player.transform.position.x, Player.transform.position.y + 15);
        Instantiate(bossData.prefab, bossSpawnPos, transform.rotation);
    }
}
