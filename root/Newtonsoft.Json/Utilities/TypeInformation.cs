using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200005E RID: 94
	internal class TypeInformation
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x000126D3 File Offset: 0x000108D3
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x000126DB File Offset: 0x000108DB
		public Type Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Type>k__BackingField = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x000126E4 File Offset: 0x000108E4
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x000126EC File Offset: 0x000108EC
		public PrimitiveTypeCode TypeCode
		{
			[CompilerGenerated]
			get
			{
				return this.<TypeCode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TypeCode>k__BackingField = value;
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00008020 File Offset: 0x00006220
		public TypeInformation()
		{
		}

		// Token: 0x0400023D RID: 573
		[CompilerGenerated]
		private Type <Type>k__BackingField;

		// Token: 0x0400023E RID: 574
		[CompilerGenerated]
		private PrimitiveTypeCode <TypeCode>k__BackingField;
	}
}
