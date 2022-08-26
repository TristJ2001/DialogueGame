using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeNPCManager : DialogueTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Trigger("PASSWORD_SCENE");
        }
    }
}
