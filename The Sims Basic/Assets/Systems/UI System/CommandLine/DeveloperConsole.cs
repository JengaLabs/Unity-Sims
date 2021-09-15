using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperConsole : MonoBehaviour
{

    private readonly string prefix;
    private readonly IEnumerable<IConsoleCommand> commands;
    
    public DeveloperConsole(string prefix, IEnumerable<IConsoleCommand> commands)
    {
        this.prefix = prefix;
        this.commands = commands;
    }


    public void ProcessCommand(string inputValue)
    {
        //if the prefix for commands in the text
        if(!inputValue.StartsWith(prefix)) { return; }

        //remove the prefix
        inputValue = inputValue.Remove(0, prefix.Length);

        //Splits the string up into arrays of words 
        string[] inputSplit = inputValue.Split(' ');

        //The first word is the command word 
        string commandInput = inputSplit[0];

        //List of the other words
        List<string> args = new List<string>();

        for (int i = 1; i < inputSplit.Length; i++ )
        {
            //Add other arugments from command
            args.Add(inputSplit[i]);
        }

        //process the command
        ProcessCommand(commandInput, args);
    }

    public void ProcessCommand(string commandInput, List<string> args)
    {

        
        foreach (var command in commands)
        {
            //ignore uppercase
            if (!commandInput.Equals(command.CommandWord, System.StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (command.Process(args))
            {
                return;
            }
        }
    }

}
