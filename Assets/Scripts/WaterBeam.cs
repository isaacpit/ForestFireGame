using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBeam : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        // guaranteed to be tree bc tree layer
        // but check just in case 
        Tree tree = other.GetComponent<Tree>();
        if (tree == null) return;

        if (tree.WaterState != 1)
            tree.ChangeWaterState();

        
    }
    
}
