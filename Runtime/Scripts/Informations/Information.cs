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
        
        [SerializeField]
        private T type;
        public T Type => type;
        
        public void OnBeforeSerialize()
        {
            name = type.ToString();
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}