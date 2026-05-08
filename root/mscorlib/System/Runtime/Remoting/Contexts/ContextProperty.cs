using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000561 RID: 1377
	[ComVisible(true)]
	public class ContextProperty
	{
		// Token: 0x06003758 RID: 14168 RVA: 0x000C8509 File Offset: 0x000C6709
		private ContextProperty(string name, object prop)
		{
			this.name = name;
			this.prop = prop;
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000C851F File Offset: 0x000C671F
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600375A RID: 14170 RVA: 0x000C8527 File Offset: 0x000C6727
		public virtual object Property
		{
			get
			{
				return this.prop;
			}
		}

		// Token: 0x04002537 RID: 9527
		private string name;

		// Token: 0x04002538 RID: 9528
		private object prop;
	}
}
