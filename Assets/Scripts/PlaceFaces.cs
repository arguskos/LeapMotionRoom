using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlaceFaces : MonoBehaviour
{
    public PuzzleFace FacePrefab;
	// Use this for initialization
	void Start () {
	   Init();
    }

    public void Init()
    {

        foreach (Transform child in transform)
        {
            var obj = Instantiate(FacePrefab, Vector3.zero, Quaternion.identity);
            // obj.transform.rotation= Quaternion.Euler(0,0,90);
            obj.transform.localRotation = Quaternion.Euler(0, 0, 90);

            Destroy(child.gameObject);
            var temp = new GameObject("");
            obj.transform.parent = temp.transform;

            temp.transform.position = child.transform.position;
            temp.transform.rotation = child.transform.rotation;

            temp.transform.parent = transform.parent.GetComponentInChildren<PlayerObject>().transform;
            obj.transform.parent = transform.parent.GetComponentInChildren<PlayerObject>().transform;
            Destroy(temp);

        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
