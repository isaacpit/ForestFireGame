﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float initialDirection;

    public NewCharacterController player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()   
    {
        // transform.localEulerAngles = new Vector3(0, 0, initialDirection + player.transform.localEulerAngles.y + 10);
    }
}
