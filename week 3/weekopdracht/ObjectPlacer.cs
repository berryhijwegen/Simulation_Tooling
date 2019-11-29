using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject holder;
    public Terrain terrain;
    public int numberOfSpawns;
    // Update is called once per frame

    void Start()
    {
        terrain.transform.position = new Vector3(0,0,0);
        for (int i = 0; i < numberOfSpawns; i++)
        {
            GameObject GO = Instantiate(objectToPlace, getRandomPosition(), Quaternion.identity);
            GO.transform.parent = holder.transform;
        }
        
    }


    Vector3 getRandomPosition(){
        Vector3 terrainSize = terrain.terrainData.size;

        return new Vector3(Random.Range(10.0f, terrainSize.x - 10.0f), 0.5f, Random.Range(10.0f, terrainSize.z - 10.0f));
    }
}
