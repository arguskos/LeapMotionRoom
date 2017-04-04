using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBtnTest : MonoBehaviour
{
    public ArduinoButtons testje;
	// Use this for initialization
	void Start () {
        //testje = (ArduinoButtons)testje.GetComponent(typeof(ArduinoButtons));
	}
	
	// Update is called once per frame
	void Update () {
        if (testje.btn0Pressed)
        {
            Debug.Log(testje.btn0Pressed);
        }
	}
}
