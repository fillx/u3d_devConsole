namespace DeveloperConsole
{
#pragma warning disable 0649
    using System;
    using DeveloperConsole.Backend;
    using DeveloperConsole.GUI;
    using UnityEngine;

    public class Console : MonoBehaviour
    {

        public static event System.Action<bool> OnStateChanged = delegate { };


        private static Console _instance;
        public static Console instance
        {
            get
            {
                if (!_instance)
                    _instance = GameObject.FindObjectOfType<Console>();
                return _instance;
            }
        }

        [SerializeField]
        internal ConsoleGui.Options consoleOptions;

        private GameObject m_consoleRoot;
        private ConsoleGui m_gui;
        private ConsoleBackend m_backend;


        private void Awake()
        {
            _instance = this;
            m_backend = new ConsoleBackend(consoleOptions.LogHandler, consoleOptions);
            m_gui = new ConsoleGui(m_backend, consoleOptions);
            ConsoleGui.OnStateChanged += x => OnStateChanged(x);
        }

        public static void AddCommand(string name, object owner, Action<string[]> callback, string description = "")
        {
            if (instance != null && instance.m_backend != null)
                instance.m_backend.RegisterCommand(name, owner, callback,description);
        }

        public static void RemoveCommand(string name, object owner)
        {
            if (instance != null && instance.m_backend != null)
                instance.m_backend.RemoveCommandIfExists(name, owner);
        }
        public static void AddVariable<T>(string name, Action<T> setter, object owner, string description = "")
        {
            if (instance != null && instance.m_backend != null)
                instance.m_backend.RegisterVariable<T>(setter, owner, name, description);
        }

        public static void RemoveVariable<T>(string name, object owner)
        {
            if (instance != null && instance.m_backend != null)
                instance.m_backend.UnregisterVariable<T>(name, owner);
        }

        /// <summary>
        /// Directly execute command
        /// </summary>
        /// <param name="line"></param>
        public static void ExecuteLine(string line)
        {
            if (instance != null && instance.m_backend != null)
                instance.m_backend.ExecuteLine(line);
        }


        /// <summary>
        /// Supports rich text.
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLine(string message)
        {
            if (instance != null && instance.m_backend != null)
                instance.m_backend.WriteLine(message);
        }

        private void Update()
        {
            m_gui.Update();
        }

        private void OnGUI()
        {
            m_gui.OnGUI();
        }


    }
}