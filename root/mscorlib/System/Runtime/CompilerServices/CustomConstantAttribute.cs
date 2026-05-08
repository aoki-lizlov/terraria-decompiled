using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007BB RID: 1979
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[Serializable]
	public abstract class CustomConstantAttribute : Attribute
	{
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06004585 RID: 17797
		public abstract object Value { get; }

		// Token: 0x06004586 RID: 17798 RVA: 0x00002050 File Offset: 0x00000250
		protected CustomConstantAttribute()
		{
		}
	}
}
