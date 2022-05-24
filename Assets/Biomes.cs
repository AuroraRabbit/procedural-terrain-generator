using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biomes : MonoBehaviour
{
    //public float yTemp;
    //public float yMoist;

    //public float tempScale;
    //public float moistScale;

    //public int interpOctave;
    //public float interpLacunarity;
    //public float interpPersistence;

    //public float closestDist;
    //public float otherClosestDist;

    //public Biome closestBiome;
    //public Biome otherClosestBiome;

    [System.Serializable]
    public class Biome
    {
	//[Range(0f,1f)] public float optimalTemp;
	//[Range(0f,1f)] public float optimalMoist;

	public Gradient gradient;
	public AnimationCurve genHeightCurve;

	[Range(1,8)] public int octaves;
	[Range(1,3)] public float lacunarity;
	[Range(0,1)] public float persistence;
    }

    public Biome desert = new Biome();
    public Biome grassland = new Biome();
    public Biome tundra = new Biome();
    public Biome savanna = new Biome();
    public Biome woodland = new Biome();
    public Biome taiga = new Biome();

    public List<Biome> biomeList;
    void Start() 
    {
        biomeList.Add(desert);
        biomeList.Add(grassland);
        biomeList.Add(tundra);
        biomeList.Add(savanna);
        biomeList.Add(woodland);
        biomeList.Add(taiga);
    }
//
//    public void GenMaps(float x, float z)
//    {
//	yTemp = Mathf.PerlinNoise((x + 0.01f / tempScale), (z + 0.01f / tempScale));
//
//	yMoist = Mathf.PerlinNoise((x / moistScale), (z / moistScale));
//
//	BiomePercent(yTemp, yMoist);
//    }
//
//    public void BiomePercent(float yTemp, float yMoist)
//    {
//	closestDist = 999f;
//	otherClosestDist = 999f;
//	closestBiome = new Biome();
//	otherClosestBiome = new Biome();
//
//	foreach (Biome b in biomeList)
//	{
//	    float dist = Vector2.Distance(new Vector2(yTemp, yMoist), new Vector2(b.optimalTemp, b.optimalMoist));
//	    if (dist < closestDist)
//	    {
//		otherClosestDist = closestDist;
//		closestDist = dist;
//		otherClosestBiome = closestBiome;
//		closestBiome = b;
//	    }
//	}
//
//	float greaterPortion = otherClosestDist / (closestDist + otherClosestDist);
//	float lesserPortion = closestDist / (closestDist + otherClosestDist);
//	
//	// Calculate octave of vertex
//	interpOctave = Mathf.RoundToInt((closestBiome.octaves * closestDist) + (otherClosestBiome.octaves * otherClosestDist));
//	
//	interpLacunarity = (closestBiome.lacunarity * closestDist) + (otherClosestBiome.lacunarity * otherClosestDist);
//	interpPersistence = (closestBiome.persistence * closestDist) + (otherClosestBiome.persistence * otherClosestDist);
//    }
}
