using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour {

    public bool IsEnabled;
    public float LifeTime;
    private float _duration;
    private float _blinkSpeed = 18.0f;
    private bool _blink;
        
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
            if (_blink)
            {
                if (Mathf.Sin(LifeTime * _blinkSpeed) <= 0)
                {
                    this.GetComponent<Text>().enabled = false;
                }
                else
                {
                    this.GetComponent<Text>().enabled = true;
                }
            }
            else
            {
                this.GetComponent<Text>().enabled = true;
            }
        }

        if (LifeTime >= _duration)
        {
            IsEnabled = false;
            this.GetComponent<Text>().enabled = false;
        }
    }

    //Makes the textcomponent on the object blink/show
    public void TriggerText(string uitext, float duration, bool isblinking)
    {
        IsEnabled = true;
        _duration = duration;
        LifeTime = 0.0f;
        this.GetComponent<Text>().text = uitext;
        this.GetComponent<Text>().enabled = true;
        _blink = isblinking;
    }

}
