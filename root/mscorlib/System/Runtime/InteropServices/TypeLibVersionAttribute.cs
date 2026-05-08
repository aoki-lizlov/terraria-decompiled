using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006DE RID: 1758
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibVersionAttribute : Attribute
	{
		// Token: 0x0600404E RID: 16462 RVA: 0x000E0FDD File Offset: 0x000DF1DD
		public TypeLibVersionAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x0600404F RID: 16463 RVA: 0x000E0FF3 File Offset: 0x000DF1F3
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06004050 RID: 16464 RVA: 0x000E0FFB File Offset: 0x000DF1FB
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x04002A69 RID: 10857
		internal int _major;

		// Token: 0x04002A6A RID: 10858
		internal int _minor;
	}
}
