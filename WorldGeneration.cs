using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldGeneration : MonoBehaviour
{
    public InputField width;
    public Slider size;
    public int worldWidth, worldHeight, biomeDensity;
    public GameObject tile;
    public bool[,] occupiedTiles;
    GameObject[] biomeTile;
    int numberOfTiles;
    int fail;
    public GameObject parent;
    public GameObject floor;
    public GameObject canvas;
    public GameObject oSCanvas;

    public GameObject player;

    public Texture2D planeTexture;
    Color biomeColor;
    public void Generate()
    {
        worldWidth = System.Convert.ToInt32(width.text);
        worldHeight = worldWidth;
        canvas.GetComponent<Canvas>().enabled = false;
        Camera.main.transform.position = new Vector3(worldWidth / 2-0.5f, 1, worldHeight / 2 - 0.5f);
        Camera.main.GetComponent<Camera>().orthographicSize = worldWidth / 2;
        floor.transform.localScale = new Vector3(50*worldWidth, 50*worldHeight, 1);
        int toModulo = worldWidth%2;
        switch (toModulo)
        {
            case 0:
                floor.transform.position = new Vector3(worldWidth*100 / 2, 0, worldHeight * 100 / 2);
                break;
            case 1:
                floor.transform.position = new Vector3(worldWidth * 100 / 2 + 0.5f, 0, worldHeight * 100 / 2 + 0.5f);
                break;
        }
        planeTexture = new Texture2D(worldHeight, worldWidth, TextureFormat.ARGB32, false);
        biomeDensity = ((worldHeight * worldWidth) / System.Convert.ToInt32(size.value)) / 15;
        biomeTile = new GameObject[worldHeight*worldWidth];
        if (biomeDensity == 0) biomeDensity = 1;
        occupiedTiles = new bool[worldWidth,worldHeight];
        this.GetComponent<BiomeMap>().DeclareBiomesArray(worldWidth,worldHeight);
        for (int i = 0; i < biomeDensity; i++)
        {
            int x = Random.Range(0, worldWidth);
            int y = Random.Range(0, worldHeight);
            float xF = (float)x;
            float yF = (float)y;
            if (occupiedTiles[x, y] == false)
            {
                numberOfTiles++;
                occupiedTiles[x, y] = true;
                if (numberOfTiles < (worldWidth * worldHeight) + 1) {
                    int biomeIndex;
                    biomeIndex = Random.Range(0, 23);
                    biomeTile[i] = Instantiate(tile, new Vector3(xF, 0, yF), Quaternion.identity);
                    switch (biomeIndex)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            Debug.Log("Generated Desert Biome");
                            biomeColor = new Color(0.811f, 0.819f, 0.639f, 1);
                            this.GetComponent<BiomeMap>().biomes[x, y] = BiomeMap.Biomes.Desert;
                            biomeTile[i].GetComponent<ExpandBiomes>().biome = BiomeMap.Biomes.Desert;
                            break;
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                            Debug.Log("Generated Plains Biome");
                            biomeColor = new Color(0.435f, 0.670f, 0.388f, 1);
                            this.GetComponent<BiomeMap>().biomes[x, y] = BiomeMap.Biomes.Plains;
                            biomeTile[i].GetComponent<ExpandBiomes>().biome = BiomeMap.Biomes.Plains;
                            break;
                        case 11:
                        case 12:
                        case 13:
                            Debug.Log("Generated Swamp Biome");
                            biomeColor = new Color(0.074f, 0.109f, 0.015f, 1);
                            this.GetComponent<BiomeMap>().biomes[x, y] = BiomeMap.Biomes.Swamp;
                            biomeTile[i].GetComponent<ExpandBiomes>().biome = BiomeMap.Biomes.Swamp;
                            break;
                        case 14:
                        case 15:
                        case 16:
                            Debug.Log("Generated Mountain Biome");
                            biomeColor = new Color(0.2f, 0.219f, 0.2f, 1);
                            this.GetComponent<BiomeMap>().biomes[x, y] = BiomeMap.Biomes.Mountains;
                            biomeTile[i].GetComponent<ExpandBiomes>().biome = BiomeMap.Biomes.Mountains;
                            break;
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                            Debug.Log("Generated Forest Biome");
                            biomeColor = new Color(0.015f, 0.101f, 0.015f, 1);
                            this.GetComponent<BiomeMap>().biomes[x, y] = BiomeMap.Biomes.Forest;
                            biomeTile[i].GetComponent<ExpandBiomes>().biome = BiomeMap.Biomes.Forest;
                            break;
                        default:
                            Debug.Log("Generated UNKNOWN Biome");
                            biomeColor = new Color(1, 0, 0, 1);
                            this.GetComponent<BiomeMap>().biomes[x, y] = BiomeMap.Biomes.UNKNOWN;
                            biomeTile[i].GetComponent<ExpandBiomes>().biome = BiomeMap.Biomes.UNKNOWN;
                            break;
                    }
                    planeTexture.SetPixel(x, y, biomeColor);

                    biomeTile[i].GetComponent<ExpandBiomes>().color = biomeColor;
                    biomeTile[i].transform.eulerAngles = new Vector3(-90, 0, 0);
                biomeTile[i].transform.parent = parent.transform;
                biomeTile[i].GetComponent<ExpandBiomes>().myX = x;
                biomeTile[i].GetComponent<ExpandBiomes>().myY = y;
                biomeTile[i].GetComponent<ExpandBiomes>().wG = this;
                }
            }
            else
            {
                i--;
            }
        }
        for (int i = 0; i < biomeDensity; i++)
        {
            biomeTile[i].GetComponent<ExpandBiomes>().ACTIVATE = true;
        }
    }
    GameObject newBiomeTile;
    public void ExpandBiome(int newX, int newY, Color color, BiomeMap.Biomes newBiome)
    {
        numberOfTiles++;
        if (numberOfTiles < (worldWidth * worldHeight)+1)
        {
            planeTexture.SetPixel(newX, newY, color);

            
            newBiomeTile = Instantiate(tile, new Vector3(newX, 0, newY), Quaternion.identity);
            newBiomeTile.transform.eulerAngles = new Vector3(-90, 0, 0);
            newBiomeTile.transform.parent = parent.transform;
            newBiomeTile.GetComponent<ExpandBiomes>().color = color;
            newBiomeTile.GetComponent<ExpandBiomes>().myX = newX;
            newBiomeTile.GetComponent<ExpandBiomes>().myY = newY;
            newBiomeTile.GetComponent<ExpandBiomes>().wG = this;
            newBiomeTile.GetComponent<ExpandBiomes>().biome = newBiome;
            this.GetComponent<BiomeMap>().biomes[newX, newY] = newBiome;
            occupiedTiles[newX, newY] = true;
            newBiomeTile.GetComponent<ExpandBiomes>().ACTIVATE = true;
        } else
        {
            Debug.Log("DONE!");
        }
    }
    int triesOfSpawning;
    private void Update()
    {
        if (numberOfTiles >= worldHeight * worldWidth)
        {
            planeTexture.Apply();
            planeTexture.filterMode = FilterMode.Point;
            floor.SetActive(true);
            floor.GetComponent<Renderer>().material.mainTexture = planeTexture;
            floor.transform.localScale = new Vector3(floor.transform.localScale.x*100, floor.transform.localScale.y * 100, 1);
            player.SetActive(true);
            int worldX = Random.Range(0 + worldWidth / 3, worldWidth - worldWidth / 3);
            int worldY = Random.Range(0 + worldHeight / 3, worldHeight - worldHeight / 3);
            while (this.GetComponent<BiomeMap>().biomes[worldX, worldY] == BiomeMap.Biomes.Mountains)
            {
                worldX = Random.Range(0 + worldWidth / 3, worldWidth - worldWidth / 3);
                worldY = Random.Range(0 + worldHeight / 3, worldHeight - worldHeight / 3);
                triesOfSpawning++;
                if (triesOfSpawning > 100)
                Debug.Break();
            }
            player.transform.position = new Vector3(worldX*100, 0.01f, worldY * 100);
            Camera.main.transform.parent = player.transform;
            Camera.main.transform.localPosition = new Vector3(0, 11, 0);
            Camera.main.orthographic = false;
            Material material = floor.GetComponent<Renderer>().sharedMaterial;
            material.SetInt("_SmoothnessTextureChannel", 0);
            material.SetFloat("_Metallic", 0);
            material.SetFloat("_GlossMapScale", 0);
            material.SetFloat("_Glossiness", 0);
            floor.GetComponent<Renderer>().sharedMaterial = material;
            this.GetComponent<GenerateResources>().enabled = true;
            Destroy(parent.gameObject);
            oSCanvas.GetComponent<Joystick>().StartPosition();
            Destroy(this);
        }
    }
    
}
