using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFace : MonoBehaviour
{



    // Use this for initialization
    private int _numberOfElements;
    public List<Element> ElementObjects = new List<Element>();
    public bool IsActivated { get; private set; }
    public void ActivatePiece (int numbe)
    {
        ElementObjects[numbe].IsOn = true;
        ElementObjects[numbe].DeffaultOn();
        IsActivated = true; 
    }

    public void DeactivatePiece (int number)
    {
        
        ElementObjects[number].IsOn = false;
        ElementObjects[number].DeffaultOff();
        foreach (var elem in ElementObjects)
        {
            if (elem.IsOn)
            {
                return;
            }
        }
        IsActivated = false;
    }

    public void TurnOffAll()
    {
        for (int i = 0; i < ElementObjects.Count; i++)
        {
            IsActivated = false;
            ElementObjects[i].IsOn = false;
            ElementObjects[i].DeffaultOff();
        }
    }
    public void TurnOnAll()
    {
        Debug.Log(ElementObjects.Count);
        for (int i = 0; i < ElementObjects.Count; i++)
        {
            IsActivated = true;
            ElementObjects[i].IsOn = true;
            ElementObjects[i].DeffaultOn();
        }
    }
    void Start()
    {

       Init();
    }

    public void Init()
    {
        foreach (Transform child in transform)
        {

            ElementObjects.Add(child.gameObject.GetComponent<Element>());
        }
        _numberOfElements = ElementObjects.Count;
        for (int i = 0; i < _numberOfElements; i++)
        {
            ElementObjects[i].Init(i);
        }
    }


    public void SetIsPreview(bool ispreview)
    {
        for (int i = 0; i < _numberOfElements; i++)
        {
            ElementObjects[i].IsPreview = ispreview;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
