using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCState 
{
    public abstract void Update();
    
    protected NPCStateMachine stateMachine;

    public NPCState(NPCStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
