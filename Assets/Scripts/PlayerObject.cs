using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{

    public int Id;

    public List<PuzzleFace> _faces = new List<PuzzleFace>();
    public int LastActivatedFace { get; private set; }
    public bool IsActive { get; private set; }

    public int GetFacesCount()
    {
        return _faces.Count;
    }
    public PuzzleFace GetFace(int faceNum)
    {
        return _faces[faceNum];
    }

    public List<PuzzleFace> GetFaces()
    {
        Debug.Log(_faces.Count+"counter faces");
        return _faces;
    }
    public void ActivateFace(int faceNum, int piece)
    {
        IsActive = true;
        LastActivatedFace = faceNum;
        GetFace(faceNum).ActivatePiece(piece);
    }

    public int GetNumberActivatedFaces()
    {
        int sum = 0;
        foreach (var face in _faces)
        {
            if (face.IsActivated)
                sum++;
            
        }
        return sum;
    }
    public void ActivateFace(PuzzleFace face, int piece)
    {
        face.ActivatePiece(piece);
    }
    public List<PuzzleFace> GetNotActivateFaces()
    {
        List<PuzzleFace> temp = new List<PuzzleFace>();
        for (int i = 0; i < _faces.Count; i++)
        {
            if (!_faces[i].IsActivated)
            {
                temp.Add(_faces[i]);
            }
        }
        return temp;
    }
    public void DeactivateAllFaces()
    {
        LastActivatedFace = -1;
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
    void Start()
    {
    }


    public void Init()
    {
        LastActivatedFace = -1;
        IsActive = false;
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
    void Update()
    {

    }
}
