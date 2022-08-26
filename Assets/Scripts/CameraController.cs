using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    
    void Update()
    {
        var position = target.transform.position;
        transform.position = new Vector3(position.x, position.y + 15, position.z - 6);
    }
}
