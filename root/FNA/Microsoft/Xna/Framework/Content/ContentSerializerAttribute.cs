using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200013F RID: 319
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public sealed class ContentSerializerAttribute : Attribute
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x0003A95F File Offset: 0x00038B5F
		// (set) Token: 0x06001798 RID: 6040 RVA: 0x0003A967 File Offset: 0x00038B67
		public bool AllowNull
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowNull>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AllowNull>k__BackingField = value;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x0003A970 File Offset: 0x00038B70
		// (set) Token: 0x0600179A RID: 6042 RVA: 0x0003A98B File Offset: 0x00038B8B
		public string CollectionItemName
		{
			get
			{
				if (string.IsNullOrEmpty(this.collectionItemName))
				{
					return "Item";
				}
				return this.collectionItemName;
			}
			set
			{
				this.collectionItemName = value;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x0003A994 File Offset: 0x00038B94
		// (set) Token: 0x0600179C RID: 6044 RVA: 0x0003A99C File Offset: 0x00038B9C
		public string ElementName
		{
			[CompilerGenerated]
			get
			{
				return this.<ElementName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ElementName>k__BackingField = value;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x0003A9A5 File Offset: 0x00038BA5
		// (set) Token: 0x0600179E RID: 6046 RVA: 0x0003A9AD File Offset: 0x00038BAD
		public bool FlattenContent
		{
			[CompilerGenerated]
			get
			{
				return this.<FlattenContent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<FlattenContent>k__BackingField = value;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x0003A9B6 File Offset: 0x00038BB6
		public bool HasCollectionItemName
		{
			get
			{
				return !string.IsNullOrEmpty(this.collectionItemName);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0003A9C6 File Offset: 0x00038BC6
		// (set) Token: 0x060017A1 RID: 6049 RVA: 0x0003A9CE File Offset: 0x00038BCE
		public bool Optional
		{
			[CompilerGenerated]
			get
			{
				return this.<Optional>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Optional>k__BackingField = value;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0003A9D7 File Offset: 0x00038BD7
		// (set) Token: 0x060017A3 RID: 6051 RVA: 0x0003A9DF File Offset: 0x00038BDF
		public bool SharedResource
		{
			[CompilerGenerated]
			get
			{
				return this.<SharedResource>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SharedResource>k__BackingField = value;
			}
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0003A9E8 File Offset: 0x00038BE8
		public ContentSerializerAttribute()
		{
			this.AllowNull = true;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0003A9F8 File Offset: 0x00038BF8
		public ContentSerializerAttribute Clone()
		{
			return new ContentSerializerAttribute
			{
				AllowNull = this.AllowNull,
				collectionItemName = this.collectionItemName,
				ElementName = this.ElementName,
				FlattenContent = this.FlattenContent,
				Optional = this.Optional,
				SharedResource = this.SharedResource
			};
		}

		// Token: 0x04000AC0 RID: 2752
		[CompilerGenerated]
		private bool <AllowNull>k__BackingField;

		// Token: 0x04000AC1 RID: 2753
		[CompilerGenerated]
		private string <ElementName>k__BackingField;

		// Token: 0x04000AC2 RID: 2754
		[CompilerGenerated]
		private bool <FlattenContent>k__BackingField;

		// Token: 0x04000AC3 RID: 2755
		[CompilerGenerated]
		private bool <Optional>k__BackingField;

		// Token: 0x04000AC4 RID: 2756
		[CompilerGenerated]
		private bool <SharedResource>k__BackingField;

		// Token: 0x04000AC5 RID: 2757
		private string collectionItemName;
	}
}
