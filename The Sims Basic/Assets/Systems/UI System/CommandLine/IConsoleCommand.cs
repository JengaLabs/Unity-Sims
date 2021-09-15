using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IConsoleCommand
{
    //Every command has a trigger string
    string CommandWord { get; }

    //If the command excuted right
    bool Process(List<string> args);

}
