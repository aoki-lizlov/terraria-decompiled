using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000140 RID: 320
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ContentSerializerCollectionItemNameAttribute : Attribute
	{
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x0003AA52 File Offset: 0x00038C52
		// (set) Token: 0x060017A7 RID: 6055 RVA: 0x0003AA5A File Offset: 0x00038C5A
		public string CollectionItemName
		{
			[CompilerGenerated]
			get
			{
				return this.<CollectionItemName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CollectionItemName>k__BackingField = value;
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0003AA63 File Offset: 0x00038C63
		public ContentSerializerCollectionItemNameAttribute(string collectionItemName)
		{
			this.CollectionItemName = collectionItemName;
		}

		// Token: 0x04000AC6 RID: 2758
		[CompilerGenerated]
		private string <CollectionItemName>k__BackingField;
	}
}
