using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject partPrefab, parentObject, playerRef, hydrantRef;

    [SerializeField]
    [Range(1, 1000)]
    int length = 1;

    [SerializeField]
    float partDistance = 0.21f;

    [SerializeField]
    bool reset, spawn, snapFirst, snapLast, connect;

    [SerializeField]
    float speed = 1.0f;

    Transform last;


    // Update is called once per frame
    void Update()
    {
        if (reset) {
          foreach (GameObject tmp in GameObject.FindGameObjectsWithTag("rope")) {
            Destroy(tmp);
          }

          reset = false;
        }

        if (spawn) {
          Spawn();

          spawn = false;
        }

        if (connect) {
          // Transform last = parentObject.transform.Find((parentObject.transform.childCount + 1).ToString());

          float step =  speed * Time.deltaTime;
          Debug.Log("step: " + step);
          Debug.Log("last: " + last.name);
          Debug.Log("pos: " + last.position);
          Debug.Log("parent: " + last.parent.position);
          last.position = Vector3.Lerp(last.position, last.parent.position, step);
          Debug.Log(last.position);



        }
    }

    

    public void Spawn() {
      int count = (int) (length / partDistance);

      for (int i = 0; i < count; ++i) {
        GameObject tmp;

        tmp = Instantiate(partPrefab, new Vector3(transform.position.x, transform.position.y + partDistance * (i + 1), transform.position.z), Quaternion.identity, parentObject.transform);
        // tmp = Instantiate(partPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, parentObject.transform);
        tmp.transform.eulerAngles = new Vector3(180, 0, 0);
        tmp.name = parentObject.transform.childCount.ToString();

        if (i == 0) {
          // Destroy(tmp.GetComponent<CharacterJoint>());
          if (snapFirst) {

            // tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            // tmp.GetComponent<CharacterJoint>().connectedBody = hydrantRef.GetComponent<Rigidbody>();
            
          }
        }
        else {
          tmp.GetComponent<CharacterJoint>().connectedBody = parentObject.transform.Find((parentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
        }

      }
      
      if (snapLast) {
        last = parentObject.transform.Find(parentObject.transform.childCount.ToString());
        last.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        last.parent = playerRef.gameObject.transform;
        
        connect = true;
        
      }
    }
}
