using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRotator : MonoBehaviour
{

    [SerializeField] private float speed = 20;
    private void Update()
    {
        transform.Rotate(new Vector3(0,0,10) *Time.deltaTime * speed);
    }
}
