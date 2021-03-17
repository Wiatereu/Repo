//Ten skrypt trzyma informacje na temat istniejących biomów i ich położenia

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeMap : MonoBehaviour
{
    public enum Biomes
    {
        Plains,
        Desert,
        Forest,
        Mountains,
        Swamp,
        UNKNOWN
    }
    public Biomes[,] biomes;
    int zI, xI;

    public int xCord, yCord;
    public bool returnStuff;

    private void Update()
    {
        if (returnStuff)
        {
            returnStuff = false;
            Debug.Log(biomes[xCord, yCord]);
        }
    }
    public void DeclareBiomesArray(int width, int height)
    {
        biomes = new Biomes[width, height];
    }
    public Biomes AskForCurrentBiome(float x, float z)
    {
        x = Mathf.Floor(x);
        z = Mathf.Floor(z);
        xI = Mathf.RoundToInt(x) / 100;
        zI = Mathf.RoundToInt(z) / 100;

        Biomes biomeToReturn = biomes[xI, zI];

        return biomeToReturn;
    }
    public Biomes ReturnBiome(int x, int z)
    {
        return biomes[x,z];
    }
    public int[,] AskForCoords(float pX, float pZ)
    {
        pX = Mathf.Floor(pX);
        pZ = Mathf.Floor(pZ);
        int[,] coords;
        xI = Mathf.RoundToInt(pX) / 100;
        zI = Mathf.RoundToInt(pZ) / 100;
        coords = new int[xI, zI];
        return coords;
    }
}
