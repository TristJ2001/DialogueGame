using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalkState : NPCState
{
    public NPCWalkState(NPCStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.Animator.SetBool("NPCWalk", true);
        // Debug.Log("Idle is true");
    }

    public override void Update()
    {
        if (stateMachine.Activated)
        {
            stateMachine.Activated = false;
            if (stateMachine.IsIdle)
            {
                stateMachine.Animator.SetBool("NPCWalk", false);
                stateMachine.State = new NPCIdleState(stateMachine);
            }
        }
    }
}
