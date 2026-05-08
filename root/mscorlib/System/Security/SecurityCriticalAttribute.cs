using System;

namespace System.Security
{
	// Token: 0x020003A3 RID: 931
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class SecurityCriticalAttribute : Attribute
	{
		// Token: 0x0600281C RID: 10268 RVA: 0x00002050 File Offset: 0x00000250
		public SecurityCriticalAttribute()
		{
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x00092BB2 File Offset: 0x00090DB2
		public SecurityCriticalAttribute(SecurityCriticalScope scope)
		{
			this._val = scope;
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600281E RID: 10270 RVA: 0x00092BC1 File Offset: 0x00090DC1
		[Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
		public SecurityCriticalScope Scope
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001D6C RID: 7532
		private SecurityCriticalScope _val;
	}
}
