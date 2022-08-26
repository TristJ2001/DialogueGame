using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCStateController : NPCStateMachine
{
    private void OnEnable()
    {
        CitizenManager.OnPlayerWalking += OnPlayerWalking;
        Unlikeable1Manager.OnPlayerWalking += OnPlayerWalking;
        PlayerController.OnActivatedAction += OnActivatedAction;
        CitizenManager.OnPlayerFinishedTalking += OnPlayerFinishedTalking;
        Unlikeable1Manager.OnPlayerFinishedTalking += OnPlayerFinishedTalking;
    }
    
    private void OnDisable()
    {
        CitizenManager.OnPlayerWalking -= OnPlayerWalking;
        Unlikeable1Manager.OnPlayerWalking -= OnPlayerWalking;
        PlayerController.OnActivatedAction -= OnActivatedAction;
        CitizenManager.OnPlayerFinishedTalking -= OnPlayerFinishedTalking;
        Unlikeable1Manager.OnPlayerFinishedTalking -= OnPlayerFinishedTalking;
    }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        // NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnPlayerFinishedTalking()
    {
        // Debug.Log("NPC is walking");
        Activated = true;
        IsWalking = true; 
        IsIdle = false;
    }

    private void OnActivatedAction()
    {
        Activated = true;
    }

    private void OnPlayerWalking()
    {
        // Debug.Log("NPC is Idle");
        IsIdle = true;
        IsWalking = false;
    }
    
    private void Start()
    {
        State = new NPCWalkState(this);
    }

    private void Update()
    {
        State.Update();
    }
}
