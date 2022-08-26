using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class UnlikeableNPCManager : AbstractNPC
{
    private Transform pos1;
    private Transform pos2;
    private Transform pos3;

    private NavMeshAgent agent;
    private Transform currentPoint;
    private Collider collider;

    private bool playerIsWalking;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        collider = GetComponent<Collider>();

        pos1 = NPCFactory._instance.uPos1;
        pos2 = NPCFactory._instance.uPos2;
        pos3 = NPCFactory._instance.uPos3;
        
        transform.position = pos1.transform.position;
        ChooseDestination();
    }

    protected override void Update()
    {
        if (playerIsWalking)
        {
            return;
        }
        
        Move();
    }

    private void OnEnable()
    {
        Unlikeable1Manager.OnPlayerWalking += OnPlayerWalking;
        DialogueManager.OnFinishedSpeakingWithUnlikeable += OnFinishedSpeakingWithUnlikeable;
    }

    private void OnDisable()
    {
        Unlikeable1Manager.OnPlayerWalking -= OnPlayerWalking;
        DialogueManager.OnFinishedSpeakingWithUnlikeable -= OnFinishedSpeakingWithUnlikeable;
    }

    private void OnFinishedSpeakingWithUnlikeable()
    {
        playerIsWalking = false;
    }

    private void OnPlayerWalking()
    {
        playerIsWalking = true;
    }

    protected override void Move()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            ChooseDestination();
        }
    }
    
    private void LoopMoving()
    {
        if (transform.position == currentPoint.position)
        {
            ChooseDestination();
        } 
    }
    
    private void ChooseDestination()
    {
        Random rand = new Random();
        
        int num = rand.Next(1, 4);

        if (num == 1)
        {
            currentPoint = pos1;
            agent.destination = pos1.position;
        }
        
        if (num == 2)
        {
            currentPoint = pos2;
            agent.destination = pos2.position;
        }
        
        if(num == 3)
        {
            currentPoint = pos3;
            agent.destination = pos3.position;
        }
    }

}
