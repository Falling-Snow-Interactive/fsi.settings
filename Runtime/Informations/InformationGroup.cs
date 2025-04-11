using System;
using System.Collections.Generic;
using UnityEngine;

namespace fsi.settings.Informations
{
    [Serializable]
    public abstract class InformationGroup<TInfo, T>
        where TInfo : Information<T>
    {
        [SerializeField]
        private List<TInfo> information;
        public List<TInfo> Information => information;

        private Dictionary<T, TInfo> informationLookup;

        public virtual bool TryGetInformation(T type, out TInfo information)
        {
            informationLookup ??= BuildLookup();
            return informationLookup.TryGetValue(type, out information);
        }

        private Dictionary<T, TInfo> BuildLookup()
        {
            informationLookup = new Dictionary<T, TInfo>();
            foreach (TInfo info in this.information)
            {
                informationLookup[info.Type] = info;
            }

            return informationLookup;
        }
        
        public void CreateLookup()
        {
            informationLookup = BuildLookup();
        }
    }
}