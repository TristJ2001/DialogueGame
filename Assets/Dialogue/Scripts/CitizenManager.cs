using System.Collections;
using System.Collections.Generic;
using Prototype.Scripts;
using UnityEngine;

public class CitizenManager : DialogueTrigger
{
    public delegate void PlayerWalking();
    public static event PlayerWalking OnPlayerWalking;
    
    public delegate void PlayerFinishedTalking();
    public static event PlayerFinishedTalking OnPlayerFinishedTalking;
    
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
                Trigger("CitizenLine");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isWaitingToTalk)
            {
                Trigger("CitizenLine");
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
        Interactable.OnTalkToAction += OnTalkToAction;
        DialogueManager.OnFinishedSpeakingWithCitizen += OnFinishedSpeakingWithCitizen;
    }
    
    private void OnDisable()
    {
        // ContainerInteractable.OnGaveChangeToUnlikeableAction -= OnGaveChangeToUnlikeableAction;
        Interactable.OnTalkToAction -= OnTalkToAction;
        DialogueManager.OnFinishedSpeakingWithCitizen -= OnFinishedSpeakingWithCitizen;
    }

    private void OnFinishedSpeakingWithCitizen()
    {
        isWaitingToTalk = false;
        OnPlayerFinishedTalking?.Invoke();
    }

    private void OnTalkToAction(string target)
    {
        if (target == "citizen")
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
