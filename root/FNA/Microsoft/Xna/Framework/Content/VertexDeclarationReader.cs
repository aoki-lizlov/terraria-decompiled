using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200013D RID: 317
	internal class VertexDeclarationReader : ContentTypeReader<VertexDeclaration>
	{
		// Token: 0x06001791 RID: 6033 RVA: 0x0003A7F4 File Offset: 0x000389F4
		protected internal override VertexDeclaration Read(ContentReader reader, VertexDeclaration existingInstance)
		{
			int num = reader.ReadInt32();
			int num2 = reader.ReadInt32();
			VertexElement[] array = new VertexElement[num2];
			for (int i = 0; i < num2; i++)
			{
				int num3 = reader.ReadInt32();
				VertexElementFormat vertexElementFormat = (VertexElementFormat)reader.ReadInt32();
				VertexElementUsage vertexElementUsage = (VertexElementUsage)reader.ReadInt32();
				int num4 = reader.ReadInt32();
				array[i] = new VertexElement(num3, vertexElementFormat, vertexElementUsage, num4);
			}
			return new VertexDeclaration(num, array);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0003A85D File Offset: 0x00038A5D
		public VertexDeclarationReader()
		{
		}
	}
}
