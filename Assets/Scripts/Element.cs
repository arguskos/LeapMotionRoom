using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Element : MonoBehaviour
{
    private Vector3 Position;
    public bool IsOn { get; set;}
    private float _delay;
    private Color _color;
    private Vector3 _scale;
    public int Id { get; private set; }
    public bool IsLoading = true;
    private float _loadmultiplier;

    public void OnCompleteOn()
    {
        IsOn = true;
    }
    public void OnCompleteOff()
    {
        IsOn = false;
    }

    public void DeffaultOn()
    {
        var scalemultiplier = 2.0f;
        var newscale = new Vector3(_scale.x * scalemultiplier, _scale.y * scalemultiplier, _scale.z);
        var colormultiplier = 0.5f;
        iTween.ColorTo(gameObject, colormultiplier*_color,_delay/4);
        iTween.ScaleTo(gameObject, newscale, _delay);

    }

    public void DeffaultOff()
    {
        iTween.ColorTo(gameObject, _color, _delay/4 );
        iTween.ScaleTo(gameObject, _scale, _delay);
    }

    public void Init(int id )
    {
        IsOn = false;
        Id = id;
        _color = GetComponent<Renderer>().material.color;
        _delay = 0.25f;
        _scale = transform.localScale;
    }
    // Use this for initialization
    void Start()
    {
     

    }

    // Update is called once per frame
    void Update()
    {

    }
}
