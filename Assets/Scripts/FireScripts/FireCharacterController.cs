using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// credit: 

public class FireCharacterController : MonoBehaviour
{
    public float CurrentHealth, HealthTickRate;
    public Image HealthImage;
    bool OnFire = false;
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
        if (OnFire)
        {
            DecreaseHealth();
        }
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
        OnFire = false;
    }

    void OnTriggerStay(Collider collision)
    {
        GroundFire fire = collision.gameObject.GetComponent<GroundFire>();
        if (fire != null && fire.currentRadius >= 1)
        {
            OnFire = true;
        }
    }

    void DecreaseHealth()
    {
        if (CurrentHealth >= 0)
        {
           // Debug.Log(CurrentHealth + "hp");
            CurrentHealth -= HealthTickRate;
            HealthImage.fillAmount = CurrentHealth / 100.0f;
        } else
        {
            Die();
        }
    }

    void Die()
    {
        //transform.Rotate(90, 0, 0);
        Destroy(this);
    }
}
