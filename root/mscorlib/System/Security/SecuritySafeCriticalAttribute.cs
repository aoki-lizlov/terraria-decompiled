using System;

namespace System.Security
{
	// Token: 0x020003A5 RID: 933
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class SecuritySafeCriticalAttribute : Attribute
	{
		// Token: 0x06002820 RID: 10272 RVA: 0x00002050 File Offset: 0x00000250
		public SecuritySafeCriticalAttribute()
		{
		}
	}
}
