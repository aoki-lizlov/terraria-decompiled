using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006AF RID: 1711
	public sealed class UnknownWrapper
	{
		// Token: 0x06003FE3 RID: 16355 RVA: 0x000E0840 File Offset: 0x000DEA40
		public UnknownWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06003FE4 RID: 16356 RVA: 0x000E084F File Offset: 0x000DEA4F
		public object WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x040029A3 RID: 10659
		private object m_WrappedObject;
	}
}
