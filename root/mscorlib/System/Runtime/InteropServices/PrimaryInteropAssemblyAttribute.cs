using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006DB RID: 1755
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class PrimaryInteropAssemblyAttribute : Attribute
	{
		// Token: 0x06004046 RID: 16454 RVA: 0x000E0F7A File Offset: 0x000DF17A
		public PrimaryInteropAssemblyAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x000E0F90 File Offset: 0x000DF190
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06004048 RID: 16456 RVA: 0x000E0F98 File Offset: 0x000DF198
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x04002A64 RID: 10852
		internal int _major;

		// Token: 0x04002A65 RID: 10853
		internal int _minor;
	}
}
