using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A5E RID: 2654
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class NotNullAttribute : Attribute
	{
		// Token: 0x06006165 RID: 24933 RVA: 0x00002050 File Offset: 0x00000250
		public NotNullAttribute()
		{
		}
	}
}
