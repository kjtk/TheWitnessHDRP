using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TerrainSounds : MonoBehaviour
{
    public Terrain terrain;
    public List<TerrainTexDef> terrainLayers = new List<TerrainTexDef>();

    private Dictionary<TerrainLayer, int> terrainLayerPosLookUp = new Dictionary<TerrainLayer, int>();
    private Dictionary<TerrainUtils.TerrainTypes, TerrainTexDef> terrainLayerLookUp = new Dictionary<TerrainUtils.TerrainTypes, TerrainTexDef>();

    private void Start()
    {
        foreach (TerrainTexDef ttd in terrainLayers)
        {
            terrainLayerPosLookUp.Add(ttd.terrainlayer, terrainLayerPosLookUp.Count);
            terrainLayerLookUp.Add(ttd.type, ttd);
        }
    }

    float[,,] currentSplatmapData;
    public void GetFootstepSound()
    {
        //for safety
        if (terrain == null || terrainLayerLookUp.Count == 0 || terrain.terrainData.terrainLayers.Length == 0) return;

        currentSplatmapData = TerrainUtils.GetSplatMapAtPos(transform.position, terrain);

        TerrainUtils.TerrainTypes typeHighestWeight = TerrainUtils.TerrainTypes.None;
        float highestValue = -1;

        Debug.Log("__________________________________");

        for (int i = 0; i < terrain.terrainData.terrainLayers.Length; i++)
        {
            //terrainlayer weight on splatmap
            float currentValue = currentSplatmapData[0, 0, i];

            //make sure the terrain layer has been assigned in our dictionary
            if (!terrainLayerPosLookUp.ContainsKey(terrain.terrainData.terrainLayers[i])) continue;

            //get all the values for the terrain layer in our dictionary
            int posInLookUP = terrainLayerPosLookUp[terrain.terrainData.terrainLayers[i]];
            TerrainTexDef ttd = terrainLayerLookUp.ElementAt(posInLookUP).Value;
            //assign the new terrain layer weight back to our dictionary
            ttd.currentWeight = currentValue;

            Debug.Log(ttd.terrainlayer.name + ": " + currentValue);

            if (ttd.footsteps != null) PlayAudio(ttd);

            if (currentValue < highestValue) continue;
            highestValue = currentValue;
            typeHighestWeight = ttd.type;
        }

        //foreach (KeyValuePair<TerrainTypes, TerrainTexDef> tlv in terrainLayerLookUp)
        //{
        //    Debug.Log(tlv.Key + ": " + tlv.Value.currentWeight);
        //}

        Debug.Log("The highest weight is: " + typeHighestWeight);
    }

    void PlayAudio(TerrainTexDef ttd)
    {
        //if (ttd.footsteps.isPlaying) return;
        ttd.footsteps.Play();
        ttd.footsteps.volume = ttd.currentWeight;
    }
}

public static class TerrainUtils
{
    public enum TerrainTypes
    {
        None,
        Sand,
        Grass,
        Rock,
        Mud
    }

    public static float[,,] GetSplatMapAtPos(Vector3 worldPos, Terrain terrain)
    {
        // calculate which splat map cell the worldPos falls within (ignoring y)
        int mapX = (int)(((worldPos.x - terrain.transform.position.x) / terrain.terrainData.size.x) * terrain.terrainData.alphamapWidth);
        int mapZ = (int)(((worldPos.z - terrain.transform.position.z) / terrain.terrainData.size.z) * terrain.terrainData.alphamapHeight);

        // get the splat data for this cell as a 1x1xN 3d array (where N = number of textures)
        return terrain.terrainData.GetAlphamaps(mapX, mapZ, 1, 1);
    }
}

[Serializable]
public class TerrainTexDef
{
    public TerrainLayer terrainlayer;
    public TerrainUtils.TerrainTypes type = TerrainUtils.TerrainTypes.None;
    public float currentWeight = -1;
    public AudioSource footsteps = null;
}