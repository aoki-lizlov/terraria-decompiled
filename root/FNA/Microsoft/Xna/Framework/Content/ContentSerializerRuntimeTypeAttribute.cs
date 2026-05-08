using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000142 RID: 322
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public sealed class ContentSerializerRuntimeTypeAttribute : Attribute
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0003AA72 File Offset: 0x00038C72
		// (set) Token: 0x060017AB RID: 6059 RVA: 0x0003AA7A File Offset: 0x00038C7A
		public string RuntimeType
		{
			[CompilerGenerated]
			get
			{
				return this.<RuntimeType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RuntimeType>k__BackingField = value;
			}
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0003AA83 File Offset: 0x00038C83
		public ContentSerializerRuntimeTypeAttribute(string runtimeType)
		{
			this.RuntimeType = runtimeType;
		}

		// Token: 0x04000AC7 RID: 2759
		[CompilerGenerated]
		private string <RuntimeType>k__BackingField;
	}
}
