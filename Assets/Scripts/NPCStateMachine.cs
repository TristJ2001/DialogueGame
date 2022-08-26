using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine : MonoBehaviour
{
    public NPCState State{ get; set; }
    
    public Animator Animator { get; protected set; }
    
    public bool IsWalking { get; protected set; }
    
    public bool IsIdle { get; protected set; }
    
    public bool Activated { get; set; }
}
