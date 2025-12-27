using System;

[Serializable]
public class WavesJson
{
    public int enemiesNumber;
    public float enemiesSpawnTime;
    public int maxEnemiesAmount;
    public string[] enemies;
}

[Serializable]
public class WavesJsonList
{
    public WavesJson[] waves;

    public WavesJson GetWave(int value) {return waves[value];}
}