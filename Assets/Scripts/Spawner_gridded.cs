using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_gridded : MonoBehaviour
{
    public int xGridWdith = 10;
    public int zGridWidth = 7;
    public float propabilityOfTreeInGrid = 0.7f;
    public int minTreesPerSquare = 1;
    public int maxTreesPerSquare = 15;
    public Transform treePrefab;

    public float bufferBetweenGrid = 0.5f;

    public GameObject ground;

    //private float groundMinX;
    //private float groundMaxX;
    //private float groundMinZ;
    //private float groundMaxZ;

    void Start()
    {
        ground = gameObject;
        ground.transform.localScale = new Vector3(xGridWdith*10, 1, zGridWidth*10);
        gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(xGridWdith, zGridWidth);

        //groundMinX = transform.position.x - (transform.localScale.x / 2);
        //groundMaxX = transform.position.x + (transform.localScale.x / 2);
        //groundMinZ = transform.position.z - (transform.localScale.z / 2);
        //groundMaxZ = transform.position.z + (transform.localScale.z / 2);
        Vector3 offset = new Vector3(-ground.transform.localScale.x/2, 0, -ground.transform.localScale.z/2);


        for (int x = 0; x < xGridWdith; ++x)
        {
            for(int z = 0; z < zGridWidth; ++z)
            {
                int numTrees = Random.Range(minTreesPerSquare, maxTreesPerSquare);
                for(int t = 0; t < numTrees; ++t)
                {
                    Transform temp = Instantiate(treePrefab, new Vector3(Random.Range((x * 10) + bufferBetweenGrid + 0.25f, (x * 10) + 10 - bufferBetweenGrid), 2, Random.Range((z * 10) + bufferBetweenGrid + 0.25f, (z * 10) + 10 - bufferBetweenGrid)) + offset, Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}