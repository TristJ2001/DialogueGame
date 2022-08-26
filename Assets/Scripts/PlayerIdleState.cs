using System.Collections;
using System.Collections.Generic;
using Prototype.Scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private const float speed = 10f;

    private bool walk;
    private string target;

    private Vector3 targetPosition;

    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.Animator.SetBool("Idle", true);
        // Debug.Log("Idle is true");
    }

    // private void OnEnable()
    // {
    //     Interactable.OnWalkToAction += OnWalkToAction;
    // }
    //
    // private void OnDisable()
    // {
    //     Interactable.OnWalkToAction += OnWalkToAction;
    // }

    private void OnWalkToAction(string target)
    {
        walk = true;
        this.target = target;
    }

    public override void Update()
    {
        // Debug.Log("walking");
        // if (stateMachine.IsTalking)
        // {
        //     stateMachine.Animator.SetBool("Talk", true);
        //     stateMachine.State = new PlayerTalkState(stateMachine);
        // }
        
        // if (Input.GetMouseButtonDown(0))
        // {
        //     if (stateMachine.IsTalking)
        //     {
        //         return;
        //     }
        //
        //     RaycastHit hit;
        //
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     if (Physics.Raycast(ray, out hit, 100, stateMachine.LayerMask))
        //     {
        //         stateMachine.NavMeshAgent.destination = hit.point;
        //         // Debug.Log(hit.point);
        //         // Debug.DrawLine(hit.point, hit.point + Vector3.up, Color.red, 1000);
        //         stateMachine.Animator.SetBool("Idle", false);
        //         stateMachine.State = new PlayerWalkState(stateMachine);
        //     }
        // }

        if (stateMachine.Activated)
        {
            stateMachine.Activated = false;
            if (stateMachine.IsTalking)
            {
                stateMachine.Animator.SetBool("Idle", false);
                stateMachine.State = new PlayerTalkState(stateMachine);
            }
            
            if(stateMachine.IsWalking)
            {
                // // Debug.Log("walking");
                // if (stateMachine.IsTalking)
                // {
                //     stateMachine.Animator.SetBool("Talk", true);
                //     stateMachine.State = new PlayerTalkState(stateMachine);
                // }

                targetPosition = GameObject.FindGameObjectWithTag(stateMachine.Target).transform.position;
                Debug.Log(targetPosition);
                stateMachine.NavMeshAgent.destination = targetPosition;
            
                stateMachine.Animator.SetBool("Idle", false);
                // Debug.Log("Idle is false");
                // stateMachine.Animator.SetBool("Walk", true);
                // Debug.Log("Walk is true");
                stateMachine.State = new PlayerWalkState(stateMachine);
            }
            // else
            // {
            //     stateMachine.Animator.SetBool("Idle", true);
            //     // Debug.Log("Idle is true");
            //     stateMachine.Animator.SetBool("Walk", false);
            //     // Debug.Log("Walk is false");
            //     stateMachine.State = new PlayerIdleState(stateMachine);
            // }
            // else
            // {
            //     Debug.Log("error");
            // }
        }
        
    }
}
