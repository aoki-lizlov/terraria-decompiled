using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006DA RID: 1754
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class AutomationProxyAttribute : Attribute
	{
		// Token: 0x06004044 RID: 16452 RVA: 0x000E0F63 File Offset: 0x000DF163
		public AutomationProxyAttribute(bool val)
		{
			this._val = val;
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06004045 RID: 16453 RVA: 0x000E0F72 File Offset: 0x000DF172
		public bool Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A63 RID: 10851
		internal bool _val;
	}
}
