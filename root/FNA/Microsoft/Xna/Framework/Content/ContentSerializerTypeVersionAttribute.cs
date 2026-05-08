using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000143 RID: 323
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public sealed class ContentSerializerTypeVersionAttribute : Attribute
	{
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x0003AA92 File Offset: 0x00038C92
		// (set) Token: 0x060017AE RID: 6062 RVA: 0x0003AA9A File Offset: 0x00038C9A
		public int TypeVersion
		{
			[CompilerGenerated]
			get
			{
				return this.<TypeVersion>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TypeVersion>k__BackingField = value;
			}
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0003AAA3 File Offset: 0x00038CA3
		public ContentSerializerTypeVersionAttribute(int typeVersion)
		{
			this.TypeVersion = typeVersion;
		}

		// Token: 0x04000AC8 RID: 2760
		[CompilerGenerated]
		private int <TypeVersion>k__BackingField;
	}
}
