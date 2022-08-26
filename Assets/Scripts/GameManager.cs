using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(NPCFactory))]
public class GameManager : MonoBehaviour
{
    private NPCFactory factory;
    
    void Start()
    {
        factory = GetComponent<NPCFactory>();

        CitizenNPCManager citizen = (CitizenNPCManager)factory.Produce(NPCType.CITIZEN);
        UnlikeableNPCManager unlikeable = (UnlikeableNPCManager)factory.Produce(NPCType.UNLIKEABLE);

        citizen.transform.position = new Vector3(-60, 1, 5);
        unlikeable.transform.position = new Vector3(10, 1, 20);
    }
}
