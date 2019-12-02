using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFire : MonoBehaviour
{
    public float maxRadius;
    private Tree myTree;
    public float currentRadius;
    public GameObject parent;
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
        parent.transform.localEulerAngles = new Vector3(0, -myTree.transform.localEulerAngles.y, 0);
    }

    float CalculateRadius(float currentFireBar)
    {
        return (currentFireBar / 100.0f) * maxRadius;
    }
}
