using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFace : MonoBehaviour
{



    // Use this for initialization
    private int _numberOfElements;
    private List<Element> ElementObjects = new List<Element>();


    void Start()
    {

        foreach (Transform child in transform)
        {

            ElementObjects.Add(child.gameObject.GetComponent < Element>());
        }
        _numberOfElements = ElementObjects.Count;
        for (int i = 0; i < ElementObjects.Count; i++)
        {
            ElementObjects[i].DeffaultOn();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
