using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// credit: 

public class FireCharacterController : MonoBehaviour
{
    public float CurrentHealth, HealthTickRate;
    CharacterController characterController;
    public float movementSpeed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float rotationSpeed = 0.75f;
    [SerializeField] ParticleSystem waterStream;



    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        //Debug.Log("rotate axis: "); Debug.Log(Input.GetAxis("Rotate"));
        //transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
        //transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);

        if (true)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = moveDirection * movementSpeed;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            waterStream.Play();

        }
        else if (Input.GetButtonUp("Fire1"))
        {
            waterStream.Stop();
        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log("mouse button down");
        }
        transform.Rotate(0, Input.GetAxis("Rotate") * Time.deltaTime * rotationSpeed, 0);
        //Gravity
        //moveDirection.y -= 10f * Time.deltaTime;
        transform.Translate(moveDirection);
    }
}
