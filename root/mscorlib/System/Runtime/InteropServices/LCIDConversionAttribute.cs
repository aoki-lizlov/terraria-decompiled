using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006BD RID: 1725
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class LCIDConversionAttribute : Attribute
	{
		// Token: 0x06004005 RID: 16389 RVA: 0x000E0A0E File Offset: 0x000DEC0E
		public LCIDConversionAttribute(int lcid)
		{
			this._val = lcid;
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x000E0A1D File Offset: 0x000DEC1D
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029BE RID: 10686
		internal int _val;
	}
}
