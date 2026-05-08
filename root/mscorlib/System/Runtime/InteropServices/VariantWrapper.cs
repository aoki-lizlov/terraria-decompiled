using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006B0 RID: 1712
	public sealed class VariantWrapper
	{
		// Token: 0x06003FE5 RID: 16357 RVA: 0x000E0857 File Offset: 0x000DEA57
		public VariantWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x000E0866 File Offset: 0x000DEA66
		public object WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x040029A4 RID: 10660
		private object m_WrappedObject;
	}
}
