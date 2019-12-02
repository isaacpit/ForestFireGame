using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrant : MonoBehaviour
{
    public NewCharacterController player;
    public float MinDistance, WaterRefillRate;
    private bool HydrantPlaced = true;
    private bool HydrantDestroyed = false;
    public AudioSource audio_source;
    public ParticleSystem waterExplosion;

    // Start is called before the first frame update
    void Start()
    {
        Transform newTrans = player.FireHydrantTransform;
        transform.parent = player.transform;
        transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
        transform.position = newTrans.position;

        HydrantPlaced = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRangeOfHydrant() && player.CurrentWater < 100 && HydrantPlaced && !HydrantDestroyed)
        {
            player.InRangeOfHydrant(WaterRefillRate);
        }
    }

    public bool isPlayerInRangeOfHydrant()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= MinDistance && HydrantPlaced;
    }

    public void ExplodeHydrant()
    {
        waterExplosion.Play();
        HydrantDestroyed = true;
        StartCoroutine("DepleteHydrant");
    }

    public void pickUpHydrant() {
        Transform newTrans = player.FireHydrantTransform;
        transform.parent = player.transform;
        transform.localEulerAngles = new Vector3(-90.0f, 0, 0);
        transform.position = newTrans.position;
        HydrantPlaced = false;
    }

    public void placeHydrant()
    {
        StartCoroutine("waitToPlaceHydrant");
    }

    IEnumerator waitToPlaceHydrant()
    {
        for (int i = 0; i < 15; i++)
        {
            yield return null;
        }
        audio_source.Play();
        transform.rotation = player.transform.rotation;
        transform.localPosition = new Vector3(0, 0.0f, 1.5f);
        transform.parent = null;
        HydrantPlaced = true;
        player.equipHose();
    }

    IEnumerator DepleteHydrant()
    {
        for (int i = 0; i < 1000; i++)
        {
            yield return null;
        }
        waterExplosion.Stop();
    }
}
