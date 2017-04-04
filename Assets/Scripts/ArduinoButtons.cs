using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoButtons : MonoBehaviour {

    new SerialPort Arduino1;
    new SerialPort Arduino2;

    public bool started;

    public bool btn0Pressed = false;
    public bool btn1Pressed = false;
    public bool btn2Pressed = false;
    public bool btn3Pressed = false;
    public bool btn4Pressed = false;
    public bool btn5Pressed = false;

    private string openMssg;
    private string btnMssg;



	// Use this for initialization
	void Start () {
        Arduino1 = new SerialPort("COM4", 115200);
        Arduino1.ReadTimeout = 25;
        Arduino1.Open();
        Arduino1.Write("a");

        Arduino2 = new SerialPort("COM5", 115200);
        Arduino2.ReadTimeout = 25;
        Arduino2.Open();
        Arduino2.Write("a");
		
	}
	
	// Update is called once per frame
	void Update () {

        if (started == false)
        {
           openMssg = Arduino2.ReadLine();
           Debug.Log(openMssg);
           if (openMssg == "1")
           {    
               //start the overall game
               started = true;
           }
        }
        else if (started == true)
        {
            Arduino1.Write("a");
            btnMssg = Arduino1.ReadLine();
            if (btnMssg != "")
            {
                for (int i = 0; i < btnMssg.Length; i++)
                {
                    if (btnMssg[i] == '1')
                    {
                        checkBtnPress(i);
                    }
                    else if (btnMssg[i] == '0')
                    {
                        checkBtnRelease(i);
                    }
                }
            }

        }
        //if (btn0Pressed == true)
        //{
        //    Debug.Log("kakakak");
        //}
		
	}
    public void checkBtnPress(int i) 
    {
        switch (i)
        {
            case 0:
                btn0Pressed = true;
                break;
            case 1:
                btn1Pressed = true;
                break;
            case 2:
                btn2Pressed = true;
                break;
            case 3:
                btn3Pressed = true;
                break;
            case 4:
                btn4Pressed = true;
                break;
            case 5:
                btn5Pressed = true;
                break;
            default:
                break;
        }

    }
    public void checkBtnRelease(int i)
    {
        switch (i)
        {
            case 0:
                btn0Pressed = false;
                break;
            case 1:
                btn1Pressed = false;
                break;
            case 2:
                btn2Pressed = false;
                break;
            case 3:
                btn3Pressed = false;
                break;
            case 4:
                btn4Pressed = false;
                break;
            case 5:
                btn5Pressed = false;
                break;
            default:
                break;
        }
    }
}
