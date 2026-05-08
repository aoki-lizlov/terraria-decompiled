using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000123 RID: 291
	internal class NullableReader<T> : ContentTypeReader<T?> where T : struct
	{
		// Token: 0x06001755 RID: 5973 RVA: 0x00039988 File Offset: 0x00037B88
		internal NullableReader()
		{
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00039990 File Offset: 0x00037B90
		protected internal override void Initialize(ContentTypeReaderManager manager)
		{
			Type typeFromHandle = typeof(T);
			this.elementReader = manager.GetTypeReader(typeFromHandle);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x000399B8 File Offset: 0x00037BB8
		protected internal override T? Read(ContentReader input, T? existingInstance)
		{
			if (input.ReadBoolean())
			{
				return new T?(input.ReadObject<T>(this.elementReader));
			}
			return null;
		}

		// Token: 0x04000ABA RID: 2746
		private ContentTypeReader elementReader;
	}
}
