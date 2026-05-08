using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006C0 RID: 1728
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ProgIdAttribute : Attribute
	{
		// Token: 0x06004009 RID: 16393 RVA: 0x000E0A25 File Offset: 0x000DEC25
		public ProgIdAttribute(string progId)
		{
			this._val = progId;
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x000E0A34 File Offset: 0x000DEC34
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029BF RID: 10687
		internal string _val;
	}
}
