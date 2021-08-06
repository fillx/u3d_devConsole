using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DeveloperConsole
{
    [ConsoleParse]
    public class DeveloperConsoleCommads : MonoBehaviour
    {
        private void Start()
        {
            Console.AddCommand("myFirstCommand", this, HelloWorld);
        }


        private void HelloWorld(string [] arg)
        {
            Debug.Log("Hello World");
        }


        [ConsoleCommand("mySecondCommand")] // Add command by Attributes
        private void HelloWorld2()
        {
            Debug.Log("World to Hell");
        }
    }
}