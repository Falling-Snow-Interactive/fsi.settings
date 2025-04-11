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
            if (Type != null)
            {
                name = Type.ToString();
            }
            else
            {
                name = "not set";
            }
        }

        public void OnAfterDeserialize() { }
    }
}