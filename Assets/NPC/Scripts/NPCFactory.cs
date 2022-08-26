using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFactory : MonoBehaviour
{
    public static NPCFactory _instance { get; private set; }

    [SerializeField] private UnlikeableNPCManager unlikeablePrefab;
    [SerializeField] private CitizenNPCManager citizenPrefab;

    [SerializeField] public Transform cPos1;
    [SerializeField] public Transform cPos2;
    [SerializeField] public Transform cPos3;

    [SerializeField] public Transform uPos1;
    [SerializeField] public Transform uPos2;
    [SerializeField] public Transform uPos3;
    
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public AbstractNPC Produce(NPCType type)
    {
        if (type == NPCType.CITIZEN)
        {
            return Instantiate(citizenPrefab.gameObject).GetComponent<CitizenNPCManager>();
        }
        else if (type == NPCType.UNLIKEABLE)
        {
            return Instantiate(unlikeablePrefab.gameObject).GetComponent<UnlikeableNPCManager>();
        }

        return null;
    }
}

public enum NPCType
{
    UNLIKEABLE,
    CITIZEN
}

