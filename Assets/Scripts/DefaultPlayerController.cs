using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPlayerController : MonoBehaviour
{
    private const float speed = 10f;
    private void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            MoveForward();
        }
    
        if (Input.GetKey(KeyCode.S))
        {
            MoveBack();
        }
    
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
    
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
    }
    
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void MoveBack()
    {
        transform.Translate(Vector3.back * (speed * Time.deltaTime));
    }
    
    private void MoveRight()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }
    
    private void MoveLeft()
    {
        transform.Translate(Vector3.left * (speed * Time.deltaTime));
    }
    
}
