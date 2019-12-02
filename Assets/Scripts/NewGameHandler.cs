using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameHandler : MonoBehaviour
{
    public List<GameObject> buildings;
    public List<GameObject> trees;

    public GameObject endGameUI;
    public GameObject userUI;

    public GameObject player;

    public int numberOfHousesNeededToWin;

    void Start()
    {
        buildings = new List<GameObject>(GameObject.FindGameObjectsWithTag("building"));
        trees = new List<GameObject>(GameObject.FindGameObjectsWithTag("tree"));

        Randomize(trees);
    }

    // Update is called once per frame
    void Update()
    {
        RemoveBurntHouses();

        //Player has lost
        if (buildings.Count < numberOfHousesNeededToWin || player.GetComponent<NewCharacterController>().CurrentHealth <= 0)
        {
            print("player has lost, Too many houses destroyed");
            endGameUI.GetComponent<EndGameMenu>().GameOver(false);
            userUI.SetActive(false);
            gameObject.GetComponent<NewGameHandler>().enabled = false;
        }

        //Player has won - No more elements are on fire
        if ( !AnyTreesOnFire() && !AnyBuildingsOnFire() && player.GetComponent<NewCharacterController>().CurrentHealth > 0)//All fires are finished
        {
            if(buildings.Count >= numberOfHousesNeededToWin)
            {
                print("player has won, minimum number of houses saved");
                endGameUI.GetComponent<EndGameMenu>().GameOver(true);
                userUI.SetActive(false);
                gameObject.GetComponent<NewGameHandler>().enabled = false;
            }
        }
    }

    bool AnyTreesOnFire()
    {
        //Checks to see if any buildings at all are on fire
        bool tempBool = false;
        for (int i = 0; i < trees.Count; i++)
        {
            if (trees[i].GetComponent<Tree>().FireState == 1)
            {
                tempBool = true;
            }
        }
        return tempBool;
    }

    bool AnyBuildingsOnFire()
    {
        //Checks to see if any trees at all are on fire
        bool tempBool = false;
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].GetComponent<Tree>().FireState == 1)
            {
                tempBool = true;
            }
        }
        return tempBool;
    }

    void RemoveBurntHouses()
    {
        //Removes burnt houses from buildings list so the count decreases.
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].GetComponent<Tree>().FireState == 2)
            {
                buildings.Remove(buildings[i]);
            }
        }
    }

    public void Randomize(List<GameObject> Trees)
    {
        for(int i = 0; i < Trees.Count; i++)
        {
            float randomWidth = Random.Range(0.75f, 1.25f);
            Trees[i].transform.localScale = new Vector3(randomWidth, Random.Range(0.75f, 1.25f), randomWidth);

            //Randomly assigns a color to the tree
            Color temp = new Color(0, Random.Range(0.2f, 0.6f), 0, 1.0f);
            Trees[i].GetComponent<MeshRenderer>().materials[1].SetColor("_Color", temp);
        }
    }
}