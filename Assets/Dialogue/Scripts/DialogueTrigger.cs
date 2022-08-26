using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueTrigger : MonoBehaviour
{
    public delegate void TriggerDialogueAction(string id);
    public static event TriggerDialogueAction OnDialogueTriggered;

    public delegate void EndDialogueAction();
    public static event EndDialogueAction OnDialogueEnded;

    public delegate void IsTalking();
    public static event IsTalking OnIsTalking;

    public void Trigger(string dialogueId)
    {
        OnDialogueTriggered?.Invoke(dialogueId);
        OnIsTalking?.Invoke();
    }

    public void EndConvo()
    {
        OnDialogueEnded?.Invoke();
    }
}