﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFace : MonoBehaviour
{



    // Use this for initialization
    private int _numberOfElements;
    private List<Element> ElementObjects = new List<Element>();
    public bool IsActivated { get; private set; }
    public void ActivatePiece (int numbe)
    {
        ElementObjects[numbe].IsOn = true;
        ElementObjects[numbe].DeffaultOn();
        IsActivated = true;
    }
    void Start()
    {

        foreach (Transform child in transform)
        {

            ElementObjects.Add(child.gameObject.GetComponent < Element>());
        }
        _numberOfElements = ElementObjects.Count;
        for (int i = 0; i < ElementObjects.Count; i++)
        {
            ElementObjects[i].Init(i);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
