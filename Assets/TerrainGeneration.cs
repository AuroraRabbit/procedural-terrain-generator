using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
	Mesh mesh;

	Vector3[] vertices;
	int[] triangles;
	Color[] vertexColors;

	Biomes biome;

	float minHeight;
	float maxHeight;

	float minGenHeight;
	float maxGenHeight;

	public string seed;
	int seedInt;
	int xOffset;
	int zOffset;

	[Range(1,255)] public int xSize = 255;
	[Range(1,255)] public int zSize = 255;
	public float scale = 10;
	public float genHeight = 1;
	public AnimationCurve islandFalloff;

	public enum biomeDropdownList
	{
	    desert,
	    grassland,
	    tundra,
	    savanna,
	    woodland,
	    taiga
	};

	public biomeDropdownList biomeDropdown;

	public Biomes.Biome biomeSelection;


	void Start()
	{
	    if (seed == "")
	    {
		int tempSeed = Random.Range(0, 999999);
		seed = tempSeed.ToString();
	    }
	    
	    foreach(char c in seed)
	    {
		seedInt += (byte)c;
	    }
	    



	    Debug.Log(seed);
	    Random.InitState(seedInt);
	    xOffset = Random.Range(-10000, 10000);
	    zOffset = Random.Range(-10000, 10000);


	    mesh = new Mesh();
	    GetComponent<MeshFilter>().mesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;

	    biome = GetComponent<Biomes>();

	    //StartCoroutine(StartGen());
	}

	void Update()
	{
	    GenMesh();
	}
	IEnumerator StartGen()
	{
		GenMesh();
		yield return new WaitForSeconds(0.1f);
		StartCoroutine(StartGen());
	}

	void GenMesh()
	{
		vertices = new Vector3[(xSize+1)*(zSize+1)];
		minHeight = 0f;
		maxHeight = 0f;

		switch (biomeDropdown)
		{
		    case biomeDropdownList.desert:
	    		biomeSelection = biome.desert;
			break;

		    case biomeDropdownList.grassland:
			biomeSelection = biome.grassland;
			break;

		    case biomeDropdownList.tundra:
			biomeSelection = biome.tundra;
			break;

		    case biomeDropdownList.savanna:
			biomeSelection = biome.savanna;
			break;

		    case biomeDropdownList.woodland:
			biomeSelection = biome.woodland;
			break;

		    case biomeDropdownList.taiga:
			biomeSelection = biome.taiga;
			break;

		}

		for (int i = 0, z = 0; z <= zSize; z++)
		{
			for (int x = 0; x <= xSize; x++)
			{
				//biome.GenMaps(x, z);
				float y = GenNoise(x, z);

				if (y > maxHeight)
				{
				    maxHeight = y;
				}
				else if (y < minHeight)
				{
				    minHeight = y;
				}

				vertices[i] = new Vector3(x, y, z);
				i++;

			}
		}
		triangles = new int[xSize * zSize * 6];

		int vertex = 0;
		int triangle = 0;
		for (int z = 0; z < zSize; z++)
		{
			for (int x = 0; x < xSize; x++)
			{
				triangles[triangle + 0] = vertex + 0;
				triangles[triangle + 1] = vertex + xSize + 1;
				triangles[triangle + 2] = vertex + 1;
				triangles[triangle + 3] = vertex + 1;
				triangles[triangle + 4] = vertex + xSize + 1;
				triangles[triangle + 5] = vertex + xSize + 2;

				vertex++;
				triangle += 6;
			}
			vertex++;
		}
		ColorMesh(minHeight, maxHeight);
		UpdateMesh();
	}

	public float GenNoise(int x, int z)
	{
	    float y = 0;
	    float frequency = 1;
	    float amplitude = 1;
	    float currentOctave = 0;
	    for (int i = 0; i < 4; i++)
	    {
		currentOctave = amplitude * Mathf.PerlinNoise
		(
		(((x - (xSize / 2)))/ scale * frequency) + xOffset,
		(((z - (zSize / 2)))/ scale * frequency) + zOffset
		);

		amplitude *= biomeSelection.persistence; 
		frequency *= biomeSelection.lacunarity;
		y += currentOctave;
	    }

	    if (y > maxGenHeight)
	    {
		maxGenHeight = y;
	    }
	    else if (y < minGenHeight)
	    {
		minGenHeight = y;
	    }

	    float xCenter;
	    float zCenter;

	    if ((xSize/2) > x)
	    {
		xCenter = Mathf.InverseLerp(1, (xSize/2), x);
	    }
	    else 
	    {
		xCenter = Mathf.InverseLerp(xSize, (xSize/2), x);
	    }

	    if ((zSize/2) > z)
	    {
		zCenter = Mathf.InverseLerp(1, (zSize/2), z);
	    }
	    else 
	    {
		zCenter = Mathf.InverseLerp(zSize, (zSize/2), z);
	    }

	    float islandMap = islandFalloff.Evaluate((xCenter * zCenter) / 2);


	    return biomeSelection.genHeightCurve.Evaluate(Mathf.InverseLerp(minGenHeight, maxGenHeight, y))*genHeight*islandMap;

	}

	void ColorMesh(float minHeight, float maxHeight) {

		vertexColors = new Color[(xSize+1)*(zSize+1)];
		for (int i = 0, z = 0; z <= zSize; z++)
		{
			for (int x = 0; x <= xSize; x++)
			{
				float y = GenNoise(x, z);

				float height = Mathf.InverseLerp(minHeight, maxHeight, y);
				vertexColors[i] = biomeSelection.gradient.Evaluate(height);
				i++;
			}
		}
	}

	void UpdateMesh()
	{
		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.colors = vertexColors;
		mesh.RecalculateNormals();
	}
}
