using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Handler : MonoBehaviour
{
    public GameObject[] buildings;
    // Start is called before the first frame update
    void Start()
    {
        buildings = GameObject.FindGameObjectsWithTag("building");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
