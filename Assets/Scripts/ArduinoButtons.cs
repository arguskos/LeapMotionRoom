using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoButtons : MonoBehaviour {

    new SerialPort Arduino1;
    new SerialPort Arduino2;

    public string comm;
    public bool started;
    private KeyCode _pressedButton;
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

    public GameFlow GF;

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
	public bool GetButton(KeyCode key)

    {
        Debug.Log(_pressedButton);
        if (_pressedButton==key)
        {
            return true;
        }
        return false;
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
                    GF.AddToArray(0);
                    GF.Check(KeyCode.Q);
                    _pressedButton = KeyCode.Q;
                    Debug.Log("sdsad");
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
                    GF.AddToArray(1);
                    GF.Check(KeyCode.W);
                    _pressedButton = KeyCode.W;

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
                    GF.AddToArray(2);
                    GF.Check(KeyCode.E);
                    _pressedButton = KeyCode.E;

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
                    GF.AddToArray(3);
                    GF.Check(KeyCode.R);
                    _pressedButton = KeyCode.R;

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
                    GF.AddToArray(4);
                    GF.Check(KeyCode.T);
                    _pressedButton = KeyCode.T;

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
                    GF.AddToArray(5);
                    GF.Check(KeyCode.Y);
                    _pressedButton = KeyCode.Y;

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
                    GF.RemoveFromArray(0);
                    GF.Check(KeyCode.Q);
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
                    GF.RemoveFromArray(1);
                    GF.Check(KeyCode.W);
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
                    GF.RemoveFromArray(2);
                    GF.Check(KeyCode.E);
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
                    GF.RemoveFromArray(3);
                    GF.Check(KeyCode.R);
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
                    GF.RemoveFromArray(4);
                    GF.Check(KeyCode.T);
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
                    GF.RemoveFromArray(5);
                    GF.Check(KeyCode.Y);
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
