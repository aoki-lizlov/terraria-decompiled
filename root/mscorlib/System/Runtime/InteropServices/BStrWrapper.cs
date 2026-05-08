using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006A2 RID: 1698
	public sealed class BStrWrapper
	{
		// Token: 0x06003FBD RID: 16317 RVA: 0x000E063B File Offset: 0x000DE83B
		public BStrWrapper(string value)
		{
			this.m_WrappedObject = value;
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x000E064A File Offset: 0x000DE84A
		public BStrWrapper(object value)
		{
			this.m_WrappedObject = (string)value;
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06003FBF RID: 16319 RVA: 0x000E065E File Offset: 0x000DE85E
		public string WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002999 RID: 10649
		private string m_WrappedObject;
	}
}
