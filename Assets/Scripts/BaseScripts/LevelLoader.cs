using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] EnemyData[] allEnemies, currentEnemies;

    public BiomeJsonList biomeData;
    private BiomeJson currentBiome;

    
    public EnemyData[] GetEnemies(string biomeName)
    {
                //Loading list of all enemies and all levels
        allEnemies = Resources.LoadAll<EnemyData>("Enemies"); 

        string path = Path.Combine(Application.streamingAssetsPath, "Biomes.json");
        if (File.Exists(path))
            biomeData = JsonUtility.FromJson<BiomeJsonList>(File.ReadAllText(path));
        else
            Debug.Log("Biomes.json file doesn't exist");
        
                //Getting enemies for current level
        currentBiome = GetBiome(biomeName);
        currentEnemies = new EnemyData[currentBiome.enemies.Length];
        int n = 0;
        
        foreach (var enemyName in currentBiome.enemies)
            currentEnemies[n++] = FindEnemyData(enemyName);
        
        return currentEnemies;
    }


    public BiomeJson GetBiome(string biomeName)
    {
        foreach (BiomeJson biome in biomeData.biomes)
        {
            if (biome.name == biomeName)
                return biome;
        }
        return null;
    }

    private EnemyData FindEnemyData(string enemyName)
    {
        foreach (var enemy in allEnemies)
        {
            if (enemy.name == enemyName)
                return enemy;
        }
        return null;
    }
}