using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractNPC : MonoBehaviour
{
    [SerializeField] protected float speed = 2f;

    protected bool PlayerIsWalking;
    //
    // [SerializeField] protected Transform CitizenPos1;
    // [SerializeField] protected Transform CitizenPos2;
    // [SerializeField] protected Transform CitizenPos3;
    // [SerializeField] protected Transform UnlikeablePos1;
    // [SerializeField] protected Transform UnlikeablePos2;
    // [SerializeField] protected Transform UnlikeablePos3;
    //
    // protected void Update()
    // {
    //     // while (!PlayerIsWalking)
    //     // {
    //     if (PlayerIsWalking)
    //     {
    //         return;
    //     }
    //     
    //     Move();
    //     // }
    // }

    protected abstract void Update();

    protected abstract void Move();
}

