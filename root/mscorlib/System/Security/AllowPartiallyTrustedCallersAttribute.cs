using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x020003A0 RID: 928
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	public sealed class AllowPartiallyTrustedCallersAttribute : Attribute
	{
		// Token: 0x06002819 RID: 10265 RVA: 0x00002050 File Offset: 0x00000250
		public AllowPartiallyTrustedCallersAttribute()
		{
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600281A RID: 10266 RVA: 0x00092BA1 File Offset: 0x00090DA1
		// (set) Token: 0x0600281B RID: 10267 RVA: 0x00092BA9 File Offset: 0x00090DA9
		public PartialTrustVisibilityLevel PartialTrustVisibilityLevel
		{
			get
			{
				return this._visibilityLevel;
			}
			set
			{
				this._visibilityLevel = value;
			}
		}

		// Token: 0x04001D65 RID: 7525
		private PartialTrustVisibilityLevel _visibilityLevel;
	}
}
