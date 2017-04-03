using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Element : MonoBehaviour
{


    public Element(GameObject obj)
    {
        Position = obj.transform.position;
        IsOn = false;
    }
    private Vector3 Position;
    private bool IsOn;

    public void OnCompleteOn()
    {
        IsOn = true;
        Debug.Log("off");
    }
    public void OnCompleteOff()
    {
        IsOn = false;
        Debug.Log("off");
    }
    public void DeffaultOn()
    {
       iTween.MoveBy(gameObject, iTween.Hash("x", -0.01f, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0, "oncomplete", "OnCompleteOn", "oncompletetarget", gameObject));


    }

    public void DeffaultOff()
    {
        iTween.MoveBy(gameObject, iTween.Hash("x", -0.01f, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0, "oncomplete", "OnCompleteOff", "oncompletetarget", gameObject));


    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
