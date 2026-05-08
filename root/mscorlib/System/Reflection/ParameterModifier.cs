using System;

namespace System.Reflection
{
	// Token: 0x02000887 RID: 2183
	public readonly struct ParameterModifier
	{
		// Token: 0x06004949 RID: 18761 RVA: 0x000EEE07 File Offset: 0x000ED007
		public ParameterModifier(int parameterCount)
		{
			if (parameterCount <= 0)
			{
				throw new ArgumentException("Must specify one or more parameters.");
			}
			this._byRef = new bool[parameterCount];
		}

		// Token: 0x17000B92 RID: 2962
		public bool this[int index]
		{
			get
			{
				return this._byRef[index];
			}
			set
			{
				this._byRef[index] = value;
			}
		}

		// Token: 0x04002E8B RID: 11915
		private readonly bool[] _byRef;
	}
}
