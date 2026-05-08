using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000144 RID: 324
	public abstract class ContentTypeReader
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000136EB File Offset: 0x000118EB
		public virtual bool CanDeserializeIntoExistingObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x0003AAB2 File Offset: 0x00038CB2
		public Type TargetType
		{
			get
			{
				return this.targetType;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000136EB File Offset: 0x000118EB
		public virtual int TypeVersion
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0003AABA File Offset: 0x00038CBA
		protected ContentTypeReader(Type targetType)
		{
			this.targetType = targetType;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00009E6B File Offset: 0x0000806B
		protected internal virtual void Initialize(ContentTypeReaderManager manager)
		{
		}

		// Token: 0x060017B5 RID: 6069
		protected internal abstract object Read(ContentReader input, object existingInstance);

		// Token: 0x04000AC9 RID: 2761
		private Type targetType;
	}
}
