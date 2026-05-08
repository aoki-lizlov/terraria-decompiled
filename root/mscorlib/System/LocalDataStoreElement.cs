using System;

namespace System
{
	// Token: 0x020001BC RID: 444
	internal sealed class LocalDataStoreElement
	{
		// Token: 0x060014B4 RID: 5300 RVA: 0x000528A0 File Offset: 0x00050AA0
		public LocalDataStoreElement(long cookie)
		{
			this.m_cookie = cookie;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x000528AF File Offset: 0x00050AAF
		// (set) Token: 0x060014B6 RID: 5302 RVA: 0x000528B7 File Offset: 0x00050AB7
		public object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x000528C0 File Offset: 0x00050AC0
		public long Cookie
		{
			get
			{
				return this.m_cookie;
			}
		}

		// Token: 0x040013CD RID: 5069
		private object m_value;

		// Token: 0x040013CE RID: 5070
		private long m_cookie;
	}
}
