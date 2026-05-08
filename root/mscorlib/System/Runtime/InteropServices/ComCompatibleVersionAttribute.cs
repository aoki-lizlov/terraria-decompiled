using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006DF RID: 1759
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComCompatibleVersionAttribute : Attribute
	{
		// Token: 0x06004051 RID: 16465 RVA: 0x000E1003 File Offset: 0x000DF203
		public ComCompatibleVersionAttribute(int major, int minor, int build, int revision)
		{
			this._major = major;
			this._minor = minor;
			this._build = build;
			this._revision = revision;
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06004052 RID: 16466 RVA: 0x000E1028 File Offset: 0x000DF228
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06004053 RID: 16467 RVA: 0x000E1030 File Offset: 0x000DF230
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06004054 RID: 16468 RVA: 0x000E1038 File Offset: 0x000DF238
		public int BuildNumber
		{
			get
			{
				return this._build;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06004055 RID: 16469 RVA: 0x000E1040 File Offset: 0x000DF240
		public int RevisionNumber
		{
			get
			{
				return this._revision;
			}
		}

		// Token: 0x04002A6B RID: 10859
		internal int _major;

		// Token: 0x04002A6C RID: 10860
		internal int _minor;

		// Token: 0x04002A6D RID: 10861
		internal int _build;

		// Token: 0x04002A6E RID: 10862
		internal int _revision;
	}
}
