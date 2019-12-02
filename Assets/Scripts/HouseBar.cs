using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBar : MonoBehaviour
{
    public Game_Handler gh;
    public GameObject housePrefab;
    public List<GameObject> houseList;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddHouseIcons());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AddHouseIcons()
    {
        yield return new WaitForSeconds(5);
        print("Running Add House Icons Function" + gh.buildings.Count);

        for (int i = 0; i < gh.buildings.Count; i++)
        {
            houseList.Add(Instantiate(housePrefab, gameObject.transform));
            print("House Icon Prefab added");
        }
    }
}