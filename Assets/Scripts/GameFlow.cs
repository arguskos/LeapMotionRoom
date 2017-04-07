using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

    public ArduinoButtons script;

    [Header("UI")]
    public GameObject TXTScore;
    public GameObject TXTMessage;
    public GameObject TXTScoreIncrement;
    public int Score;

    private List<List<PuzzleFace>> _sequence = new List<List<PuzzleFace>>();



    //private List<PuzzleFace> FacesPlayerOne = new List<PuzzleFace>();
    //private List<PuzzleFace> FacesPlayerTwo = new List<PuzzleFace>();

    [Header("Prefabs")]
    public PlayerObject ObjectPlayerOne;
    public PlayerObject ObjectPlayerTwo;
    public PuzzleFace PreviewPie;
    public PuzzleFace DebugPie;
    public GameObject NewPart;

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
    public int[] LevelProgression;
    public int[] Low;
    public int[] MaxCombo;
    public int[] High;
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

                    int rnd = Random.Range(0, player.GetNotActivateFaces().Count);
                    Debug.Log(rnd + ": rnd" + item);
                    player.ActivateFace(player.GetNotActivateFaces()[rnd], item);
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
        _sequence.Clear();
        var temp = (ObjectPlayerOne.GetFaces().Concat(ObjectPlayerTwo.GetFaces())).ToList();
        Debug.Log(temp.Count + "tocunter");
        int facesCovered = 0;
        while (temp.Count > 0)
        {
            Debug.Log(facesCovered + "faces covered");
            if (temp.Count() == 1)
            {


                _sequence.Add(new List<PuzzleFace>());
                _sequence[_sequence.Count - 1].Add(temp[0]);
                temp.Remove(temp[0]);
                facesCovered++;
            }
            else
            {
                int rnd = Random.Range(0, LevelProgression[Stage]);
                facesCovered += rnd;
                if (rnd == 0)
                {
                    int rndFace = Random.Range(0, temp.Count());
                    PuzzleFace tempFace = temp[rndFace];
                    _sequence.Add(new List<PuzzleFace>());
                    _sequence[_sequence.Count - 1].Add(tempFace);
                    temp.Remove(tempFace);
                    facesCovered++;
                }
                if (rnd == 1)
                {
                    int rndFace = Random.Range(0, temp.Count());
                    PuzzleFace tempFace = temp[rndFace];
                    _sequence.Add(new List<PuzzleFace>());
                    _sequence[_sequence.Count - 1].Add(tempFace);
                    temp.Remove(tempFace);
                    facesCovered++;
                    rndFace = Random.Range(0, temp.Count());
                    tempFace = temp[rndFace];
                    _sequence[_sequence.Count - 1].Add(tempFace);
                    temp.Remove(tempFace);
                    facesCovered++;
                }
            }
        }
        Debug.Log("lol" + _sequence.Count + " " + temp.Count);
        _partsInSequence = _sequence.Count;
        SoundManager.PlaySound("Progress_002");

    }
    void Start()
    {
        Stage = 0;
        SpawnCurrent();
        Score = 0;
        GenerateSequenece();

        StartCoroutine(Clear());

        TXTScore.GetComponent<Text>().text = Score.ToString();
        ShowPartInSequnce();

    }
    public void AddToArray(int i)
    {
        ArrayInputed.Add(i);

    }

    public void RemoveFromArray(int i)
    {
        ArrayInputed.Remove(i);
    }
    // Update is called once per frame
    void Update()
    {
        //---------
        //Over here an example of the preferred use of the UI feedback:
        //When a shape is correct
        if (Input.GetKeyDown(KeyCode.U))
        {
            TXTMessage.GetComponent<BlinkText>().TriggerText("Correct", 1.0f,true);
            TXTScoreIncrement.GetComponent<BlinkText>().TriggerText("+1", 1.5f, false); //Over here +1 should change to the amount incremented
        }

        //When a shape is Wrong
        if (Input.GetKeyDown(KeyCode.I))
        {
            TXTMessage.GetComponent<BlinkText>().TriggerText("Wrong", 1.0f, true);
            TXTScoreIncrement.GetComponent<BlinkText>().TriggerText("-1", 1.5f, false); //If we're gonna use minus points as punishment use this
        }

        //When a shape is shifting
        if (Input.GetKeyDown(KeyCode.O))
        {
            TXTMessage.GetComponent<BlinkText>().TriggerText("Next Level", 2.0f, true);
            TXTScoreIncrement.GetComponent<BlinkText>().TriggerText("+10", 1.5f, false); //Maybe add bonus points when a shape is complete?
        }
        //---------

        //Play selection sound on keydown
        if (!_isBlocked)
        {
            //if (Input.anyKeyDown)
            //{
            //    SoundManager.PlaySound("Selection");
            //}

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
      
        Debug.Log(_currentPart+" currpart");
        List<int > generated = new List<int>();
      
        foreach (var face in _sequence[_currentPart])
        {
            int high = High[Stage];
            if (_sequence.Count > 1)
            {
                high = MaxCombo[Stage];
            }
            for (int i = 0; i < Random.Range(Low[Stage],high); i++)
            {
                int rnd = Random.Range(0, MaxElements);

                while (generated.Contains(rnd))
                {
                    rnd = Random.Range(0, MaxElements);
                }
                generated.Add(rnd);
                DebugPie.ActivatePiece(rnd);
                ArrayToGuess.Add(rnd);
                face.ActivatePiece(rnd);
                Debug.Log(rnd);
            }   
        }
        string outer = "";
        foreach (var num in ArrayToGuess)
        {
            outer += num.ToString();

        }
        Debug.Log(outer+ " SEQUENCE");
        //Debug.Log("generAting");
        //for (int i = 0; i < NumbersToGuess; i++)
        //{
        //    int rnd = Random.Range(0, MaxElements);
        //    while (ArrayToGuess.Contains(rnd))
        //    {
        //        rnd = Random.Range(0, MaxElements);

        //    }

        //    ArrayToGuess.Add(rnd);

        //}



        //if (ModeSubstract)
        //{
        //    foreach (var item in ArrayToGuess)
        //    {
        //        print(item);
        //        DebugPie.ActivatePiece(item);
        //    }
        //    int rnd = Random.Range(2, MaxElements);
        //    List<int> temp = new List<int>();
        //    for (int i = 0; i < rnd; i++)
        //    {
        //        if (ArrayToGuess.Count > i)
        //        {
        //            temp.Add(ArrayToGuess[i]);
        //            ActivatePlayerFaces(ObjectPlayerOne, ArrayToGuess[i]);
        //        }
        //        else
        //        {
        //            int r = Random.Range(0, MaxElements);
        //            temp.Add(r);
        //            ActivatePlayerFaces(ObjectPlayerOne, r);
        //        }
        //    }


        //    var firstNotSecond = ArrayToGuess.Except(temp).ToList();
        //    var secondNotFirst = temp.Except(ArrayToGuess).ToList();
        //    foreach (var num in secondNotFirst)
        //    {
        //        ActivatePlayerFaces(ObjectPlayerTwo, num);

        //    }
        //    string first = "";
        //    string second = "";
        //    foreach (var num in firstNotSecond)
        //    {

        //        first += num.ToString();

        //    }
        //    foreach (var num in secondNotFirst)
        //    {

        //        second += num.ToString();

        //    }
        //    Debug.Log("first: " + first + "\nsecond: " + second);

        //}
        //else
        //{
        //    foreach (var item in ArrayToGuess)
        //    {
        //        print(item);
        //        DebugPie.ActivatePiece(item);
        //        if (EachPlayerGetsPiece)
        //        {
        //            if (ObjectPlayerOne.IsActive == ObjectPlayerTwo.IsActive)
        //            {
        //                if (Random.Range(0, 2) == 0)
        //                {
        //                    ActivatePlayerFaces(ObjectPlayerOne, item);
        //                }
        //                else
        //                {
        //                    ActivatePlayerFaces(ObjectPlayerTwo, item);


        //                }
        //            }
        //            else if (!ObjectPlayerOne.IsActive)
        //            {
        //                ActivatePlayerFaces(ObjectPlayerOne, item);

        //            }
        //            else if (!ObjectPlayerTwo.IsActive)
        //            {
        //                ActivatePlayerFaces(ObjectPlayerTwo, item);

        //            }
        //        }
        //        else
        //        {
        //            if (Random.Range(0, 2) == 0)
        //            {
        //                ActivatePlayerFaces(ObjectPlayerOne, item);
        //            }
        //            else
        //            {
        //                ActivatePlayerFaces(ObjectPlayerTwo, item);
        //            }



        //        }
        //    }
        //}
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
        if (!_isChecking)
        {
            _isChecking = true;
            StartCoroutine(SlowChecker(key, ArrayInputed, ArrayToGuess));
        }
    }

    private IEnumerator SlowChecker(KeyCode key, List<int> input, List<int> guess)
    {
        //sleeps for two seconds then check
        yield return new WaitForSeconds(2.0f);
        input.Sort();
        guess.Sort();
        Debug.Log(Input.GetKey(key));
        if ((Input.GetKey(key)) )
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

        SoundManager.PlaySound("Progress_001");

        for (int i = 0; i < Sequence.transform.GetChild(_currentPart).GetComponent<PuzzleFace>().ElementObjects.Count; i++)
        {
            if (ArrayToGuess.Contains(i))
            {
                Sequence.transform.GetChild(_currentPart).GetComponent<PuzzleFace>().ElementObjects[i].DeffaultOn();
            }
        }
        _currentPart++;
        if (_currentPart > 4)

        {
            StartCoroutine(MoveSequence());
        }
        if (_currentPart== _partsInSequence)
        {
            Stage++;
            _currentPart = 0;
            SpawnCurrent();
            for (int i = 0; i < Sequence.transform.GetChild(_currentPart).GetComponent<PuzzleFace>().ElementObjects.Count; i++)
            {
                Sequence.transform.GetChild(i).GetComponent<PuzzleFace>().TurnOffAll();
            }

        }
        NumbersToGuess = Random.Range(1, 3);
        StartCoroutine(Clear());

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
        Destroy(Sequence.transform.GetChild(0).gameObject);
        GameObject part = Instantiate(NewPart.gameObject);

        part.transform.parent = Sequence.transform;
        part.transform.position = Sequence.transform.GetChild(5).gameObject.transform.position + new Vector3(0.5f, 0, 0);
        part.transform.localScale = Sequence.transform.GetChild(5).gameObject.transform.localScale;
        part.transform.rotation = Sequence.transform.GetChild(5).gameObject.transform.rotation;

        for (int i = 0; i < 10; i++)
        {
            Sequence.transform.position -= new Vector3(0.05f, 0, 0);
            yield return null;

        }

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
        SoundManager.PlaySound("Error");
        Score -= 50;
        _currentPart = 0;
        SoundManager.PlaySound("Wrong");
        StartCoroutine(Clear());
        ShowPartInSequnce();


    }
}
