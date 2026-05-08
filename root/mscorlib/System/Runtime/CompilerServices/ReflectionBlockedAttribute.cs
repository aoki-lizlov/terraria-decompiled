using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007E8 RID: 2024
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	internal class ReflectionBlockedAttribute : Attribute
	{
		// Token: 0x060045EB RID: 17899 RVA: 0x00002050 File Offset: 0x00000250
		public ReflectionBlockedAttribute()
		{
		}
	}
}
