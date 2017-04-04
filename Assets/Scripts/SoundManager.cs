using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{


    //Initialize
    public AudioSource[] soundArray = new AudioSource[4];
    public AudioSource[] musicArray = new AudioSource[3];
    public AudioSource[] ambientArray = new AudioSource[4];

    public float musicIntensity = 0.0f;
    public float ambientIntensity = 0.0f;
    public float totalTimeInSeconds = 500.0f;

    public AudioMixer AudioMaster;
        
    public enum Sound { Select, Wrong, Progress1, Progress2 };

    //Play sound function
    public void PlaySound(string soundName)
    {
        for (int i = 0; i < soundArray.Length; i++)
        {
            if (soundArray[i].name == soundName)
            {
                soundArray[i].Play();
            }
        }
    }


    public void PlaySound(Sound soundName)
    {

        soundArray[(int)soundName].Play();

    }


    //Loop sound function
    public void ToggleLoop(string loopName)
    {
        for (int i = 0; i < soundArray.Length; i++)
        {
            if ((soundArray[i].name == loopName) && (!soundArray[i].isPlaying))
            {
                soundArray[i].Play();
            }

            else if (soundArray[i].name == loopName)
            {
                soundArray[i].Stop();
            }
        }
    }


    public void ToggleLoop(Sound soundName)
    {

        if ((!soundArray[(int)soundName].isPlaying))
        {
            soundArray[(int)soundName].Play();
        }

        else
        {
            soundArray[(int)soundName].Stop();
        }

    }



    void Update()
    {
        //Music Blending
        for (int i = 0; i < musicArray.Length; i++)
        {
            if (musicIntensity < i)
            {
            }
            else if (musicIntensity > i)
            {
                musicArray[i].GetComponent<AudioSource>().volume = musicIntensity % 1.0f;
            }

            if (musicIntensity - 1.0f >= i)
            {
                musicArray[i].GetComponent<AudioSource>().volume = 1.0f;
            }
        }

        //Ambient Blending
        for (int i = 0; i < ambientArray.Length; i++)
        {
            if (ambientIntensity < i)
            {
            }
            else if (ambientIntensity > i)
            {
                ambientArray[i].GetComponent<AudioSource>().volume = ambientIntensity % 1.0f;
            }

            if (ambientIntensity - 1.0f >= i)
            {
                ambientArray[i].GetComponent<AudioSource>().volume = 1.0f;
            }
        }

        //Randomize sounds
        ambientIntensity = 0.5f + ((ambientArray.Length) * (Time.realtimeSinceStartup / totalTimeInSeconds));
        musicIntensity = -1 + (musicArray.Length + 1) * (Time.realtimeSinceStartup / totalTimeInSeconds);

        //Debug Soundtests
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaySound("Selection");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlaySound("Wrong");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlaySound("Error");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlaySound("Progress_001");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlaySound("Progress_002");
        }
    }
    
}
