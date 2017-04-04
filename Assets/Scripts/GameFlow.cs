using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameFlow : MonoBehaviour
{

    // Use this for initialization



    public int MaxElements = 6;
    public int NumbersToGuess = 2;

    private int SequienceCount = 1;

    public List<int> ArrayToGuess = new List<int>();
    public List<int> ArrayInputed = new List<int>();

    public bool GenerateMoreThenOneFace = true;
    public bool EachPlayerGetsPiece = true;

    public bool ModeSubstract = false;
    //private List<PuzzleFace> FacesPlayerOne = new List<PuzzleFace>();
    //private List<PuzzleFace> FacesPlayerTwo = new List<PuzzleFace>();
    public PlayerObject ObjectPlayerOne;
    public PlayerObject ObjectPlayerTwo;
    public PuzzleFace DebugPie;

    private bool _blockInput = false;
    private void ActivatePlayerFaces(PlayerObject player, int item)
    {
        if (ModeSubstract)
        {
            if (player.LastActivatedFace == -1)
            {
                int rnd = Random.Range(0, player.GetFacesCount());

                player.ActivateFace(rnd, item);
            }
            else
            {
                player.ActivateFace(player.LastActivatedFace, item);
            }


        }
        else
        {
            if (GenerateMoreThenOneFace)
            {
                int rnd = Random.Range(0, player.GetFacesCount());
                player.ActivateFace(rnd, item);

            }
            else
            {
                if (player.LastActivatedFace == -1)
                {
                    int rnd = Random.Range(0, player.GetFacesCount());

                    player.ActivateFace(rnd, item);
                }
                else
                {
                    player.ActivateFace(player.LastActivatedFace, item);
                }

            }
        }
    }
    void Start()
    {
        GenerateSequenece();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_blockInput);
        //possible error is number will be in the list on realeas for whatever reason
        if (!_blockInput)
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                ArrayInputed.Add(0);
                Check();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                ArrayInputed.Add(1);
                Check();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                ArrayInputed.Add(2);
                Check();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                ArrayInputed.Add(3);
                Check();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                ArrayInputed.Add(4);
                Check();
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                ArrayInputed.Add(5);
                Check();
            }


            if (Input.GetKeyUp(KeyCode.Q))
            {
                ArrayInputed.Remove(0);
                Check();
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                ArrayInputed.Remove(1);
                Check();
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                ArrayInputed.Remove(2);
                Check();
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                ArrayInputed.Remove(3);
                Check();
            }
            if (Input.GetKeyUp(KeyCode.T))
            {
                ArrayInputed.Remove(4);
                Check();
            }
            if (Input.GetKeyUp(KeyCode.Y))
            {
                ArrayInputed.Remove(5);
                Check();
            }
        }

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



        if (ModeSubstract)
        {
            foreach (var item in ArrayToGuess)
            {
                print(item);
                DebugPie.ActivatePiece(item);
            }
            int rnd = Random.Range(NumbersToGuess, MaxElements);
            List<int> temp = new List<int>();
            for (int i = 0; i < rnd; i++)
            {
                if (ArrayToGuess.Count > i)
                {
                    temp.Add(ArrayToGuess[i]);
                    ActivatePlayerFaces(ObjectPlayerOne, ArrayToGuess[i]);
                }
                else
                {
                    int r = Random.Range(0, MaxElements);
                    while (temp.Contains(r))
                    {
                        r = Random.Range(0, MaxElements);
                    }
                    temp.Add(r);
                    ActivatePlayerFaces(ObjectPlayerOne, r);
                }
            }


            //var firstNotSecond = ArrayToGuess.Except(temp).ToList();
            var secondNotFirst = temp.Except(ArrayToGuess).ToList();
            foreach (var num in secondNotFirst)
            {
                ActivatePlayerFaces(ObjectPlayerTwo, num);

            }




            //string first = "";
            //string second = "";
            //foreach (var num in firstNotSecond)
            //{

            //    first += num.ToString();

            //}
            //foreach (var num in secondNotFirst)
            //{

            //    second += num.ToString();

            //}
            //Debug.Log("first: " + first + "\nsecond: " + second);

        }
        else
        {
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
                    if (Random.Range(0, 2) == 0)
                    {
                        ActivatePlayerFaces(ObjectPlayerOne, item);
                    }
                    else
                    {
                        ActivatePlayerFaces(ObjectPlayerTwo, item);
                    }



                }
            }
        }
    }

    public void Check()
    {

        {
            Debug.Log(ArrayInputed.Count);

            string outer = "";
            string outer2 = "";
            int counter = 0;
            foreach (var num in ArrayInputed)
            {
                outer += num.ToString();
                outer2 += ArrayToGuess[counter].ToString();
                counter++;


            }
            if (ArrayInputed.Count == NumbersToGuess)
            {
                ArrayInputed.Sort();
                ArrayToGuess.Sort();
                if (ArrayInputed.SequenceEqual(ArrayToGuess))
                {
                    Guessed();
                    _blockInput = true;

                }
                else
                {
                    NotGuessed();
                    _blockInput = true;

                }
                Debug.Log(outer);
                Debug.Log(outer2);

            }
        }
    }
    public void Guessed()
    {
        Debug.Log("right");
        StartCoroutine(Clear());
    }

    private IEnumerator Clear()

    {
        ObjectPlayerOne.DeactivateAllFaces();
        ObjectPlayerTwo.DeactivateAllFaces();
        ArrayToGuess.Clear();
        ArrayInputed.Clear();
        DebugPie.TurnOffAll();
        yield return new WaitForSeconds(2);
        GenerateSequenece();
        _blockInput = false;
    }
    public void NotGuessed()
    {
        Debug.Log("wrong");
        StartCoroutine(Clear());
    }
}
