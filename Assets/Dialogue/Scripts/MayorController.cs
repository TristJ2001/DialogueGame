using System;
using System.Collections;
using System.Collections.Generic;
using Prototype.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class MayorController : DialogueTrigger
{
    private GameObject change1;
    private GameObject change2;
    private GameObject change3;
    
    private bool convoCompleted;
    private bool isWaitingToTalk;

    private GameObject player;
    private Collider collider;
    private Vector3 playerPos;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        change1 = GameObject.FindGameObjectWithTag("change1");
        change2 = GameObject.FindGameObjectWithTag("change2");
        change3 = GameObject.FindGameObjectWithTag("change3");
    }
    
    private void Update()
    {
        if (isWaitingToTalk)
        {
            if (collider.bounds.Contains(playerPos))
            {
                if (ObjectivesManager._instance.ChesterQuestAccepted &&
                    !ObjectivesManager._instance.ChesterQuestCompleted &&
                    !ObjectivesManager._instance.ChesterQuestFailed)
                {
                    Trigger("MayorConvo1");
                    isWaitingToTalk = false;
                    return;
                }
                
                if (ObjectivesManager._instance.ChesterQuestCompleted)
                {
                    Trigger("MayorHappyConvo");
                    isWaitingToTalk = false;
                    return;
                }

                if (ObjectivesManager._instance.ChesterQuestFailed)
                {
                    Trigger("MayorAngryConvo");
                    isWaitingToTalk = false;
                    return;
                }
                
                Trigger("MayorIdleConvo");
                isWaitingToTalk = false;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (isWaitingToTalk)
        {
            if (ObjectivesManager._instance.ChesterQuestAccepted &&
                !ObjectivesManager._instance.ChesterQuestCompleted &&
                !ObjectivesManager._instance.ChesterQuestFailed)
            {
                Trigger("MayorConvo1");
                isWaitingToTalk = false;
                return;
            }
            
            if (ObjectivesManager._instance.ChesterQuestCompleted)
            {
                Trigger("MayorHappyConvo");
                isWaitingToTalk = false;
                return;
            }

            if (ObjectivesManager._instance.ChesterQuestFailed)
            {
                Trigger("MayorAngryConvo");
                isWaitingToTalk = false;
                return;
            }
            
            Trigger("MayorIdleConvo");
            isWaitingToTalk = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EndConvo();
    }

    private void OnEnable()
    {
        DialogueManager.OnMayorDialogueEnded += OnMayorDialogueEnded;
        Interactable.OnTalkToAction += OnTalkToAction;
        DialogueManager.OnChesterQuestCompleted += OnChesterQuestCompleted;
    }
    
    private void OnDisable()
    {
        DialogueManager.OnMayorDialogueEnded -= OnMayorDialogueEnded;
        Interactable.OnTalkToAction -= OnTalkToAction;
        DialogueManager.OnChesterQuestCompleted -= OnChesterQuestCompleted;
    }

    private void OnChesterQuestCompleted()
    {
        Inventory._instance.Add("change", change1);
        Inventory._instance.Add("change", change2);
        Inventory._instance.Add("change", change3);
    }
    
    private void OnTalkToAction(string target)
    {
        if (target == "mayor")
        {
            playerPos = player.transform.position;
            isWaitingToTalk = true;
        }
    }

    private void OnMayorDialogueEnded()
    {
        convoCompleted = true;
        // EndConvo();
    }
}
