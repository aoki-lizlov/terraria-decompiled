using System;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005D7 RID: 1495
	[Serializable]
	internal class CallContextSecurityData : ICloneable
	{
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06003A06 RID: 14854 RVA: 0x000CC8B8 File Offset: 0x000CAAB8
		// (set) Token: 0x06003A07 RID: 14855 RVA: 0x000CC8C0 File Offset: 0x000CAAC0
		internal IPrincipal Principal
		{
			get
			{
				return this._principal;
			}
			set
			{
				this._principal = value;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06003A08 RID: 14856 RVA: 0x000CC8C9 File Offset: 0x000CAAC9
		internal bool HasInfo
		{
			get
			{
				return this._principal != null;
			}
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x000CC8D4 File Offset: 0x000CAAD4
		public object Clone()
		{
			return new CallContextSecurityData
			{
				_principal = this._principal
			};
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x000025BE File Offset: 0x000007BE
		public CallContextSecurityData()
		{
		}

		// Token: 0x040025DB RID: 9691
		private IPrincipal _principal;
	}
}
