using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEnterTrigger : DialogueTrigger
{
    [SerializeField] private string dialogueSetID;
    
    private void OnTriggerEnter(Collider other)
    {
        Trigger(dialogueSetID);
    }

    private void OnTriggerExit(Collider other)
    {
        EndConvo();
    }
}


