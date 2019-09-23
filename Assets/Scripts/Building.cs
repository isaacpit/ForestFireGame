using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public int WaterState = 0;//0 = Dry, 1 = Being Watered
    public int FireState = 0;//0 = Dry, 1 = On Fire, 3 = Burnt
    public float FireBar, WaterBar = 0;
    private float HealthBar = 100.0f;
    public GameObject[] trees;
    public List<GameObject> adjacentTrees = new List<GameObject>();
    //public List<GameObject> nearbyTrees = new List<GameObject>();
    public Material Red;
    public Material Green;
    public Material Black;
    public Material LightBlue;
    public Image HealthImage, FireImage, WaterImage;
    public float FireBarFrameRate = .5f;
    public float WaterBarFrameRate = 4.0f;
    public float HealthBarFrameRate = 2.0f;
    /*public float timeToCatchOnFire = 3;
    public float timeToCatchOnFireFromWet = 3;*/
    public float timeToBurnOut = 2;
    public bool startingToCatchFire = false;

    void Start()
    {
        trees = GameObject.FindGameObjectsWithTag("tree");
        for (int i = 0; i < trees.Length; ++i)
        {
            if (Vector3.Distance(trees[i].transform.position, gameObject.transform.position) <= 5) adjacentTrees.Add(trees[i]);
        }
        if (FireState == 1)
        {
            FireBar = 100.0f;
            StartCoroutine(catchFire());

        }
    }

    void Update()
    {
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
        }
        if (FireState == 1)
        {
            HealthBar -= HealthBarFrameRate * (FireBar / 100.0f);
        }
        for (int i = 0; i < adjacentTrees.Count; i++)
        {
            if (adjacentTrees[i].GetComponent<Tree>().FireState == 1)
            {
                FireBar += FireBarFrameRate;
                if (!startingToCatchFire)
                {
                    startingToCatchFire = true;
                    StartCoroutine(catchFire());
                }
                break;
            }
        }
        //Debug.Log(FireBar + " and " + WaterBar);
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
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "water")
        {
            WaterState = 1;
            Debug.Log("Water Enter");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "water")
        {
            WaterState = 0;
            Debug.Log("Water Exit");
        }
    }

    private IEnumerator catchFire()
    {
        while (FireBar < 100.0f && FireBar > 0.0f)
        {
            yield return null;
        }
        if (FireBar >= 100.0f)
        {
            FireState = 1;
            FireBar = 100.0f;
            gameObject.GetComponent<Renderer>().material = Red;
            StartCoroutine(burnOut());
        }
    }

    private IEnumerator burnOut()
    {
        while (FireBar >= 0 && HealthBar >= 0)
        {
            yield return null;
        }
        if (HealthBar >= 0)
        {
            FireState = 0;
            gameObject.GetComponent<Renderer>().material = Green;
        }
        else
        {
            FireState = 2;
            gameObject.GetComponent<Renderer>().material = Black;
        }

    }
}