using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(384, AllowMultiple = false)]
	public class JsonExtensionDataAttribute : Attribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020A0 File Offset: 0x000002A0
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020A8 File Offset: 0x000002A8
		public bool WriteData
		{
			[CompilerGenerated]
			get
			{
				return this.<WriteData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WriteData>k__BackingField = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020B1 File Offset: 0x000002B1
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020B9 File Offset: 0x000002B9
		public bool ReadData
		{
			[CompilerGenerated]
			get
			{
				return this.<ReadData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReadData>k__BackingField = value;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020C2 File Offset: 0x000002C2
		public JsonExtensionDataAttribute()
		{
			this.WriteData = true;
			this.ReadData = true;
		}

		// Token: 0x0400001A RID: 26
		[CompilerGenerated]
		private bool <WriteData>k__BackingField;

		// Token: 0x0400001B RID: 27
		[CompilerGenerated]
		private bool <ReadData>k__BackingField;
	}
}
