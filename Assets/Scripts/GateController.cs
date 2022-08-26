using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    private float gateMoveDelay = 0.1f;
    
    private void OnEnable()
    {
        DialogueManager.OnHumphrey1DialogueEnded += OnHumphrey1DialogueEnded;
    }

    private void OnDisable()
    {
        DialogueManager.OnHumphrey1DialogueEnded -= OnHumphrey1DialogueEnded;
    }

    private void OnHumphrey1DialogueEnded()
    {
        StartCoroutine(LowerGateCoroutine());
    }
    
    IEnumerator LowerGateCoroutine()
    {
        while (gate.transform.position.y > -11)
        {
            gate.transform.Translate(Vector3.down);
            yield return new WaitForSeconds(gateMoveDelay);
        }
    }
}
