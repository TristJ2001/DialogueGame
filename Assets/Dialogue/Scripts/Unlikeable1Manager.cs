using System;
using System.Collections;
using System.Collections.Generic;
using Prototype.Scripts;
using UnityEngine;

public class Unlikeable1Manager : DialogueTrigger
{
    public delegate void PlayerWalking();
    public static event PlayerWalking OnPlayerWalking;

    public delegate void PlayerFinishedTalking();
    public static event PlayerFinishedTalking OnPlayerFinishedTalking;
    
    // private bool convoCompleted;
    private bool isWaitingToTalk;

    private GameObject player;
    private Collider collider;
    private Vector3 playerPos;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (isWaitingToTalk)
        {
            if (collider.bounds.Contains(playerPos))
            {
                if (!ObjectivesManager._instance.GaveChangeToUnlikeable)
                {
                    Trigger("UnlikeableConvo");
                }
                else
                {
                    Trigger("UnlikeableThankfulLine");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isWaitingToTalk)
            {
                if (!ObjectivesManager._instance.GaveChangeToUnlikeable)
                {
                    Trigger("UnlikeableConvo");
                }
                else
                {
                    Trigger("UnlikeableThankfulLine");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EndConvo();
    }

    private void OnEnable()
    {
        // ContainerInteractable.OnGaveChangeToUnlikeableAction += OnGaveChangeToUnlikeableAction;
        DialogueManager.OnFinishedSpeakingWithUnlikeable += OnFinishedSpeakingWithUnlikeable;
        Interactable.OnTalkToAction += OnTalkToAction;
    }
    
    private void OnDisable()
    {
        // ContainerInteractable.OnGaveChangeToUnlikeableAction -= OnGaveChangeToUnlikeableAction;
        DialogueManager.OnFinishedSpeakingWithUnlikeable -= OnFinishedSpeakingWithUnlikeable;
        Interactable.OnTalkToAction -= OnTalkToAction;
    }

    private void OnFinishedSpeakingWithUnlikeable()
    {
        isWaitingToTalk = false;
        OnPlayerFinishedTalking?.Invoke();
    }

    private void OnTalkToAction(string target)
    {
        if (target == "unlikeable")
        {
            OnPlayerWalking?.Invoke();
            playerPos = player.transform.position;
            isWaitingToTalk = true;
        }
    }

    // private void OnGaveChangeToUnlikeableAction()
    // {
    //     Trigger("Unlikeable1ThankfulLine");
    //     convoCompleted = true;
    // }
}
