using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleCommand : ScriptableObject, IConsoleCommand
{
    [SerializeField] private string commandWord = string.Empty;

    public string CommandWord => commandWord;


    public bool Process(List<string> args)
    {
        throw new System.NotImplementedException();
    }
}
