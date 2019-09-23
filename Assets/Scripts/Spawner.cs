using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int xGridWdith = 10;
    public int zGridWidth = 7;
    public GameObject ground;
    public float minDistanceBetweenTrees, maxFireRange;
    public Tree TreePrefab;
    public int TreeCount;

    public Tree HousePrefab;
    public int HouseCount;
    public float minDistanceAwayFromHouse;

    private List<Tree> trees = new List<Tree>();
    public GameObject ObjectPool;

    // Start is called before the first frame update

    void Start()
    {
        StartGame();
    }
    void StartGame()
    {
        ground.transform.localScale = new Vector3(xGridWdith * 10, 1, zGridWidth * 10);
        ground.GetComponent<Renderer>().material.mainTextureScale = new Vector2(xGridWdith, zGridWidth);

        bool exitForLoop = false;

        //Spawn Trees
        for (int i = 0; i < TreeCount; i++)
        {
            bool validSpot;
            float x, z;
            int loopCount = 0;

            //Look for open spot
            do
            {
                loopCount++;
                validSpot = true;
                x = Random.Range(-((xGridWdith*5) - .5f), ((xGridWdith*5) - .5f));
                z = Random.Range(-((zGridWidth*5) - .5f), ((zGridWidth*5) - .5f));

                //checks if spot is valid
                for (int j = 0; j < trees.Count; j++)
                {
                    if (Vector2.Distance(new Vector2(x, z), new Vector2(trees[j].transform.position.x, trees[j].transform.position.z)) <= minDistanceBetweenTrees){
                        Debug.Log("Tree spawn at (" + x + ", " + z + ") blocked by existing tree at (" + trees[j].transform.position.x + ", " + trees[j].transform.position.z + ")");
                        validSpot = false;
                    }
                }

                //Exit if stuck and can't find open spot
                if (loopCount >= 2000)
                {
                    exitForLoop = true;
                    validSpot = true;
                    Debug.Log("Maximum number of trees at: " + i);
                }
            } while (!validSpot);

            //create tree in new spot
            Tree newTree = Instantiate(TreePrefab, new Vector3(x, 4.0f, z), Quaternion.identity);
            //Tree newTree = Instantiate(TreePrefab, ObjectPool.transform);
            //newTree.transform.position = new Vector3(x, 4.15f, z);
            trees.Add(newTree);
            if (exitForLoop) break;
            if (i == 0) newTree.FireState = 1;
        }

        //Spawn Houses
        for (int i = 0; i < HouseCount; ++i)
        {
            Tree newHouse = Instantiate(HousePrefab, new Vector3(Random.Range(-((xGridWdith * 5) - .5f), ((xGridWdith * 5) - .5f)), 2.25f, Random.Range(-((zGridWidth * 5) - .5f), ((zGridWidth * 5) - .5f))), Quaternion.identity);
            trees.Add(newHouse);

            for (int t = 0; t < trees.Count; t++)
            {
                if (Vector3.Distance(newHouse.transform.position, trees[t].transform.position) < minDistanceAwayFromHouse && trees[t] != newHouse)
                {
                    Tree temp = trees[t];
                    trees.Remove(temp);
                    Destroy(temp.gameObject);
                }
            }
        }

        //Create list of adjacent trees
        /*for (int i = 0; i < trees.Count; i++)
        {
            for (int j = 0; j < trees.Count; j++)
            {
                if (i != j && Vector3.Distance(trees[i].transform.position, trees[j].transform.position) <= maxFireRange) trees[i].adjacentTrees.Add(trees[j]);
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Restart"))
        {
            DeleteTreesAndStartAgain();
        }
    }

    void DeleteTreesAndStartAgain()
    {
        for (int i = 0; i < trees.Count; i++)
        {
            Destroy(trees[i].gameObject);
        }
        StartGame();
    }
}
