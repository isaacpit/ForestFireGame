using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Handler : MonoBehaviour
{
    //public GameObject[] buildings;
    public List<GameObject> buildings;//= new List<GameObject>();

    public GameObject endGameUI;
    public GameObject userUI;

    public bool buildingsAdded = false;

    public GameObject player;

    public ManagerSpawner managerSpawner;

    public bool anyTreesOnFire = true;

    public int numberOfHousesNeededToWin = 3;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddBuildings());
    }

    // update is called once per frame
    void Update()
    {
        //print("Time Scale is " + Time.timeScale);
        if(buildingsAdded)
        {
            //Player has lost
            if (buildings.Count < numberOfHousesNeededToWin || player.GetComponent<NewCharacterController>().CurrentHealth <= 0)
            {
                print("player has lost, Too many houses destroyed");
                endGameUI.GetComponent<EndGameMenu>().GameOver(false);
                userUI.SetActive(false);
                gameObject.GetComponent<Game_Handler>().enabled = false;
            }

            //Player has won - No more elements are on fire
            if(anyTreesOnFire == false && buildings.Count >= numberOfHousesNeededToWin && player.GetComponent<NewCharacterController>().CurrentHealth > 0)//All fires are finished
            {
                print("player has won, minimum number of houses saved");
                endGameUI.GetComponent<EndGameMenu>().GameOver(true);
                userUI.SetActive(false);
                gameObject.GetComponent<Game_Handler>().enabled = false;
            }

            //Removes burnt houses from buildings list so the count decreases.
            for (int i = 0; i < buildings.Count; i++)
            {
                if (buildings[i].GetComponent<Tree>().FireState == 2)
                {
                    buildings.Remove(buildings[i]);
                }
            }

            //Checks to see if any trees at all are on fire
            bool tempBool = false;
            for(int i = 0; i < managerSpawner.trees.Count; i++)
            {
                if (managerSpawner.trees[i].GetComponent<Tree>().FireState == 1)
                {
                    tempBool = true;
                }
            }
            anyTreesOnFire = tempBool;
        }
    }

    IEnumerator AddBuildings()
    {
        yield return new WaitForSeconds(2);
        buildings = new List<GameObject>(GameObject.FindGameObjectsWithTag("building"));
        buildingsAdded = true;
    }
}