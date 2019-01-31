﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void Start()
    {
        transform.position = target.position + offset;
    }

    void Update()
    {
        if(target != null)
            transform.position = target.position + offset;
    }
}
