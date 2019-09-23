using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCharacterController : MonoBehaviour
{
    public float movementSpeed = 7.5f;
    public float rotateSpeed = 1.5f;
    public ParticleSystem waterStream;
    
    
    private Vector3 waterSize;
    // Start is called before the first frame update
    void Start()
    {
        waterSize = waterStream.transform.localScale;
        waterStream.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0.0f) transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime, Space.World);
        if (Input.GetAxis("Vertical") < 0.0f) transform.Translate(Vector3.back * movementSpeed * Time.deltaTime, Space.World);
        if (Input.GetAxis("Horizontal") > 0.0f) transform.Translate(Vector3.right * movementSpeed * Time.deltaTime, Space.World);
        if (Input.GetAxis("Horizontal") < 0.0f) transform.Translate(Vector3.left * movementSpeed * Time.deltaTime, Space.World);

        if (Input.GetAxis("Horizontal2") > 0.0f) transform.Rotate(Vector3.up * 100 * rotateSpeed * Time.deltaTime);
        if (Input.GetAxis("Horizontal2") < 0.0f) transform.Rotate(-Vector3.up * 100 * rotateSpeed * Time.deltaTime);

        //Debug.Log("fire button held: " + Input.GetButton("Fire1"));
        //Debug.Log("water stream playing: " + waterStream.isPlaying);

        //if (Input.GetButtonDown("Fire1")) waterStream.transform.localScale = waterSize;
        //if (Input.GetButtonUp("Fire1")) waterStream.transform.localScale = Vector3.zero;
        if (Input.GetButtonDown("Fire1"))
        {
            waterStream.Play();

        }
        else if (Input.GetButtonUp("Fire1"))
        {
            waterStream.Stop();
        }
        //else
        //{
        //    waterStream.Stop();
        //}
        //if (Input.GetButtonUp("Fire1")) waterStream.transform.localScale = Vector3.zero;


    }
}
