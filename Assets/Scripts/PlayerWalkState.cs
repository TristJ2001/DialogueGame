using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public delegate void DestinationReachedAction();
    public static event DestinationReachedAction OnDestinationReached;
    
    public PlayerWalkState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.Animator.SetBool("Walk", true);
        // Debug.Log("Walk is true");
    }

    public override void Update()
    {
        stateMachine.transform.right = stateMachine.NavMeshAgent.velocity;
        stateMachine.Animator.speed = stateMachine.NavMeshAgent.velocity.magnitude;
        
        if (!stateMachine.NavMeshAgent.hasPath)
        {
            OnDestinationReached?.Invoke();
            
            if (stateMachine.IsTalking)
            {
                stateMachine.Animator.SetBool("Walk", false);
                stateMachine.State = new PlayerTalkState(stateMachine);
                return;
            }
            
            stateMachine.Animator.SetBool("Walk", false);
            // Debug.Log("Walk is false");
            // stateMachine.Animator.SetBool("Idle", true);
            // Debug.Log("Idle is true");
            stateMachine.State = new PlayerIdleState(stateMachine);
        }
    }
}
