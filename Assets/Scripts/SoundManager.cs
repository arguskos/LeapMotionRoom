using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{


    //Initialize
    public AudioSource[] soundArray = new AudioSource[4];

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



    //Example of use
    /*void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PlaySound("Error");
            ToggleLoop("MachineLoop");
        }
    }*/
}
