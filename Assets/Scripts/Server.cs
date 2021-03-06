﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
//using Random = UnityEngine.Random;

//public enum RoomsID
//{
//    Debref = 9999,
//    Bref = 9998,
//    Between = 9997,
//    Game = 9996
//}
//public class Team
//{
//    public string TeamName;
//    public int RoomNumber;
//    public int TeamId;
//    public int ScoreRoom;
//    public float RoomTime;
//    public int ScoreAll;
//}

//public class Room
//{
//    public int RoomTime;

//}

public class Server : MonoBehaviour
{
    public Text RoomTimer;
    //public Text Info;
    //public InputField InputField;
    public Text Score;
    public string TeamName;
    private float _localTimer;
    //private string qrString = "";
    //private bool background = true;
    //private Team t;
    //private bool _enteredRoom = false;
    //private float _serverEllapsed = 0;





    public void IncScore()
    {
        Score.text = (Int32.Parse(Score.text) + 1).ToString();
    }
    void Start()
    {
        //AndroidNFCReader.enableBackgroundScan();
        //AndroidNFCReader.ScanNFC(gameObject.name, "OnFinishScan");

        CreateTeam();

    }

    private int ID = 0;
    public void CreateTeam()
    {

        //t.teamId = PlayerPrefs.GetInt("teamId", 0); ;
        //if (t.teamId == 0)
        //{
        //    t.teamId = Random.Range(0, 333);
        //}
        //PlayerPrefs.SetInt("teamId", t.teamId);
        string url = "http://192.168.0.151:8000/TeamCreation/1";
        Debug.Log("SADASDAPSKKDPS TEAM  CREATIN");
        WWWForm form = new WWWForm();
        form.AddField("name", TeamName);
        byte[] rawData = form.data;
        var headers = new Dictionary<string, string>();
        WWW www = new WWW(url, rawData, headers);

        StartCoroutine(RequestTeamCreation(www));
    }


    public void EnterRoom()
    {
        string url = "http://192.168.0.151:8000/Enter/" + ID;

        WWWForm form = new WWWForm();
        form.AddField("", "bla");
        byte[] rawData = form.data;
        var headers = new Dictionary<string, string>();
        WWW www = new WWW(url, rawData, headers);

        StartCoroutine(RoomEnterRequest(www));



    }

    public void LeaveRoom()
    {
        string url = "http://192.168.0.151:8000/Finish/" + ID;

        WWWForm form = new WWWForm();
        form.AddField("score", Score.text);
        byte[] rawData = form.data;

        // headers.Add("X-Auth-Token", "7da4596d42e24f9798d73ec40bbbbd81");

        var headers = new Dictionary<string, string>();
        WWW www = new WWW(url, rawData, headers);


        StartCoroutine(RoomEndRequest(www));


    }
    IEnumerator GetTimeRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            //   Debug.Log("WWW Ok!: " + www.data);

            //t = JsonUtility.FromJson<Team>(www.data);
            //Debug.Log(t.TeamName);
            //Info.text = t.TeamName + " is in room:" + (t.RoomNumber) + " \nID:" + t.TeamId + " CurrScore" + t.ScoreRoom +
            //            " All scores" + t.ScoreAll;
            //Debug.Log(t.TeamId+"----"+t.RoomNumber.ToString());
            // print(float.Parse( www.data));
            //print(www.responseHeaders+"SAASDO{{SAD{SA");
            float t = 0;
            float.TryParse(www.data, NumberStyles.Float, null, out t);
            int time = ((int)t);
            RoomTimer.text = (Math.Floor(time/60.0f).ToString()+":"+time%60);
            //Debug.Log(time);
            if (time == -1)
            {
                LeaveRoom();
            }

            // Debug.Log(r.RoomTime);
            // t.RoomTime = r.RoomTime;
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
    void Update()
    {

        _localTimer += Time.deltaTime;
        if (_localTimer >= 0.5f)
        {
            _localTimer = 0;
            string url = "http://192.168.0.151:8000/Time/" + ID;

            WWW www = new WWW(url);

            StartCoroutine(GetTimeRequest(www));
            //Debug.Log("ROOM TIME AFTER COURUTINE "+t.RoomTime);
            //if (t != null)
            //{
            //    if (t.RoomTime > 0)
            //    {
            //        //t.RoomTime -= (int)_serverEllapsed;
            //        Debug.Log(_serverEllapsed);
            //        RoomTimer.text = t.RoomTime.ToString();
            //        if (t.RoomTime <= 0)
            //        {
            //            RoomTimer.text = "0";
            //            LeaveRoom();
            //        }
            //_currentRoom.RoomTime -= ((int)Time.time - lastTime)* 1000;
            //lastTime = (int) Time.time;
            //RoomTimer.text = (_currentRoom.RoomTime / 1000).ToString();
            //if (_currentRoom.RoomTime <= 0)
            //{
            //    string url = "http://192.168.0.151:8000/";
            //    Debug.Log("RoomFinished localy");
            //    WWWForm form = new WWWForm();
            //    t.ScoreRoom = int.Parse(Score.text);
            //    form.AddField("", "bla");

            //    byte[] rawData = form.data;
            //    var headers = new Dictionary<string, string>();

            //    headers.Add("Stage", "RoomFinished");
            //    headers.Add("RoomScore",t.ScoreRoom.ToString());
            //    headers.Add("TeamId",t.TeamId.ToString());
            //    headers.Add("RoomNumber", t.RoomNumber.ToString());

            //    // headers.Add("X-Auth-Token", "7da4596d42e24f9798d73ec40bbbbd81");
            //    _enteredRoom = false;
            //    WWW www = new WWW(url, rawData, headers);
            //    StartCoroutine(WaitForRequest(www));

            //}
            //    }
            //}
        }


    }
    IEnumerator RoomEnterRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW ENTERED!: " + www.data);

            //t = JsonUtility.FromJson<Team>(www.data);
            //Debug.Log(t.TeamName);
            //Info.text = t.TeamName + " is in room:" + (t.RoomNumber) + " \nID:" + t.TeamId + " CurrScore" + t.ScoreRoom +
            //            " All scores" + t.ScoreAll;
            //Debug.Log(t.TeamId+"----"+t.RoomNumber.ToString());


        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }


    //IEnumerator GetTeamInfoRequest(WWW www)
    //{
    //    yield return www;

    //    // check for errors
    //    if (www.error == null)
    //    {
    //        Debug.Log("WWW Ok!: " + www.data);

    //        //t = JsonUtility.FromJson<Team>(www.data);
    //        //Debug.Log(t.TeamName);
    //        //Info.text = t.TeamName + " is in room:" + (t.RoomNumber) + " \nID:" + t.TeamId + " CurrScore" + t.ScoreRoom +
    //        //            " All scores" + t.ScoreAll;
    //        //Debug.Log(t.TeamId+"----"+t.RoomNumber.ToString());
    //        t = JsonUtility.FromJson<Team>(www.data);
    //        Debug.Log(t.TeamName);
    //        Info.text = t.TeamName + " is in room:" + (t.RoomNumber) + " \nID:" + t.TeamId + " CurrScore" + t.ScoreRoom +
    //                    " All scores" + t.ScoreAll;

    //    }
    //    else
    //    {
    //        Debug.Log("WWW Error: " + www.error);
    //    }
    //}
    IEnumerator RoomEndRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW END!: " + www.data);

            //Debug.Log(t.TeamId+"----"+t.RoomNumber.ToString());

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
    IEnumerator RequestTeamCreation(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW TEAMCREATIOn: " + www.data);

            ID = int.Parse(www.data);
            Debug.Log(ID);
            //Debug.Log(t.TeamId+"----"+t.RoomNumber.ToString());
            EnterRoom();

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    //IEnumerator WaitForRequest(WWW www)
    //{
    //    yield return www;

    //    // check for errors
    //    if (www.error == null)
    //    {
    //        Debug.Log("WWW Ok!: " + www.data);

    //        var temp = JsonUtility.FromJson<Room>(www.data);
    //        if (temp != null)
    //        {
    //            _currentRoom = temp;
    //            RoomTimer.text = (_currentRoom.RoomTime / 1000).ToString();
    //        }

    //    }
    //    else
    //    {
    //        Debug.Log("WWW Error: " + www.error);
    //    }
    //}

    //void OnGUI()
    //{
    //    if (!background)
    //    {
    //        // Scan NFC button
    //        if (GUI.Button(new Rect(0, Screen.height - 50, Screen.width, 50), "Scan NFC"))
    //        {
    //            AndroidNFCReader.ScanNFC(gameObject.name, "OnFinishScan");
    //        }
    //        if (GUI.Button(new Rect(0, Screen.height - 100, Screen.width, 50), "Enable Backgraound Mode"))
    //        {
    //            AndroidNFCReader.enableBackgroundScan();
    //            background = true;
    //        }
    //    }
    //    else
    //    {
    //        if (GUI.Button(new Rect(0, Screen.height - 50, Screen.width, 50), "Disable Backgraound Mode"))
    //        {
    //            AndroidNFCReader.disableBackgroundScan();
    //            background = false;
    //        }
    //    }
    //    //if (qrString != "NO_ALLOWED_OS")
    //    //{
    //    //    Debug.Log(qrString);
    //    //    GUI.Label(new Rect(0, 0, Screen.width, 50), "Result: " + qrString + "new Version");
    //    //    if (qrString.Length < 3)
    //    //    {
    //    //        string url = "http://192.168.0.151:8000/";
    //    //        t.RoomNumber = Int32.Parse(qrString);
    //    //        WWWForm form = new WWWForm();
    //    //        form.AddField("", t.RoomNumber);
    //    //        form.AddField("", t.TeamId);
    //    //        byte[] rawData = form.data;
    //    //        var headers = new Dictionary<string, string>();
    //    //        headers.Add("Stage", "RoomEntery");

    //    //        WWW www = new WWW(url, rawData, headers);

    //    //        StartCoroutine(WaitForRequest(www));
    //    //    }
    //    //}
    //}

    //// NFC callback
    //void OnFinishScan(string result)
    //{

    //    // Cancelled
    //    if (result == AndroidNFCReader.CANCELLED)
    //    {

    //        // Error
    //    }
    //    else if (result == AndroidNFCReader.ERROR)
    //    {


    //        // No hardware
    //    }
    //    else if (result == AndroidNFCReader.NO_HARDWARE)
    //    {
    //    }


    //    qrString = getToyxFromUrl(result);
    //}

    //// Extract toyxId from url
    //string getToyxFromUrl(string url)
    //{
    //    int index = url.LastIndexOf('/') + 1;

    //    if (url.Length > index)
    //    {
    //        return url.Substring(index);
    //    }

    //    return url;
    //}

}
