using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeveloperConsoleBehaviour : MonoBehaviour
{


    [SerializeField] private string prefix = string.Empty;
    [SerializeField] private ConsoleCommand[] commands = new ConsoleCommand[0];

    //UI it sits on 
    [Header("UI")]
    [SerializeField] private GameObject uiCanvas = null;
    [SerializeField] private TMP_InputField inputField = null;


    //Return to time scale of 0 
    private float pausedTimeScale;

    private static DeveloperConsoleBehaviour instance;

    private DeveloperConsole developerConsole;

    //Keep track of world delegates
    private InputClass _InputClass;

    private DeveloperConsole DeveloperConsole
    {
        get
        {
            if(developerConsole != null)
            {
                return developerConsole;
            }
            return developerConsole = new DeveloperConsole(prefix, commands);
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        //DontDestroyOnLoad(gameObject);

        //get the input class 
        _InputClass = GameManager.Instance.GetInputClass();

        //Set the developer console as the current input
        _InputClass.onDeveloperOpenConsole += Toggle;

    }

    public void Toggle()
    {
        Debug.Log("Toggling dev console");
        
        if (!uiCanvas.activeSelf)
        {
            
            Time.timeScale = pausedTimeScale;
            //Set the canvas active
            _InputClass.ManipulateAMenu("Debug UI", true);
        }
        else
        {
            //cache time scale
            pausedTimeScale = Time.timeScale;
            //Set time scale
            Time.timeScale = 1;
            //Set ui console true
            _InputClass.ManipulateAMenu("Debug UI", false);

            //inputField.ActivateInputField();
        }
    }

    public void ProcessCommand(string inputValue)
    {
        DeveloperConsole.ProcessCommand(inputValue);

        inputField.text = string.Empty;
    }

}
