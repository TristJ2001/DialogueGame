using System;
using System.Collections;
using System.Collections.Generic;
using Prototype.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class HumphreyManager : DialogueTrigger
{
    public static HumphreyManager _instance { get; private set; }
    
    private bool convoCompleted;

    // public bool ConvoComlpeted
    // {
    //     get { return convoCompleted; }
    // }
    
    private float moveDelay = 0.1f;
    private float fadeTime = 1f;
    private float speed = 1f;
    private Renderer r;
    private Color colour;

    private bool isWaitingToTalk;

    private Collider collider;

    private GameObject player;

    private Vector3 playerPos;

    private GameObject HumphreyParent;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        
        collider = this.GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player");
        HumphreyParent = GameObject.FindGameObjectWithTag("humphrey");
    }

    private void Update()
    {
        if (isWaitingToTalk)
        {
            if (collider.bounds.Contains(playerPos))
            {
                if (convoCompleted == false)
                {
                    Trigger("HumphreyConvo1");
                    isWaitingToTalk = false;
                }
                else
                {
                    Trigger("HumphreyIdleConvo");
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
                    Trigger("HumphreyConvo1");
                    isWaitingToTalk = false;
                }
                else
                {
                    Trigger("HumphreyIdleConvo");
                    isWaitingToTalk = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // isWaitingToTalk = false;
        EndConvo();
    }

    public bool IsConvoCompleted()
    {
        if (convoCompleted)
        {
            return true;
        }

        return false;
    }

    private void OnEnable()
    {
        DialogueManager.OnHumphrey1DialogueEnded += OnHumphrey1DialogueEnded;
        Interactable.OnTalkToAction += OnTalkToAction;
    }
    
    private void OnDisable()
    {
        DialogueManager.OnHumphrey1DialogueEnded -= OnHumphrey1DialogueEnded;
        Interactable.OnTalkToAction -= OnTalkToAction;
    }

    private void OnTalkToAction(string target)
    {
        if (target == "humphrey")
        {
            playerPos = player.transform.position;
            isWaitingToTalk = true;
        }
    }
    
    private void OnHumphrey1DialogueEnded()
    {
        convoCompleted = true;
        isWaitingToTalk = false;
        StartCoroutine(MoveCoroutine());
    }
    
    IEnumerator MoveCoroutine()
    {
        while (HumphreyParent.transform.position.x < -10)
        {
            HumphreyParent.transform.Translate(Vector3.right);
            yield return new WaitForSeconds(moveDelay);
        }
    }
}
