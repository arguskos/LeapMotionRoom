using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour {

    public int Id;

    private List<PuzzleFace> _faces = new List<PuzzleFace>();
    public  int  LastActivatedFace { get; private set; }
    public bool IsActive { get; private set; }

    public int GetFacesCount()
    {
        return _faces.Count;
    }
    public PuzzleFace GetFace(int faceNum)
    {
        return _faces[faceNum];
    }
    public void ActivateFace(int faceNum,int piece)
    {
        IsActive = true;
        LastActivatedFace = faceNum;
        GetFace(faceNum).ActivatePiece(piece);
    }

    public void DeactivateAllFaces()
    {
        IsActive = false;
        foreach (var facc in _faces)
        {
            facc.TurnOffAll();
        }
    }
	// Use this for initialization
	void Start ()
	{
	    LastActivatedFace = -1;
        foreach (Transform child in transform)
        {

            _faces.Add(child.gameObject.GetComponent<PuzzleFace>());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
