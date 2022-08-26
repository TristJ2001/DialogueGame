using System;
using System.Collections;
using System.Collections.Generic;
using Prototype.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class GilbertManager : DialogueTrigger
{
    private bool convoCompleted;
    private bool isWaitingToTalk;

    private GameObject player;
    private Collider collider;
    private Vector3 playerPos;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Update()
    {
        if (isWaitingToTalk)
        {
            if (collider.bounds.Contains(playerPos))
            {
                if (convoCompleted == false)
                {
                    Trigger("GilbertConvo1");
                    isWaitingToTalk = false;
                }
                else
                {
                    if (ObjectivesManager._instance.GaveChangeToGilbert)
                    {
                        Trigger("GilbertThankfulConvo");
                        isWaitingToTalk = false;
                        return;
                    }
                    
                    Trigger("GilbertIdleConvo");
                    isWaitingToTalk = false;
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
                if (convoCompleted == false)
                {
                    Trigger("GilbertConvo1");
                    isWaitingToTalk = false;
                }
                else
                {
                    if (ObjectivesManager._instance.GaveChangeToGilbert)
                    {
                        Trigger("GilbertThankfulConvo");
                        isWaitingToTalk = false;
                        return;
                    }
                    
                    Trigger("GilbertIdleConvo");
                    isWaitingToTalk = false;
                }
            }
        }
    }

    private void OnEnable()
    {
        // ContainerInteractable.OnGaveChangeToGilbertAction += OnGaveChangeToGilbertAction;
        Interactable.OnTalkToAction += OnTalkToAction;
        DialogueManager.OnGilbertDialogueEnded += OnGilbertDialogueEnded;
    }
    
    private void OnDisable()
    {
        // ContainerInteractable.OnGaveChangeToGilbertAction -= OnGaveChangeToGilbertAction;
        Interactable.OnTalkToAction -= OnTalkToAction;
        DialogueManager.OnGilbertDialogueEnded -= OnGilbertDialogueEnded;
    }

    private void OnGilbertDialogueEnded()
    {
        convoCompleted = true;
    }
    
    private void OnTalkToAction(string target)
    {
        if (target == "gilbert")
        {
            playerPos = player.transform.position;
            isWaitingToTalk = true;
        }
    }

    // private void OnGaveChangeToGilbertAction()
    // {
    //     Trigger("GilbertThankfulConvo");
    // }
    
    private void OnTriggerExit(Collider other)
    {
        // isWaitingToTalk = false;
        EndConvo();
    }
}
