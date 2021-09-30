using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Audio;

//Handles getting all sound files 
public class SoundFilesManager
{
    
    private string SoundFileDirectory; 

    //Collection of sounds and file names 
    private Dictionary<string, AudioClip> Clips = new Dictionary<string, AudioClip>();

    public SoundFilesManager()
    {
        SoundFileDirectory = Application.streamingAssetsPath;
        GetSoundFiles();

    }


    /// <summary>
    /// Return all sound files with names 
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, AudioClip> GetAudioClips()
    {
        return Clips;
    }

    public void GetSoundFiles()
    {
        //Load all sound files and store them 
        object[] clips = (AudioClip[])Resources.LoadAll("", typeof(AudioClip));

        
        if(clips.Length <= 0)
        {
            Debug.LogError("No sound files found in resources" + this);
            return;
        }

        //Loop through every loaded clip
        foreach(AudioClip clip in clips)
        {
            //Check if that clip name ins't used
            if (!Clips.ContainsKey(clip.name))
            {
                //add that clip
                Clips.Add(clip.name, clip);
            }
            else
            {
                Debug.Log("Clip already added");
            }
            
        }
    }

}
