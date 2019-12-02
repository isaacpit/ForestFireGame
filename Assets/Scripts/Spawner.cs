using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    // public int xGridWidth = 10;
    // public int zGridWidth = 7;
    public GameObject ground;
    public float minDistanceBetweenTrees;
    public float WindSpeed = -1; //Enter -1 for a random Wind Speed
    public float WindDirection = -1; //-1: Random 0:None 1:N 2:NE 3:E 4:SE 5:S 6:SW 7:W 8:NW
    public TextMeshProUGUI windSpeed;
    public WindScript windDisplay;
    //public Tree TreePrefab;
    public List<Tree> TreePrefabs = new List<Tree>();
    public int TreeCount;
    public Tree HousePrefab;
    public int HouseCount;
    public float minDistanceAwayFromHouse;

    public List<Tree> trees = new List<Tree>();
    public GameObject ObjectPool;

    [SerializeField] GameObject treeSpawnBox;

    [SerializeField] GameObject houseSpawnBox;

    [SerializeField] List<ElementSpawner> houseSpawners;

    [SerializeField] List<ElementSpawner> treeSpawners;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {

        // ground.transform.localScale = new Vector3(xGridWidth * 10, 0.5f, zGridWidth * 10);
        // ground.GetComponent<Renderer>().material.mainTextureScale = new Vector2(xGridWidth, zGridWidth);

        
        Vector2 TotalWind = CalculateWind();
        //Spawn Houses
        bool exitForLoop = false;

        exitForLoop = false;

        // spawn houses
        for (int i = 0; i < houseSpawners.Count; ++i) {
          houseSpawners[i].SpawnElements(TotalWind);
        }

        for (int i = 0; i < treeSpawners.Count; ++i) {
          treeSpawners[i].SpawnElements(TotalWind);
        }

        ManagerSpawner.Instance.StartFire();
        // trees[HouseCount + 1].FireState = 1;
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

    Vector2 CalculateWind()
    {
        int xDir = Random.Range(-1, 1);
        int zDir = Random.Range(-1, 1);
        float power;
        if (WindSpeed == -1)
        {
            power = Random.Range(1.0f, 10.0f);
        }else
        {
            power = WindSpeed;
        }
        
        windSpeed.text = power.ToString("F0") + " MPH";
        if ((xDir == 0 && zDir == 0 && WindDirection == -1) || WindDirection == 0)
        {
            windSpeed.gameObject.SetActive(false);
            windDisplay.gameObject.SetActive(false);
        } else if ((xDir == 1 && zDir == 0 && WindDirection == -1) || WindDirection == 3)
        {
            windDisplay.initialDirection = 315;
        } else if ((xDir == 1 && zDir == -1 && WindDirection == -1) || WindDirection == 4)
        {
            windDisplay.initialDirection = 270;
        } else if ((xDir == 0 && zDir == -1 && WindDirection == -1) || WindDirection == 5)
        {
            windDisplay.initialDirection = 225;
        } else if ((xDir == -1 && zDir == -1 && WindDirection == -1) || WindDirection == 6)
        {
            windDisplay.initialDirection = 180;
        } else if ((xDir == -1 && zDir == 0 && WindDirection == -1) || WindDirection == 7)
        {
            windDisplay.initialDirection = 135;
        } else if ((xDir == -1 && zDir == 1 && WindDirection == -1) || WindDirection == 8)
        {
            windDisplay.initialDirection = 90;
        }else if ((xDir == 0 && zDir == 1 && WindDirection == -1) || WindDirection == 1)
        {
            windDisplay.initialDirection = 45;
        }
        return new Vector2((float)(xDir * (power / 10.0f)), (float)(zDir * (power / 10.0f)));
    }
}
