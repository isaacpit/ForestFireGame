using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// credit: 

public class NewCharacterController : MonoBehaviour
{
    public float CurrentHealth, HealthTickRate, CurrentWater, WaterTickRate, CurrentStamina, StaminaTickRate;
    public Transform FireHydrantTransform;
    public Image HealthImage, WaterImage, StaminaImage;
    public int HydrantCount;
    public FireHydrant hydrantPrefab;
    private List<FireHydrant> activeHydrants = new List<FireHydrant>();
    private FireHydrant currentHydrant;
    public GameObject Hose, Axe;
    bool OnFire = false;
    CharacterController characterController;
    private float movementSpeed;
    public float sprintSpeed, walkSpeed;
    private Vector3 moveDirection = Vector3.zero;
    public float rotationSpeed;
    [SerializeField] ParticleSystem waterStream;
    private Image currentTool;
    [SerializeField] Vector3 mouse_pos;
    private bool sprayingWater, hoseEquipped, axeEquipped, hydrantEquipped, dodging = false;
    Vector3 currentDirection;

    Ray clickRay;

    Vector3 pointA = new Vector3(0.0f, 1.0f, 0.0f);
    Vector3 pointB = new Vector3(1.0f, 1.0f, 0.0f);
    Vector3 pointC = new Vector3(1.0f, 1.0f, 1.0f);

    Plane playerHeightPlane;

    Vector3 hitPoint;
    

    Vector2 ptScreenCenter;

    public AudioSource waterAudioSource;

    void Start()
    {
        movementSpeed = walkSpeed;
        characterController = GetComponent<CharacterController>();
        playerHeightPlane = new Plane(pointA, pointB, pointC);
        CurrentWater = 0;
        ptScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    public void rotateTowardsMouse() {
        mouse_pos = Input.mousePosition;
        // if (-10f < mouse_pos && mouse_pos < )

        // Debug.Log("mouse_pos" + mouse_pos);
        float distanceFromCenterX = Mathf.Abs(ptScreenCenter.x -mouse_pos.x);
        // Debug.Log("distance from center: " + distanceFromCenterX);
        if (distanceFromCenterX < 10) {
          return;
        }
        clickRay = Camera.main.ScreenPointToRay(mouse_pos);
        currentDirection = transform.forward * 10;

        float enter = 0.0f;
        if (playerHeightPlane.Raycast(clickRay, out enter))
        {
            //Get the point that the ray intersects the plane
            hitPoint = clickRay.GetPoint(enter);

            Vector3 targetDir = hitPoint - transform.position;
            // makes it only rotate in X and Z
            targetDir.y = 0; 

            // The step size is equal to speed times frame time.
            
            float step = rotationSpeed * Time.deltaTime * (distanceFromCenterX / (Screen.width / 2));

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            // Debug.DrawRay(transform.position, newDir, Color.red);

            // rotate towards direction of mouse
            transform.rotation = Quaternion.LookRotation(newDir);
            //characterController.transform.rotate = Quaternion.LookRotation(newDir);

        }

       
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
       
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, currentDirection + transform.position);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, hitPoint);

        // Gizmos.color = Color.green;
        // Debug.DrawLine(clickRay.origin, hitPoint);

        
        Gizmos.color = Color.red;
        Debug.DrawLine(pointA, pointB);
        Debug.DrawLine(pointB, pointC);
        Debug.DrawLine(pointC, pointA);

    }

    void Update()
    {
        //Debug.Log("rotate axis: "); Debug.Log(Input.GetAxis("Rotate"));
        //transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
        //transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
        /*else if (currentTool.gameObject.name == "Axe")
        {
            Hose.SetActive(false);
            Axe.SetActive(true);
            axeEquipped = true;
            hoseEquipped = hydrantEquipped = false;
            if (currentHydrant != null)
            {
                currentHydrant.gameObject.SetActive(false);
            }
        }
        else if (currentTool.gameObject.name == "Hydrant")
        {
            Hose.SetActive(false);
            Axe.SetActive(false);
            hydrantEquipped = true;
            hoseEquipped = axeEquipped = false;
            if (currentHydrant != null)
            {
                currentHydrant.gameObject.SetActive(true);
                
            }
            else if (HydrantCount > 0)
            {
                currentHydrant = Instantiate(hydrantPrefab, this.gameObject.transform);
                currentHydrant.player = this;
                HydrantCount--;
            }
        }*/
        if (OnFire && !dodging)
        {
            DecreaseHealth();
        }

        moveDirection = Vector3.Normalize(Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward);

        if (Input.GetButtonDown("Fire1") && (CurrentWater > 0))
        {
            waterStream.Play();
            sprayingWater = true;
            waterAudioSource.Play();
        }
        else if(Input.GetButtonUp("Fire1") || CurrentWater <= 0)
        {
            waterStream.Stop();
            sprayingWater = false;
            waterAudioSource.Pause();
        }

        if (Input.GetButtonDown("Select"))
        {
            bool hydrantPickedUp = false;
            for (int i = 0; i < activeHydrants.Count; i++)
            {
                if (activeHydrants[i].isPlayerInRangeOfHydrant())
                {
                    activeHydrants[i].pickUpHydrant();
                    hydrantPickedUp = true;
                    currentHydrant = activeHydrants[i];
                    break;
                }
            }
            if (!hydrantPickedUp)
            {
                placeHydrant();
            }
        }



        if (Input.GetButtonDown("Fire2")) {
            Axe.gameObject.SetActive(true);
            Hose.gameObject.SetActive(false);
            Debug.Log("winding up axe");
            Axe.GetComponent<Axe>().windupAxe();
        }
        else if (Input.GetButton("Fire2")) {
            Debug.Log("charging axe");
            Axe.GetComponent<Axe>().chargeAxe();
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            Debug.Log("swinging axe");
            Axe.GetComponent<Axe>().swingAxe();
        }

        if (Input.GetButtonDown("Sprint") && CurrentStamina >= 50.0f)
        {
            CurrentStamina -= 50.0f;
            StartCoroutine("PlayerDodge");
        }
        else if (CurrentStamina < 100 && !dodging)
        {
            CurrentStamina += StaminaTickRate;
            movementSpeed = walkSpeed;
        }
        StaminaImage.fillAmount = CurrentStamina / 100;

        if (sprayingWater)
        {
            CurrentWater -= WaterTickRate;
            WaterImage.fillAmount = CurrentWater / 100;

        }

        //Gravity
        //moveDirection.y -= 10f * Time.deltaTime;
        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
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

    public void InRangeOfHydrant(float refillRate)
    {
        CurrentWater += refillRate;
        WaterImage.fillAmount = CurrentWater / 100;
    }

    void DecreaseHealth()
    {
        if (CurrentHealth > 0)
        {
           // Debug.Log(CurrentHealth + "hp");
            CurrentHealth -= HealthTickRate;
            HealthImage.fillAmount = CurrentHealth / 100.0f;
        } else
        {
            Die();
        }
    }

    public void placeHydrant()
    {
        if (HydrantCount > 0 && currentHydrant == null)
        {
            FireHydrant currentHydrant = Instantiate(hydrantPrefab, this.gameObject.transform);
            currentHydrant.player = this;
            HydrantCount--;
            currentHydrant.placeHydrant();
            activeHydrants.Add(currentHydrant);
        } else if(currentHydrant != null)
        {
            currentHydrant.placeHydrant();
            currentHydrant = null;
        }
    }

    public void equipHose()
    {
        Hose.gameObject.SetActive(true);
        Axe.gameObject.SetActive(false);
    }

    void Die()
    {
        //transform.Rotate(90, 0, 0);
        waterStream.Stop();
        sprayingWater = false;
        // Destroy(this);
        this.enabled = false;
    }

    IEnumerator PlayerDodge()
    {
        dodging = true;
        movementSpeed = sprintSpeed;
        for (int i = 0; i < 50; i++)
        {
            yield return null;
        }
        dodging = false;
    }

    public void setAxeCanHit() {
      Axe.GetComponent<Axe>().canHit = true;
    }
}