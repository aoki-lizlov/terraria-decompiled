using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006B5 RID: 1717
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = false)]
	[ComVisible(true)]
	public sealed class DispIdAttribute : Attribute
	{
		// Token: 0x06003FF7 RID: 16375 RVA: 0x000E097F File Offset: 0x000DEB7F
		public DispIdAttribute(int dispId)
		{
			this._val = dispId;
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x000E098E File Offset: 0x000DEB8E
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029AF RID: 10671
		internal int _val;
	}
}
