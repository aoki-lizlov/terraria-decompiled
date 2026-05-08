using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200013C RID: 316
	internal class VertexBufferReader : ContentTypeReader<VertexBuffer>
	{
		// Token: 0x0600178F RID: 6031 RVA: 0x0003A7A8 File Offset: 0x000389A8
		protected internal override VertexBuffer Read(ContentReader input, VertexBuffer existingInstance)
		{
			VertexDeclaration vertexDeclaration = input.ReadRawObject<VertexDeclaration>();
			int num = (int)input.ReadUInt32();
			byte[] array = input.ReadBytes(num * vertexDeclaration.VertexStride);
			VertexBuffer vertexBuffer = new VertexBuffer(input.ContentManager.GetGraphicsDevice(), vertexDeclaration, num, BufferUsage.None);
			vertexBuffer.SetData<byte>(array);
			return vertexBuffer;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0003A7EC File Offset: 0x000389EC
		public VertexBufferReader()
		{
		}
	}
}
