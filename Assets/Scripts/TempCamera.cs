using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCamera : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, 0.0f, target.position.z); ;
        transform.rotation = Quaternion.Euler(0.0f,target.localEulerAngles.y, 0);
    }
}
