using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    //Menu sounds source
    private AudioSource _AudioSource;

    //Data type for holding sounds and thier corosponding keys
    Dictionary<string, AudioClip> Clips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        //Get _audio source to use
        //_AudioSource = this.gameObject.GetComponent(AudioSource);

        //Get audio clips from sound manager


        //Set self as audio for menu sounds
    }

    public void AddSound(string SoundName, AudioClip SoundClip)
    {
        //Check clip name does not already exist
        if(Clips.ContainsKey(SoundName) == false)
        {
            //Check sound clip is not already being used
            if (Clips.ContainsValue(SoundClip))
            {
                //Add clip to current list
                Clips.Add(SoundName, SoundClip);
            }
            else
            {
                Debug.Log("Sound clip " + SoundClip.name + " already exist");
            }
        }
        else
        {
            Debug.Log("Sound name " + SoundName + " already in use");
        }
    }

    public void PlaySound(string SoundName)
    {
        //Verifiy sound exist
        if(Clips.ContainsKey(SoundName) == true)
        {
            //Set Sound clip as current clip 
            _AudioSource.clip = Clips[SoundName];
            //Play Clip
            _AudioSource.Play();
            return;
        }

        Debug.Log("Sound does not exist");

    }







}
