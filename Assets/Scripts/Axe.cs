using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public bool canSwing, canHit, canCharge = false;
    public Animator axeAnimator;
    public AudioSource axeChopAudioSource;
    public AudioSource axeSwingAudioSource;
    // Start is called before the first frame update

    public int axeChargeCounter = 0;

    public int AXE_CHARGE_MAX = 100;
    void Start()
    {
        canSwing = true;
        canCharge = true;
    }

    void OnTriggerStay(Collider collider)
    {
      Debug.Log("hit tree: " + canSwing + canHit);
        Tree hitTree = collider.gameObject.GetComponent<Tree>();
        FireHydrant hydrant = collider.gameObject.GetComponent<FireHydrant>();
        if (canHit && hitTree != null && !hitTree.isHouse && hitTree.FireState != 2)
        {
            hitTree.KillTree();
            //canHit = false;
            axeChopAudioSource.Play();
        } else if (canHit && hydrant != null)
        {
            hydrant.ExplodeHydrant();
            // canHit = false;
        }
    }

    public void swingAxe()
    {
        axeAnimator.SetTrigger("AxeSwing");
        // canHit = true;
        // axeAnimator.SetBool("AxeSwing", true);
        // axeAnimator.SetBool("AxeCharge", false);
        axeChargeCounter = 0;
        // axeAnimator.SetInteger("AxePower", axeChargeCounter);
    }

    public void windupAxe() {
      axeAnimator.SetTrigger("AxeWindUp");
    }


    public void chargeAxe() {
      // if (canCharge) {
      //   StartCoroutine("axeCharge");
      // }
      Debug.Log("Charging axe: " + axeChargeCounter);
      if ( ! (axeChargeCounter > AXE_CHARGE_MAX)) {
        axeChargeCounter++;
      }
      axeAnimator.SetInteger("AxePower", axeChargeCounter);
      
    }

}
