using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SpawnerController spawner;
    public LevelLoader biomeLoader;
    EnemyData[] enemies;
    WavesJsonList waves;
    WavesJson currentWave;
    int waveNum, countDefeatedEnemies, numberForNextWave;

    void Start()
    {
                //Loading current level's data
        string path = Path.Combine(Application.streamingAssetsPath, "Waves.json");
        if (File.Exists(path))
            waves = JsonUtility.FromJson<WavesJsonList>(File.ReadAllText(path));
        else
            Debug.Log("Waves.json file doesn't exist");

        waveNum = -1;
        countDefeatedEnemies = 0;
        
        enemies = biomeLoader.GetEnemies("Desert");

        StartNextWave();
    }

    public void EnemyDefeated(GameObject enemy = null, bool destroyedByPlayer = true)
    {
        if (enemy is not null)
        {
            spawner.DeleteEnemy(enemy);
            if (destroyedByPlayer && ++countDefeatedEnemies == numberForNextWave)
                StartNextWave();
        }
    }

    public void StartNextWave()
    {
        currentWave = waves.GetWave(++waveNum);
        numberForNextWave = countDefeatedEnemies + currentWave.enemiesNumber;

        int n = 0;
        EnemyData[] newEnemyList = new EnemyData[currentWave.enemies.Length];

        foreach (string name in currentWave.enemies)
            foreach (EnemyData data in enemies)
                if (data.name == name)
                {
                    newEnemyList[n++] = data;
                    if (name.Substring(0, 4) == "Boss")
                        spawner.SpawnBoss(data);
                    break;
                }
        
        spawner.ChangeEnemyList(newEnemyList, currentWave.enemiesSpawnTime, currentWave.maxEnemiesAmount);
    }
}
