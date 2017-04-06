﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Element : MonoBehaviour
{
    private Vector3 Position;
    public bool IsOn { get; set;}
    private float _delay;
    private Color _color;
    private Color _originalcolor;
    private Vector3 _scale;
    public int Id { get; private set; }
    public bool IsPreview;
    public Material Mat;    
    public void OnCompleteOn()
    {
        IsOn = true;
    }
    public void OnCompleteOff()
    {
        IsOn = false;
    }

    //On behaviour
    public void DeffaultOn()
    {
        var scalemultiplier = 1.5f;
        var newscale = new Vector3(_scale.x *scalemultiplier, _scale.y * scalemultiplier, _scale.z+0.1f );
        iTween.ColorTo(gameObject, _color,_delay);
        iTween.ScaleTo(gameObject, newscale, 1);

    }

    //Off behaviour
    public void DeffaultOff()
    {
        iTween.ColorTo(gameObject, _originalcolor, _delay/4 );
        iTween.ScaleTo(gameObject, _scale, 1);
    }

    public void Init(int id )
    {
        IsPreview = false;
        IsOn = false;
        Id = id;
        _originalcolor = GetComponent<Renderer>().material.color;
        _delay = 0.1f + (Id * 0.1f);
        _scale = transform.localScale;
    }
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //Setup difference between general elements and the preview selection element
        if (IsPreview == true)
        {
            _color = _originalcolor * 0.0f;
            _delay = 1.0f;
        }
        else
        {
            _color = _originalcolor * 0.5f;
        }
    }
}
