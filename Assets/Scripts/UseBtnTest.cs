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
        if (testje.btn0KeyDown)
        {
            Debug.Log("keydown" + testje.btn0KeyDown);
        }
        if (testje.btn0KeyUp)
        {
            Debug.Log("keyup"+testje.btn0KeyUp);
        }
        if (testje.btn0Pressed)
        {
            Debug.Log("keypressed" + testje.btn0Pressed);
        }
        if (testje.btn1KeyDown)
        {
            Debug.Log("keydown" + testje.btn1KeyDown);
        }
        if (testje.btn1KeyUp)
        {
            Debug.Log("keyup" + testje.btn1KeyUp);
        }
        if (testje.btn1Pressed)
        {
            Debug.Log("keypressed" + testje.btn1Pressed);
        }
	}
}
