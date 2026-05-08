using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A5C RID: 2652
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	public sealed class DisallowNullAttribute : Attribute
	{
		// Token: 0x06006163 RID: 24931 RVA: 0x00002050 File Offset: 0x00000250
		public DisallowNullAttribute()
		{
		}
	}
}
