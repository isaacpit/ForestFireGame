using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HydrantDisplay : MonoBehaviour
{
    Image[] Hydrants;
    public NewCharacterController player;
    // Start is called before the first frame update
    void Start()
    {
        Hydrants = GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Hydrants.Length; i++)
        {
            if (i < player.HydrantCount)
            {
                Hydrants[i].gameObject.SetActive(true);
            } else
            {
                Hydrants[i].gameObject.SetActive(false);
            }
        }
    }
}
