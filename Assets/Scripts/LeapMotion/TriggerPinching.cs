using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPinching : MonoBehaviour
{

    // Use this for initialization
    public bool IsInTrigger = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickable")
        {
            Debug.Log("pinching");
            IsInTrigger = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pickable")
        {
            Debug.Log("pinching");
            IsInTrigger = false;
        }
    }
}
