using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : NPCState
{
    public NPCIdleState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.Animator.SetBool("NPCIdle", true);
    }

    public override void Update()
    {
        if (stateMachine.Activated)
        {
            stateMachine.Activated = false;
            if(stateMachine.IsWalking)
            {
                stateMachine.Animator.SetBool("NPCIdle", false);
                stateMachine.State = new NPCWalkState(stateMachine);
            }
        }
    }
}
