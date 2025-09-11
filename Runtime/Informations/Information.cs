using System;
using UnityEngine;

namespace fsi.settings.Informations
{
    [Serializable]
    public abstract class Information<T> : ISerializationCallbackReceiver 
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        public abstract T Type { get; }
        
        public void OnBeforeSerialize()
        {
            name = ToString();
        }

        public void OnAfterDeserialize() { }

        public override string ToString()
        {
            string s = Type != null ? Type.ToString() : "not set";
            return s;
        }
    }
}