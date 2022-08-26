using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Scripts;
using UnityEngine.EventSystems;

public class CaptainManager : DialogueTrigger
{
   public delegate void CaptainRewardRecieved();
   public static event CaptainRewardRecieved OnCaptainRewardRecieved;
   
   private GameObject CaptainsChange;
   
   private bool convoCompleted;
   
   private bool isWaitingToTalk;

   private GameObject player;
   private Collider collider;
   private Vector3 playerPos;
   
   private void Awake()
   {
      collider = GetComponent<Collider>();
      player = GameObject.FindGameObjectWithTag("Player");
      CaptainsChange = GameObject.FindGameObjectWithTag("captainChange");
   }
   
   private void Update()
   {
      if (isWaitingToTalk)
      {
         if (collider.bounds.Contains(playerPos))
         {
            if (convoCompleted == false)
            {
               Trigger("CaptainConvo");
               isWaitingToTalk = false;
            }
            else
            {
               if (ObjectivesManager._instance.CaptainQuestCompleted && !ObjectivesManager._instance.CaptainRewardRecieved)
               {
                  Trigger("CaptainQuestCompletedConvo");
                  Inventory._instance.Add("change", CaptainsChange);
                  OnCaptainRewardRecieved?.Invoke();
                  isWaitingToTalk = false;
                  return;
               }
               
               if (!ObjectivesManager._instance.CaptainQuestAccepted)
               {
                  Trigger("CaptainGiveQuestConvo");
                  isWaitingToTalk = false;
                  return;
               }
               
               Trigger("CaptainIdleLine");
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
               Trigger("CaptainConvo");
               isWaitingToTalk = false;
            }
            else
            {
               if (ObjectivesManager._instance.CaptainQuestCompleted && !ObjectivesManager._instance.CaptainRewardRecieved)
               {
                  Trigger("CaptainQuestCompletedConvo");
                  Inventory._instance.Add("captainChange", CaptainsChange);
                  OnCaptainRewardRecieved?.Invoke();
                  isWaitingToTalk = false;
                  return;
               }

               if (!ObjectivesManager._instance.CaptainQuestAccepted)
               {
                  Trigger("CaptainGiveQuestConvo");
                  isWaitingToTalk = false;
                  return;
               }
               
               Trigger("CaptainIdleLine");
               isWaitingToTalk = false;
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
      DialogueManager.OnCaptainDialogueEnded += OnCaptainDialogueEnded;
      Interactable.OnTalkToAction += OnTalkToAction;
   }
   
   private void OnDisable()
   {
      DialogueManager.OnCaptainDialogueEnded -= OnCaptainDialogueEnded;
      Interactable.OnTalkToAction += OnTalkToAction;
   }

   private void OnTalkToAction(string target)
   {
      if (target == "captain")
      {
         playerPos = player.transform.position;
         isWaitingToTalk = true;
      }
   }
   
   private void OnCaptainDialogueEnded()
   {
      convoCompleted = true;
   }
}
