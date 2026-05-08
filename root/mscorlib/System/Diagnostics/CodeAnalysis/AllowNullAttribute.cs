using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A5B RID: 2651
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	public sealed class AllowNullAttribute : Attribute
	{
		// Token: 0x06006162 RID: 24930 RVA: 0x00002050 File Offset: 0x00000250
		public AllowNullAttribute()
		{
		}
	}
}
