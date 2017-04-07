using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour {

    public bool IsEnabled;
    public float LifeTime;
    private float _duration;
        
    // Use this for initialization
	void Start ()
    {
        LifeTime = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (IsEnabled)
        {
            LifeTime += Time.deltaTime;
        }

        if (LifeTime >= _duration)
        {
            IsEnabled = false;
            this.GetComponent<Text>().enabled = false;
        }
    }

    public void TriggerText(string uitext, float duration)
    {
        IsEnabled = true;
        _duration = duration;
        LifeTime = 0.0f;
        this.GetComponent<Text>().text = uitext;
        this.GetComponent<Text>().enabled = true;
    }

}
