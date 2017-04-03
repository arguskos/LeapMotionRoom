using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{

    // Use this for initialization
    public int MaxElements = 6;
    public int NumbersToGuess = 2;
    public List<int> ArrayToGuess = new List<int>();

    public bool GenerateMoreThenOneFace = false;
    public bool EachPlayerGetsPiece = true;
    //private List<PuzzleFace> FacesPlayerOne = new List<PuzzleFace>();
    //private List<PuzzleFace> FacesPlayerTwo = new List<PuzzleFace>();
    public PlayerObject ObjectPlayerOne;
    public PlayerObject ObjectPlayerTwo;
    public PuzzleFace DebugPie;
    private void ActivatePlayerFaces(PlayerObject player, int item)
    {
        int rnd = Random.Range(0, player.GetFacesCount());
        player.ActivateFace(rnd, item);
    }
    void Start()
    {
        GenerateSequenece();
        foreach (var item in ArrayToGuess)
        {
            print(item);
            DebugPie.ActivatePiece(item);
            if (EachPlayerGetsPiece)
            {
                if (ObjectPlayerOne.IsActive == ObjectPlayerTwo.IsActive)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        ActivatePlayerFaces(ObjectPlayerOne, item);
                    }
                    else
                    {
                        ActivatePlayerFaces(ObjectPlayerTwo, item);


                    }
                }
                else if (!ObjectPlayerOne.IsActive)
                {
                    ActivatePlayerFaces(ObjectPlayerOne, item);

                }
                else if (!ObjectPlayerTwo.IsActive)
                {
                    ActivatePlayerFaces(ObjectPlayerTwo, item);

                }
            }
            else
            {

               
                    ActivatePlayerFaces(ObjectPlayerOne, item);

                
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GenerateSequenece()
    {
        for (int i = 0; i < NumbersToGuess; i++)
        {
            int rnd = Random.Range(0, MaxElements);
            while (ArrayToGuess.Contains(rnd))
            {
                rnd = Random.Range(0, MaxElements);

            }
            ArrayToGuess.Add(rnd);
        }
    }
    public void Guessed()
    {

    }
    public void NotGuessed()
    {

    }
}
