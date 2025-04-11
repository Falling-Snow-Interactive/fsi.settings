using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace fsi.settings.Logging
{
    [Serializable]
    public class Logger
    {
        [SerializeField]
        private string prefix;
        
        [SerializeField]
        private bool enableLogs = false;
        
        [SerializeField]
        private bool enableWarnings = false;
        
        [SerializeField]
        private bool enableErrors = false;

        public Logger(string prefix)
        {
            this.prefix = prefix;
        }
        
        public void Log(string message, GameObject gameObject = null)
        {
            if (enableLogs)
            {
                Debug.Log($"{prefix} | {message}", gameObject);
            }
        }

        public void Warning(string message, GameObject gameObject = null)
        {
            if (enableWarnings)
            {
                Debug.LogWarning($"{prefix} | {message}", gameObject);
            }
        }

        public void Error(string message, GameObject gameObject = null)
        {
            if (enableErrors)
            {
                Debug.LogError($"{prefix} | {message}", gameObject);
            }
        }
    }
}
