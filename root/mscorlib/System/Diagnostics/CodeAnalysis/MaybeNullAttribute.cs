using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A5D RID: 2653
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class MaybeNullAttribute : Attribute
	{
		// Token: 0x06006164 RID: 24932 RVA: 0x00002050 File Offset: 0x00000250
		public MaybeNullAttribute()
		{
		}
	}
}
