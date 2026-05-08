using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000DD RID: 221
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VertexPositionColorTexture : IVertexType
	{
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0003494B File Offset: 0x00032B4B
		VertexDeclaration IVertexType.VertexDeclaration
		{
			get
			{
				return VertexPositionColorTexture.VertexDeclaration;
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00034954 File Offset: 0x00032B54
		static VertexPositionColorTexture()
		{
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x000349A3 File Offset: 0x00032BA3
		public VertexPositionColorTexture(Vector3 position, Color color, Vector2 textureCoordinate)
		{
			this.Position = position;
			this.Color = color;
			this.TextureCoordinate = textureCoordinate;
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x000136EB File Offset: 0x000118EB
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x000349BC File Offset: 0x00032BBC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{{Position:",
				this.Position.ToString(),
				" Color:",
				this.Color.ToString(),
				" TextureCoordinate:",
				this.TextureCoordinate.ToString(),
				"}}"
			});
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00034A30 File Offset: 0x00032C30
		public static bool operator ==(VertexPositionColorTexture left, VertexPositionColorTexture right)
		{
			return left.Position == right.Position && left.Color == right.Color && left.TextureCoordinate == right.TextureCoordinate;
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00034A6B File Offset: 0x00032C6B
		public static bool operator !=(VertexPositionColorTexture left, VertexPositionColorTexture right)
		{
			return !(left == right);
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00034A77 File Offset: 0x00032C77
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != base.GetType()) && this == (VertexPositionColorTexture)obj;
		}

		// Token: 0x04000A7C RID: 2684
		public Vector3 Position;

		// Token: 0x04000A7D RID: 2685
		public Color Color;

		// Token: 0x04000A7E RID: 2686
		public Vector2 TextureCoordinate;

		// Token: 0x04000A7F RID: 2687
		public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.Color, VertexElementUsage.Color, 0),
			new VertexElement(16, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
		});
	}
}
