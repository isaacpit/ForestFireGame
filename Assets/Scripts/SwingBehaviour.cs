using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBehaviour : StateMachineBehaviour
{
   

    public void Start() {
      // myAxe = GOmyAxe.GetComponent<Axe>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // myAxe.canSwing = true;
        // if (stateInfo.shortNameHash == Animator.StringToHash("AxeSwing") || stateInfo.shortNameHash == Animator.StringToHash("AxeSwingPoweredUp") ) {
        //   Debug.Log("Enter state!");
        //   Axe axeRef = animator.gameObject.GetComponent<NewCharacterController>().Axe.GetComponent<Axe>();
        //   axeRef.canHit = true;
        // }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("State Exit:" + stateInfo.shortNameHash);
        Debug.Log("StateAxeSwing: " + Animator.StringToHash("AxeSwing"));
        Debug.Log("StateAxePoweredUpSwing: " + Animator.StringToHash("AxeSwingPoweredUp"));
        // myAxe.canSwing = false;

        
        if (stateInfo.shortNameHash == Animator.StringToHash("AxeSwing") || stateInfo.shortNameHash == Animator.StringToHash("AxeSwingPoweredUp") ) {
          Debug.Log("Exit state!");
          animator.SetInteger("AxePower", 0);
            NewCharacterController player = animator.gameObject.GetComponent<NewCharacterController>();
          Axe axeRef = player.Axe.GetComponent<Axe>();
          axeRef.canHit = false;
            player.equipHose();
        }

        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
