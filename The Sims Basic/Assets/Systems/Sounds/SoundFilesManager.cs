using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Handles getting all sound files and handing them to the sound manager
public class SoundFilesManager
{
    
    private string SoundFileDirectory; 

    //Collection of sounds and file names 
    private Dictionary<string, AudioClip> Clips = new Dictionary<string, AudioClip>();

    public SoundFilesManager()
    {
        SoundFileDirectory = Application.persistentDataPath;
    }


    /// <summary>
    /// Return all sound files with names 
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, AudioClip> GetAudioClips()
    {
        GetSoundFiles();
        return Clips;
    }

    public void GetSoundFiles()
    {
        Debug.Log("Getting sound files ");
        Object[] clips = Resources.FindObjectsOfTypeAll(typeof(AudioClip));
        
        if(clips.Length <= 0)
        {
            Debug.Log("No sound files found");
            return;
        }
        foreach(var obj in clips)
        {
            Debug.Log(obj.name);
        }
    }

}
