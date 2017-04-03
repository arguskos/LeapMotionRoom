using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Element : MonoBehaviour
{



    private Vector3 Position;
    public bool IsOn { get; set;}
    public int Id { get; private set; }
    public void OnCompleteOn()
    {
        IsOn = true;
    }
    public void OnCompleteOff()
    {
        IsOn = false;
    }
    public void DeffaultOn()
    {
       iTween.ColorTo(gameObject, iTween.Hash("r", 1f, "g", 0f, "b", 0f, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0, "oncomplete", "OnCompleteOn", "oncompletetarget", gameObject));


    }

    public void DeffaultOff()
    {
        iTween.MoveBy(gameObject, iTween.Hash("x", -0.01f, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0, "oncomplete", "OnCompleteOff", "oncompletetarget", gameObject));


    }

    public void Init(int id )
    {
        IsOn = false;
        Id = id;
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
