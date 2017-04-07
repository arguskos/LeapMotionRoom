using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoButtons : MonoBehaviour {

    new SerialPort Arduino1;
    new SerialPort Arduino2;

    public string comm;
    public bool started;

    public bool btn0KeyDown = false;
    public bool btn1KeyDown = false;
    public bool btn2KeyDown = false;
    public bool btn3KeyDown = false;
    public bool btn4KeyDown = false;
    public bool btn5KeyDown = false;

    public bool btn0KeyUp = false;
    public bool btn1KeyUp = false;
    public bool btn2KeyUp = false;
    public bool btn3KeyUp = false;
    public bool btn4KeyUp = false;
    public bool btn5KeyUp = false;

    public bool btn0Pressed = false;
    public bool btn1Pressed = false;
    public bool btn2Pressed = false;
    public bool btn3Pressed = false;
    public bool btn4Pressed = false;
    public bool btn5Pressed = false;



    public string openMssg;
    public string btnMssg;



	// Use this for initialization
	void Start () {
        Arduino1 = new SerialPort(comm, 115200);
        Arduino1.ReadTimeout = 25;
        Arduino1.Open();
        Arduino1.Write("a");

        //Arduino2 = new SerialPort("COM4", 115200);
        //Arduino2.ReadTimeout = 25;
        //Arduino2.Open();
        //Arduino2.Write("a");
		
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

    }
    public void checkBtnPress(int i) 
    {
        switch (i)
        {
            case 0:
                if (btn0KeyDown == true)
                {
                    btn0KeyDown = false;
                    btn0Pressed = true;
                }
                else if (btn0Pressed == false)
                {
                    btn0KeyDown = true;      
                }
                break;
            case 1:
                if (btn1KeyDown == true)
                {
                    btn1KeyDown = false;
                    btn1Pressed = true;
                }
                else if (btn1Pressed == false)
                {
                    btn1KeyDown = true;      
                }
                break;
            case 2:
                if (btn2KeyDown == true)
                {
                    btn2KeyDown = false;
                    btn2Pressed = true;
                }
                else if (btn2Pressed == false)
                {
                    btn2KeyDown = true;      
                }
                break;
            case 3:
                if (btn3KeyDown == true)
                {
                    btn3KeyDown = false;
                    btn3Pressed = true;
                }
                else if (btn3Pressed == false)
                {
                    btn3KeyDown = true;      
                }
                break;
            case 4:
                if (btn4KeyDown == true)
                {
                    btn4KeyDown = false;
                    btn4Pressed = true;
                }
                else if (btn4Pressed == false)
                {
                    btn4KeyDown = true;      
                }
                break;
            case 5:
                if (btn5KeyDown == true)
                {
                    btn5KeyDown = false;
                    btn5Pressed = true;
                }
                else if (btn5Pressed == false)
                {
                    btn5KeyDown = true;      
                }
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
                if (btn0Pressed == true)
                {
                    btn0Pressed = false;
                    btn0KeyUp = true;

                }
                else
                {
                    btn0KeyUp = false;
                }
                break;
            case 1:
                if (btn1Pressed == true)
                {
                    btn1Pressed = false;
                    btn1KeyUp = true;

                }
                else
                {
                    btn1KeyUp = false;
                }
                break;
            case 2:
                if (btn2Pressed == true)
                {
                    btn2Pressed = false;
                    btn2KeyUp = true;

                }
                else
                {
                    btn2KeyUp = false;
                }
                break;
            case 3:
                if (btn3Pressed == true)
                {
                    btn3Pressed = false;
                    btn3KeyUp = true;

                }
                else
                {
                    btn3KeyUp = false;
                }
                break;
            case 4:
                if (btn4Pressed == true)
                {
                    btn4Pressed = false;
                    btn4KeyUp = true;

                }
                else
                {
                    btn4KeyUp = false;
                }
                break;
            case 5:
                if (btn5Pressed == true)
                {
                    btn5Pressed = false;
                    btn5KeyUp = true;

                }
                else
                {
                    btn5KeyUp = false;
                }
                break;
            default:
                break;
        }
    }
}
