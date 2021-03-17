using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandBiomes : MonoBehaviour
{
    public bool ACTIVATE;
    bool U, D, L, R;
    public WorldGeneration wG;
    public int myX, myY;
    public Color color;
    public BiomeMap.Biomes biome;
    private void Update()
    {
        if (ACTIVATE)
        {
            int dir;
            if (!U | !D | !L | !R)
            {
                dir = Random.Range(0, 4);
                switch (dir)
                {
                    case 0:
                        U = true;
                        break;
                    case 1:
                        D = true;
                        break;
                    case 2:
                        L = true;
                        break;
                    case 3:
                        R = true;
                        break;
                }
            }
            if (U)
            {
                U = true;
                if (wG.occupiedTiles[myX, myY+1]==false)
                {
                    wG.ExpandBiome(myX, myY + 1, color, biome);
                }
            }
            if (D)
            {
                D = true;
                if (wG.occupiedTiles[myX, myY - 1] == false)
                {
                    wG.ExpandBiome(myX, myY - 1, color, biome);
                }
            }
            if (L)
            {
                L = true;
                if (wG.occupiedTiles[myX-1, myY] == false)
                {
                    wG.ExpandBiome(myX-1, myY, color, biome);
                }
            }
            if (R)
            {
                R = true;
                if (wG.occupiedTiles[myX + 1, myY] == false)
                {
                    wG.ExpandBiome(myX + 1, myY, color, biome);
                }
            }
            if (U & D & L & R)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
