using System;
using UnityEngine;

namespace fsi.settings.Globals
{
	[Serializable]
	public abstract class GlobalVariable<T>
	{
		[SerializeField]
		private bool useOverride = false;

		[SerializeField]
		private T overrideValue;
		public T Value => useOverride ? overrideValue : GetGlobalValue();
		
		protected GlobalVariable(T value)
		{
			overrideValue = value;
		}

		protected abstract T GetGlobalValue();
	}
}