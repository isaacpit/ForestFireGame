using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStartStopDemo : MonoBehaviour
{

    ParticleSystem PS_parent;
    bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        PS_parent = GetComponent<ParticleSystem>();
        PS_parent.Play();
        isPlaying = PS_parent.isPlaying;
    }

    void Toggle()
    {
        if (isPlaying)
        {
            PS_parent.Stop();
        }
        else
        {
            PS_parent.Play();
        }
        isPlaying = !isPlaying;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Toggle();
        }
    }
}
