using System;

namespace System.Security
{
	// Token: 0x020003A4 RID: 932
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[Obsolete("SecurityTreatAsSafe is only used for .NET 2.0 transparency compatibility.  Please use the SecuritySafeCriticalAttribute instead.")]
	public sealed class SecurityTreatAsSafeAttribute : Attribute
	{
		// Token: 0x0600281F RID: 10271 RVA: 0x00002050 File Offset: 0x00000250
		public SecurityTreatAsSafeAttribute()
		{
		}
	}
}
