using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    public int WaterState = 0;//0 = Dry, 1 = Being Watered
    public int FireState = 0;//0 = Dry, 1 = On Fire, 3 = Burnt
    public float FireBar, WaterBar = 0;
    public float HealthBar = 100.0f;
    public Mesh HealthyTree, DeadTree;
    //public List<Tree> adjacentTrees = new List<Tree>();
    public Material Red;
    public Material Green;
    public Material Black;
    public Material LightBlue;
    public Image HealthImage, FireImage, WaterImage;
    public GameObject BarsObject;
    public float FireBarFrameRate;
    public float WaterBarFrameRate;
    public float HealthBarFrameRate;
    public float FireCatchPoint;
    public bool isHouse = false;
    private MeshCollider mc;
    private GroundFire groundFire;
    public Vector2 TotalWind;
    [SerializeField] ParticleSystem PS_FireParent;
    float lastWatered = 0;
    [SerializeField] float wateredDiff = 0.15f;

    public AudioSource fireAudioSource;

    public MeshRenderer mr;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mc = GetComponent<MeshCollider>();

        groundFire = GetComponentInChildren<GroundFire>();
        if (FireState == 1)
        {
            FireBar = 100.0f;
            StartFirePS();
            StartCoroutine(burnOut());
            fireAudioSource.Play();
        }
        else
        {
            // prevent fire from starting auto starting
            StopFirePS();
        }
    }
    

    void Update()
    {
        //print(gameObject.name + " First Status: " + fireAudioSource.isPlaying);//Used for initial Debugw

        if(Time.timeScale == 1)
        {
            if (WaterBar >= 20)
            {
                mc.isTrigger = true;
            }
            else
            {
                mc.isTrigger = false;
            }

            if (WaterState == 1)
            {
                if (FireBar >= 0)
                {
                    FireBar -= WaterBarFrameRate;
                }
                else
                {
                    WaterBar += WaterBarFrameRate;
                }
                fireAudioSource.Pause();
            }

            if (FireState != 1 && FireBar >= FireCatchPoint)
            {
                FireState = 1;
                StartFirePS();
                StartCoroutine(burnOut());
                fireAudioSource.Play();
            }
            else if (FireState == 1)
            {
                IncreaseFireBar();
                DecreaseHealth(HealthBarFrameRate * (FireBar / 100.0f));
                mr.materials[1].color = Color.Lerp(new Color(0, 0, 0), mr.materials[1].color, HealthBar / 100.0f);

            }

            if (FireBar >= 0)
            {
                FireImage.fillAmount = FireBar / 100;
                WaterImage.fillAmount = 0;
            }
            else
            {
                WaterImage.fillAmount = WaterBar / 100;
                FireImage.fillAmount = 0;
            }
            HealthImage.fillAmount = HealthBar / 100;
            MoveFireFromWind();
        }
    }

    void MoveFireFromWind()
    {
        float firePercentage = FireBar / 100;
        Vector2 effectiveWind = firePercentage * (TotalWind * 2.5f);
        groundFire.transform.localPosition = new Vector3(effectiveWind.x, .1f, effectiveWind.y);
    }

    public void DecreaseHealth(float amount)
    {
        HealthBar -= amount;
        if (HealthBar <= 0)
        {
            StartCoroutine("AxeKill");
        }
       // HealthImage.SetActive(true);
    }

    private IEnumerator AxeKill()
    {
        for (int i = 0; i < 15; i++)
        {
            yield return null;
        }
        KillTree();
    }

    void OnTriggerStay(Collider collision)
    {
        GroundFire fire = collision.gameObject.GetComponent<GroundFire>();
        if (fire != null && FireState != 2 && fire != groundFire)
        {
            IncreaseFireBar();
        }
    }

    // turn off water state after some time of not being watered
    private IEnumerator waterDecay()
    {
        while (WaterState != 0)
        {
            float currTime = Time.time;
            if (currTime - lastWatered > wateredDiff && WaterState == 1)
            {
                WaterState = 0;
            }
            yield return null;
        }
        


    }

    void IncreaseFireBar()
    {
        if (WaterBar >= 0)
        {
            WaterBar -= FireBarFrameRate;

        } else
        {
            FireBar += FireBarFrameRate;
            if (FireBar >= 100.0f)
            {
                FireBar = 100.0f;
            }
        }

    }

    public void ChangeWaterState()
    {
        WaterState = 1;
        lastWatered = Time.time;

        StartCoroutine(waterDecay());
    }

    public void KillTree()
    {
        FireState = 2;
        FireBar = 0;
        GetComponent<MeshFilter>().mesh = DeadTree;
        GetComponent<MeshCollider>().sharedMesh = DeadTree;
        gameObject.GetComponent<Renderer>().material = Black;
        gameObject.GetComponent<MeshCollider>().sharedMesh = DeadTree;
        if (BarsObject) BarsObject.SetActive(false);
        fireAudioSource.Pause();
    }

    private IEnumerator burnOut()
    {
        while (FireBar >= FireCatchPoint && HealthBar >= 0)
        {
            yield return null;
        }
        StopFirePS();
        if (HealthBar >= 0)
        {
            FireState = 0;
        }
        else
        {
            KillTree();
        }
    }

    void StartFirePS()
    {
        PS_FireParent.Play();
    }

    void StopFirePS()
    {
        PS_FireParent.Stop();
    }
}