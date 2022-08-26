using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalkState : PlayerState
{
    public PlayerTalkState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        stateMachine.Animator.SetBool("Talk", true);
    }
    
    public override void Update()
        {
            if (stateMachine.IsIdle)
            {
                stateMachine.Animator.SetBool("Talk", false);
                stateMachine.State = new PlayerIdleState(stateMachine);
            }
        }
}
