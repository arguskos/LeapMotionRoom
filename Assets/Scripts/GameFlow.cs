using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{

    // Use this for initialization
    [Header("Don't touch public for debug")]
    public int MaxElements = 6;
    public int NumbersToGuess = 2;
    public List<int> ArrayToGuess = new List<int>();
    public List<int> ArrayInputed = new List<int>();
    public bool GenerateMoreThenOneFace = true;
    public bool EachPlayerGetsPiece = true;
    public bool ModeSubstract = false;

    [Header("UI")]
    public GameObject TXTScore;
    public int Score;



    //private List<PuzzleFace> FacesPlayerOne = new List<PuzzleFace>();
    //private List<PuzzleFace> FacesPlayerTwo = new List<PuzzleFace>();

    [Header("Prefabs")]
    public PlayerObject ObjectPlayerOne;
    public PlayerObject ObjectPlayerTwo;
    public PuzzleFace PreviewPie;
    public PuzzleFace DebugPie;
    public PuzzleFace NewPart;

    public SoundManager SoundManager;
    public GameObject Sequence;
    public GameObject[] Shapes;

    [Header("Leap motion")]
    public Leap.Unity.Attachments.HandAttachments HandLeft;
    public Leap.Unity.Attachments.HandAttachments HandRight;


    private bool _isBlocked = false;
    private bool _isChecking = false;
    private int _partsInSequence;
    private int _currentPart = 0;
    public int Stage { get; private set; }


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
                    Debug.Log(rnd + ": rnd" + item);
                    player.ActivateFace(rnd, item);
                }
                else
                {
                    player.ActivateFace(player.LastActivatedFace, item);
                }

            }
        }
    }

    private void SpawnCurrent()
    {
        if (ObjectPlayerOne && ObjectPlayerTwo)
        {
            Destroy(ObjectPlayerOne.transform.parent.gameObject);
            Destroy(ObjectPlayerTwo.transform.parent.gameObject);
        }
        GameObject obj1 = Instantiate(Shapes[Stage]);
        GameObject obj2 = Instantiate(Shapes[Stage]);



        PlaceFaces p1 = obj1.GetComponentInChildren<PlaceFaces>();
        PlaceFaces p2 = obj2.GetComponentInChildren<PlaceFaces>();
        p1.Init();
        p2.Init();

        ObjectPlayerOne = obj1.GetComponentInChildren<PlayerObject>();
        ObjectPlayerTwo = obj2.GetComponentInChildren<PlayerObject>();
        ObjectPlayerOne.Init();
        ObjectPlayerTwo.Init();
        HandRight.Palm = ObjectPlayerOne.transform;
        HandLeft.Palm = ObjectPlayerTwo.transform;
        _partsInSequence = ObjectPlayerOne._faces.Count;

    }
    void Start()
    {
        Stage = 0;
        SpawnCurrent();
        GenerateSequenece();
        Score = 0;
     
        TXTScore.GetComponent<Text>().text = Score.ToString();
        NotGuessed();
        ShowPartInSequnce();

    }

    // Update is called once per frame
    void Update()
    {
        //Play selection sound on keydown
        if (!_isBlocked)
        {
            if (Input.anyKeyDown)
            {
                SoundManager.PlaySound("Selection");
            }

            //Add number to inputted array if it isn't full yet
            if (ArrayInputed.Count <= NumbersToGuess)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ArrayInputed.Add(0);
                    Check(KeyCode.Q);
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    ArrayInputed.Add(1);
                    Check(KeyCode.W);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ArrayInputed.Add(2);
                    Check(KeyCode.E);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    ArrayInputed.Add(3);
                    Check(KeyCode.R);
                }
                if (Input.GetKeyDown(KeyCode.T))
                {
                    ArrayInputed.Add(4);
                    Check(KeyCode.T);
                }
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    ArrayInputed.Add(5);
                    Check(KeyCode.Y);
                }
            }

            //Remove from inputted array on keyup
            if (Input.GetKeyUp(KeyCode.Q))
            {
                ArrayInputed.Remove(0);
                Check(KeyCode.Q);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                ArrayInputed.Remove(1);
                Check(KeyCode.W);
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                ArrayInputed.Remove(2);
                Check(KeyCode.E);
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                ArrayInputed.Remove(3);
                Check(KeyCode.R);
            }
            if (Input.GetKeyUp(KeyCode.T))
            {
                ArrayInputed.Remove(4);
                Check(KeyCode.T);
            }
            if (Input.GetKeyUp(KeyCode.Y))
            {
                ArrayInputed.Remove(5);
                Check(KeyCode.Y);
            }
        }
        if (PreviewPie)

            //Activate previewpie UI and start charging on buttonpress

            for (int i = 0; i < MaxElements; i++)
            {
                if (ArrayInputed.Contains(i))
                {
                    PreviewPie.ActivatePiece(i);
                }
                else
                {
                    PreviewPie.DeactivatePiece(i);
                }


            }
        /*if (ArrayInputed.Count == 0)
        {
            PreviewPie.TurnOffAll();
        }*/
        //Check for solution


        //Update UI Score
    }

    public void GenerateSequenece()
    {
        Debug.Log("generAting");
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
            int rnd = Random.Range(2, MaxElements);
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
                    temp.Add(r);
                    ActivatePlayerFaces(ObjectPlayerOne, r);
                }
            }


            var firstNotSecond = ArrayToGuess.Except(temp).ToList();
            var secondNotFirst = temp.Except(ArrayToGuess).ToList();
            foreach (var num in secondNotFirst)
            {
                ActivatePlayerFaces(ObjectPlayerTwo, num);

            }
            string first = "";
            string second = "";
            foreach (var num in firstNotSecond)
            {

                first += num.ToString();

            }
            foreach (var num in secondNotFirst)
            {

                second += num.ToString();

            }
            Debug.Log("first: " + first + "\nsecond: " + second);

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

    public void Check(KeyCode key)
    {
        //        Debug.Log(ArrayInputed.Count);

        string outer = "";
        string outer2 = "";
        int counter = 0;
        //foreach (var num in ArrayInputed)
        //{
        //    outer += num.ToString();
        //    outer2 += ArrayToGuess[counter].ToString();
        //    counter++;


        //}
        if (ArrayInputed.Count == NumbersToGuess && !_isChecking)
        {
            _isChecking = true;
            StartCoroutine(SlowChecker(key, ArrayInputed, ArrayToGuess));
        }
    }

    private IEnumerator SlowChecker(KeyCode key, List<int> input, List<int> guess)
    {
        //sleeps for two seconds then check
        yield return new WaitForSeconds(0.8f);
        input.Sort();
        guess.Sort();
        Debug.Log(Input.GetKey(key));
        if (Input.GetKey(key)&& ArrayInputed.Count == NumbersToGuess)
        {
            if (input.SequenceEqual(guess))
            {
                Guessed();
            }
            else
            {
                NotGuessed();
            }
        }
        TXTScore.GetComponent<Text>().text = Score.ToString();

        _isChecking = false;
    }
    //Correct
    private void Guessed()
    {
        Debug.Log("right");
        Score += 150;

        SoundManager.PlaySound("Progress_002");

        for (int i = 0; i < Sequence.transform.GetChild(_currentPart).GetComponent<PuzzleFace>().ElementObjects.Count; i++)
        {
            if (ArrayToGuess.Contains(i))
            {
                Sequence.transform.GetChild(_currentPart).GetComponent<PuzzleFace>().ElementObjects[i].DeffaultOn();
            }
        }
        _currentPart++;
       
        if (_currentPart == _partsInSequence)
        {
            Stage++;
            _currentPart = 0;
            SpawnCurrent();
            for (int i = 0; i < Sequence.transform.GetChild(_currentPart).GetComponent<PuzzleFace>().ElementObjects.Count; i++)
            {
                Sequence.transform.GetChild(i).GetComponent<PuzzleFace>().TurnOffAll();
            }

        }
        NumbersToGuess = Random.Range(1,4);
        StartCoroutine(Clear());
        if (_currentPart > 1)

        {
            StartCoroutine(MoveSequence());
        }
        _isBlocked = true;
    }

    //Clear
    private void ShowPartInSequnce()
    {
        HideAllPartInSequence();
        for (int i = 0; i < 6; i++)
        {
            if (i < _partsInSequence)
            {
                Sequence.transform.GetChild(i).gameObject.SetActive(true);

            }
        }
    }

    private void HideAllPartInSequence()
    {
        foreach (Transform part in Sequence.transform)
        {
            part.gameObject.SetActive(false);
        }
    }
    private IEnumerator Clear()

    {

        ArrayToGuess.Clear();
        ArrayInputed.Clear();
        DebugPie.TurnOffAll();
        PreviewPie.TurnOffAll();

        yield return new WaitForSeconds(2);
        ObjectPlayerOne.DeactivateAllFaces();
        ObjectPlayerTwo.DeactivateAllFaces();
        GenerateSequenece();
        ShowPartInSequnce();
        _isBlocked = false;
    }

    private IEnumerator MoveSequence()
    {
        for (int i = 0; i < 10; i++)
        {
            Sequence.transform.position-=new Vector3(0.1f,0,0);
            yield return null;

        }
        Destroy(Sequence.transform.GetChild(0).gameObject);
        PuzzleFace part = Instantiate(NewPart,new Vector3(0,0,0),Quaternion.identity);
        part.transform.parent = Sequence.transform;
        part.transform.position=new Vector3(0,0,0);
    }
    //Wrong
    private void NotGuessed()
    {
        _isBlocked = true;
        for (int i = 0; i < Sequence.transform.GetChild(_currentPart).GetComponent<PuzzleFace>().ElementObjects.Count; i++)
        {
            Sequence.transform.GetChild(i).GetComponent<PuzzleFace>().TurnOffAll();
            
        }
        Debug.Log("wrong");
        Score -= 50;
        _currentPart = 0;
        SoundManager.PlaySound("Wrong");
        StartCoroutine(Clear());
        ShowPartInSequnce();


    }
}
