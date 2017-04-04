using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour {

    public int Id;

    public List<PuzzleFace> _faces = new List<PuzzleFace>();
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
    public void ActivateAllFaces()
    {
        IsActive = true;
        foreach (var facc in _faces)
        {
            facc.TurnOnAll();
        }
    }
    // Use this for initialization
    void Start ()
	{
	   
    }

   
    public void Init()
    {
        LastActivatedFace = -1;
        Debug.Log(transform.childCount);
        foreach (Transform face in transform)
        {
            
            if (face.gameObject.GetComponent<PuzzleFace>())
            {
                face.gameObject.GetComponent<PuzzleFace>().Init();
                _faces.Add(face.GetComponent<PuzzleFace>());
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
