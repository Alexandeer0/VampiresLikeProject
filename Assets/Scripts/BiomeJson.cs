using System;

[Serializable]
public class BiomeJson
{
    public string name;
    public float spawnRate;
    public float lightLevel;
    public string[] enemies;
}

[Serializable]
public class BiomeJsonList
{
    public BiomeJson[] biomes;
}