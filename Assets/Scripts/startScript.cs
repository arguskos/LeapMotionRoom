using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.SceneManagement;

public class startScript : MonoBehaviour {

    new SerialPort Ardu;

    public string com;
    private string startMssg;

	// Use this for initialization
	void Start () {
        Ardu = new SerialPort(com, 115200);
        Ardu.ReadTimeout = 25;
        Ardu.Open();
        Ardu.Write("a");
	}
	
	// Update is called once per frame
	void Update () {
        startMssg = Ardu.ReadLine();
        Debug.Log(startMssg);
        if (startMssg == "1")
        {
            //start the overall game
            SceneManager.LoadScene(0);
        }
	}
}
