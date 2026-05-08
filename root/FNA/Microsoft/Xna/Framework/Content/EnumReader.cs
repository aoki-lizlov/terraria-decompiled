using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000119 RID: 281
	internal class EnumReader<T> : ContentTypeReader<T>
	{
		// Token: 0x0600173D RID: 5949 RVA: 0x000393EF File Offset: 0x000375EF
		public EnumReader()
		{
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x000393F8 File Offset: 0x000375F8
		protected internal override void Initialize(ContentTypeReaderManager manager)
		{
			Type underlyingType = Enum.GetUnderlyingType(typeof(T));
			this.elementReader = manager.GetTypeReader(underlyingType);
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00039422 File Offset: 0x00037622
		protected internal override T Read(ContentReader input, T existingInstance)
		{
			return input.ReadRawObject<T>(this.elementReader);
		}

		// Token: 0x04000AB8 RID: 2744
		private ContentTypeReader elementReader;
	}
}
