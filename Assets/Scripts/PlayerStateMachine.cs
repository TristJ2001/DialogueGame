using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState State{ get; set; }
    
    public Animator Animator { get; protected set; }
    
    public NavMeshAgent NavMeshAgent { get; protected set; }
    
    public LayerMask LayerMask { get; protected set; }
    
    public bool IsTalking { get; protected set; }
    
    public bool IsWalking { get; protected set; }
    
    public bool IsIdle { get; protected set; }
    
    public string Target { get; protected set; }
    
    public bool Activated { get; set; }
}
