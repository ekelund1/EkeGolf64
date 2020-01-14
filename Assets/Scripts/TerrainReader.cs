using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainReader : MonoBehaviour
{
    public int surfaceIndex = 0;
    public string surfaceName = "";

    private Terrain terrain;
    private TerrainData terrainData;
    private Vector3 terrainPos;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        terrain = Terrain.activeTerrain;
        terrainData = terrain.terrainData;
        terrainPos = terrain.transform.position;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(50, 50, 250, 25), "index: " + surfaceIndex.ToString() + ", name: " + surfaceName);

    }
    // Update is called once per frame
    protected virtual void Update()
    {
        surfaceIndex = GetMainTexture(transform.position);
        surfaceName = terrainData.splatPrototypes[surfaceIndex].texture.name;
    }
    private float[] GetTextureMix(Vector3 WorldPos)
    {
        // returns an array containing the relative mix of textures
        // on the main terrain at this world position.

        // The number of values in the array will equal the number
        // of textures added to the terrain.

        // calculate which splat map cell the worldPos falls within (ignoring y)
        int mapX = (int)(((WorldPos.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
        int mapZ = (int)(((WorldPos.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);

        // get the splat data for this cell as a 1x1xN 3d array (where N = number of textures)
        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

        // extract the 3D array data to a 1D array:
        float[] cellMix = new float[splatmapData.GetUpperBound(2) + 1];

        for (int n = 0; n < cellMix.Length; n++)
        {
            cellMix[n] = splatmapData[0, 0, n];
        }
        return cellMix;
    }

    private int GetMainTexture(Vector3 WorldPos)
    {
        // returns the zero-based index of the most dominant texture
        // on the main terrain at this world position.
        float[] mix = GetTextureMix(WorldPos);

        float maxMix = 0;
        int maxIndex = 0;

        // loop through each mix value and find the maximum
        for (int n = 0; n < mix.Length; n++)
        {
            if (mix[n] > maxMix)
            {
                maxIndex = n;
                maxMix = mix[n];
            }
        }
        return maxIndex;
    }
}
