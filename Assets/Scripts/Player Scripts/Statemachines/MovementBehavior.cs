using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : StateMachineBehaviour
{
   //[SerializeField] Vector3 direction;
    [SerializeField] float move;
    [SerializeField] private bool forward;
    Player zend;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        zend = Player.GetPlayer();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!forward) {
            zend.CharCon.Move(zend.transform.right * move * Time.deltaTime);
        }
        else {
            zend.CharCon.Move(zend.transform.forward * move * Time.deltaTime);
        }
    }

}
