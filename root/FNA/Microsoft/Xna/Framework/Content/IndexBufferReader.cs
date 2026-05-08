using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200011C RID: 284
	internal class IndexBufferReader : ContentTypeReader<IndexBuffer>
	{
		// Token: 0x06001744 RID: 5956 RVA: 0x000394CC File Offset: 0x000376CC
		protected internal override IndexBuffer Read(ContentReader input, IndexBuffer existingInstance)
		{
			IndexBuffer indexBuffer = existingInstance;
			bool flag = input.ReadBoolean();
			int num = input.ReadInt32();
			byte[] array = input.ReadBytes(num);
			if (indexBuffer == null)
			{
				if (flag)
				{
					indexBuffer = new IndexBuffer(input.ContentManager.GetGraphicsDevice(), IndexElementSize.SixteenBits, num / 2, BufferUsage.None);
				}
				else
				{
					indexBuffer = new IndexBuffer(input.ContentManager.GetGraphicsDevice(), IndexElementSize.ThirtyTwoBits, num / 4, BufferUsage.None);
				}
			}
			indexBuffer.SetData<byte>(array);
			return indexBuffer;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0003952D File Offset: 0x0003772D
		public IndexBufferReader()
		{
		}
	}
}
