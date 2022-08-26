using System;
using System.Collections;
using System.Collections.Generic;
using Prototype.Scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerController : PlayerStateMachine
{
    public delegate void ActivatedAction();
    public static event ActivatedAction OnActivatedAction;
    
    [SerializeField] private TMP_InputField commandInput;
    
    private const float speed = 10f;
    private bool isTalking;
    
    [SerializeField] private LayerMask _layerMask;

    private Vector3 targetPosition;

    private bool waitingToTalk;
    
    // private bool waitingToTalk;
    
    private void OnEnable()
    {
        DialogueTrigger.OnIsTalking += OnIsTalking;
        DialogueManager.OnDialogueFinished += OnDialogueFinished;
        Interactable.OnWalkToAction += OnWalkToAction;
        Interactable.OnTalkToAction += OnTalkToAction;
        PlayerWalkState.OnDestinationReached += OnDestinationReached;
        // DialogueManager.OnDialogueFinished += 
    }
    
    private void OnDisable()
    {
        DialogueTrigger.OnIsTalking -= OnIsTalking;
        DialogueManager.OnDialogueFinished -= OnDialogueFinished;
        Interactable.OnWalkToAction -= OnWalkToAction;
        Interactable.OnTalkToAction -= OnTalkToAction;
        PlayerWalkState.OnDestinationReached -= OnDestinationReached;
    }

    private void OnDestinationReached()
    {
        IsWalking = false;

        if (waitingToTalk)
        {
            IsTalking = true;
            waitingToTalk = false;
        }
    }
    
    private void OnIsTalking()
    {
        IsIdle = false;
        IsWalking = false;
        IsTalking = true;
    }

    private void OnTalkToAction(string target)
    {
        waitingToTalk = true;

        // if (target == "guard")
        // {
        //     Debug.Log(target);
        //     target = "humphrey";
        // }
        
        GameObject Target = GameObject.FindGameObjectWithTag(target);
        
        float distance = Vector3.Distance (this.transform.position, Target.transform.position);

        if (distance > 4)
        {
            OnWalkToAction(target);
        }
        // Debug.Log("Distance is: " + distance);
    }
    
    private void OnWalkToAction(string target)
    {
        // if (target == "guard")
        // {
        //     Debug.Log(target);
        //     target = "humphrey";
        // }
        // targetPosition = GameObject.FindGameObjectWithTag(target).transform.position;
        // Debug.Log(targetPosition);
        // NavMeshAgent.destination = targetPosition;
        Target = target;
        IsIdle = false;
        IsTalking = false;
        IsWalking = true;
    }
    
    private void OnDialogueFinished()
    {
        IsTalking = false;
        IsIdle = true;
        IsWalking = false;
    }
    
    private void Awake()
    {
        commandInput.onEndEdit.AddListener(Activate);
        LayerMask = _layerMask;
        Animator = GetComponentInChildren<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Activate(string text)
    {
        OnActivatedAction?.Invoke();
        Activated = true;
    }

    private void Start()
    {
        State = new PlayerIdleState(this);
    }
    
    void Update()
    {
        State.Update();

        // if (transform.position == targetPosition)
        // {
        //     IsWalking = false;
        //     targetPosition = new Vector3();
        //     State = new PlayerIdleState(this);
        // }
    }
}
