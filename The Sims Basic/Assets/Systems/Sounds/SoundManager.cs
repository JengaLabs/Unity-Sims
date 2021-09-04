using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Takes sound files and asigns them to specific sound managers 
public class SoundManager : MonoBehaviour
{
    

    //Audio source object
    public AudioSource audioSourcePrefab;
    //Audio source pool
    private AudioSourcePool _AudioSourcePool;

    //Sound files manager 
    private SoundFilesManager _SoundsFilesManager;

    //Data type for holding sounds and their names 
    Dictionary<string, AudioClip> Clips = new Dictionary<string, AudioClip>();

    //Input Events
    private InputClass InputEvents;


    private void Awake()
    {
        //Create a audio source pool 
        _AudioSourcePool = new AudioSourcePool(audioSourcePrefab);

        //Get sound file manager
        _SoundsFilesManager = GameManager.Instance.GetSoundFileManager();

        //Get sounds from sound file manager 
        Clips = _SoundsFilesManager.GetAudioClips();

        //Get input class
        InputEvents = GameManager.Instance.GetInputClass();

        InputEvents.onPlaySound += PlaySound;
    }


    private void PlaySound(string soundName)
    {
        PlaySoundAtPoint(soundName, Camera.main.transform.position);
    }


    private void PlaySoundAtPoint(string SoundName, Vector3 position)
    {
        //Check audio exist 
        if (ValidateSoundFile(SoundName))
        {
            _AudioSourcePool.PlayAtPoint(Clips[SoundName], position);
        }


        return;
    }

    

    private bool ValidateSoundFile(string SoundName)
    {
        if (Clips.ContainsKey(SoundName))
        {
            return true;
        }
        Debug.Log("Audio file " + SoundName + " does not exist in audio clips.");
        return false;
    }

    



}
