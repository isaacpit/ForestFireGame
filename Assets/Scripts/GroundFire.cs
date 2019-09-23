using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFire : MonoBehaviour
{
    public float maxRadius;
    private Tree myTree;
    public float currentRadius;

    // Start is called before the first frame update
    void Start()
    {
        myTree = GetComponentInParent<Tree>();
    }

    // Update is called once per frame
    void Update()
    {
            currentRadius = CalculateRadius(myTree.FireBar);
            transform.localScale = new Vector3(currentRadius, .001f, currentRadius);
        
    }

    float CalculateRadius(float currentFireBar)
    {
        return (currentFireBar / 100.0f) * maxRadius;
    }
}
