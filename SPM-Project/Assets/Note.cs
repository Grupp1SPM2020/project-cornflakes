﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private bool logThis;
    [SerializeField] [TextArea] private string message;

    private void Start()
    {
        if (logThis) 
        {
            string header = "[Remove the Note script from '" + gameObject.name + "' when this message has been read]\n";
            Debug.Log(header + message);
        }
    }
}
