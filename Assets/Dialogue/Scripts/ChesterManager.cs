using System.Collections;
using System.Collections.Generic;
using Prototype.Scripts;
using UnityEngine;
using Random = System.Random;

public class ChesterManager : DialogueTrigger
{
    private bool convoCompleted;
    private bool isWaitingToTalk;

    private GameObject player;
    private Collider collider;
    private Vector3 playerPos;

    private Random rand = new Random();
    private void Awake()
    {
        collider = GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        if (isWaitingToTalk)
        {
            if (collider.bounds.Contains(playerPos))
            {
                if (ObjectivesManager._instance.CaptainQuestAccepted == false)
                {
                    int _random = rand.Next(0, 3);
                    if (_random == 0)
                    {
                        Trigger("ChesterIdleConvo1");
                    }
                    else if (_random == 1)
                    {
                        Trigger("ChesterIdleConvo2");
                    }
                    else if (_random == 2)
                    {
                        Trigger("ChesterIdleConvo3");
                    }
                    isWaitingToTalk = false;
                }
                else
                {
                    if (!convoCompleted)
                    {
                        Trigger("ChesterConvo");
                        isWaitingToTalk = false;
                        return;
                    }
                    
                    if (ObjectivesManager._instance.ChesterQuestAccepted &&
                        !ObjectivesManager._instance.ChesterQuestCompleted)
                    {
                        Trigger("ChesterWaitingForQuest");
                        isWaitingToTalk = false;
                        return;
                    }
                    
                    if (ObjectivesManager._instance.CaptainQuestCompleted)
                    {
                        Trigger("ChesterHostileLine");
                        isWaitingToTalk = false;
                        return;
                    }

                    if (convoCompleted && !ObjectivesManager._instance.ChesterQuestAccepted &&
                        !ObjectivesManager._instance.CaptainQuestCompleted)
                    {
                        Trigger("ChesterGiveQuestConvo");
                        isWaitingToTalk = false;
                        return;
                    }

                    if (ObjectivesManager._instance.ChesterQuestFailed)
                    {
                        Trigger("ChesterQuestFailedConvo");
                        isWaitingToTalk = false;
                        return;
                    }

                    if (ObjectivesManager._instance.ChesterQuestCompleted 
                        && !ObjectivesManager._instance.GaveChangeToChester 
                        && !ObjectivesManager._instance.KeptChangeChester)
                    {
                        Trigger("ChesterQuestedCompleted");
                        isWaitingToTalk = false;
                        return;
                    }

                    if (ObjectivesManager._instance.GaveChangeToChester)
                    {
                        Trigger("ChesterHappyLine");
                        isWaitingToTalk = false;
                        return;
                    }

                    if (ObjectivesManager._instance.KeptChangeChester)
                    {
                        Trigger("ChesterAngryLine");
                        isWaitingToTalk = false;
                        return;
                    }
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
                if (ObjectivesManager._instance.CaptainQuestAccepted == false)
                {
                    int _random = rand.Next(0, 3);
                    if (_random == 0)
                    {
                        Trigger("ChesterIdleConvo1");
                    }
                    else if (_random == 1)
                    {
                        Trigger("ChesterIdleConvo2");
                    }
                    else if (_random == 2)
                    {
                        Trigger("ChesterIdleConvo3");
                    }
                    isWaitingToTalk = false;
                }
                else
                {
                    if (!convoCompleted)
                    {
                        Trigger("ChesterConvo");
                        isWaitingToTalk = false;
                        return;
                    }
                    
                    if (ObjectivesManager._instance.ChesterQuestAccepted &&
                        !ObjectivesManager._instance.ChesterQuestCompleted)
                    {
                        Trigger("ChesterWaitingForQuest");
                        isWaitingToTalk = false;
                        return;
                    }

                    if (ObjectivesManager._instance.CaptainQuestCompleted)
                    {
                        Trigger("ChesterHostileLine");
                        isWaitingToTalk = false;
                        return;
                    }

                    if (convoCompleted && !ObjectivesManager._instance.ChesterQuestAccepted &&
                        !ObjectivesManager._instance.CaptainQuestCompleted)
                    {
                        Trigger("ChesterGiveQuestConvo");
                        isWaitingToTalk = false;
                        return;
                    }

                    if (ObjectivesManager._instance.ChesterQuestFailed)
                    {
                        Trigger("ChesterQuestFailedConvo");
                        isWaitingToTalk = false;
                        return;
                    }

                    if (ObjectivesManager._instance.ChesterQuestCompleted 
                        && !ObjectivesManager._instance.GaveChangeToChester 
                        && !ObjectivesManager._instance.KeptChangeChester)
                    {
                        Trigger("ChesterQuestedCompleted");
                        isWaitingToTalk = false;
                        return;
                    }

                    if (ObjectivesManager._instance.ChesterQuestCompleted && ObjectivesManager._instance.GaveChangeToChester)
                    {
                        Trigger("ChesterHappyLine");
                        isWaitingToTalk = false;
                        return;
                    }

                    if (ObjectivesManager._instance.ChesterQuestCompleted && ObjectivesManager._instance.KeptChangeChester)
                    {
                        Trigger("ChesterAngryLine");
                        isWaitingToTalk = false;
                        return;
                    }
                    
                    
                }
            }
        }
    }
    
    private void OnEnable()
    {
        Interactable.OnTalkToAction += OnTalkToAction;
        DialogueManager.OnChesterDialogueEnded += OnChesterDialogueEnded;
    }
    
    private void OnDisable()
    {
        Interactable.OnTalkToAction -= OnTalkToAction;
        DialogueManager.OnChesterDialogueEnded -= OnChesterDialogueEnded;
    }
    
    private void OnChesterDialogueEnded()
    {
        convoCompleted = true;
    }
    
    private void OnTalkToAction(string target)
    {
        if (target == "chester")
        {
            playerPos = player.transform.position;
            isWaitingToTalk = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        EndConvo();
    }
}
