using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006C1 RID: 1729
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class ImportedFromTypeLibAttribute : Attribute
	{
		// Token: 0x0600400B RID: 16395 RVA: 0x000E0A3C File Offset: 0x000DEC3C
		public ImportedFromTypeLibAttribute(string tlbFile)
		{
			this._val = tlbFile;
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x000E0A4B File Offset: 0x000DEC4B
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029C0 RID: 10688
		internal string _val;
	}
}
