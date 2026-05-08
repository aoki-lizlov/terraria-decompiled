using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000DF RID: 223
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VertexPositionTexture : IVertexType
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x00034C12 File Offset: 0x00032E12
		VertexDeclaration IVertexType.VertexDeclaration
		{
			get
			{
				return VertexPositionTexture.VertexDeclaration;
			}
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00034C19 File Offset: 0x00032E19
		static VertexPositionTexture()
		{
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00034C4C File Offset: 0x00032E4C
		public VertexPositionTexture(Vector3 position, Vector2 textureCoordinate)
		{
			this.Position = position;
			this.TextureCoordinate = textureCoordinate;
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x000136EB File Offset: 0x000118EB
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00034C5C File Offset: 0x00032E5C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{{Position:",
				this.Position.ToString(),
				" TextureCoordinate:",
				this.TextureCoordinate.ToString(),
				"}}"
			});
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00034CB4 File Offset: 0x00032EB4
		public static bool operator ==(VertexPositionTexture left, VertexPositionTexture right)
		{
			return left.Position == right.Position && left.TextureCoordinate == right.TextureCoordinate;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00034CDC File Offset: 0x00032EDC
		public static bool operator !=(VertexPositionTexture left, VertexPositionTexture right)
		{
			return !(left == right);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00034CE8 File Offset: 0x00032EE8
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != base.GetType()) && this == (VertexPositionTexture)obj;
		}

		// Token: 0x04000A84 RID: 2692
		public Vector3 Position;

		// Token: 0x04000A85 RID: 2693
		public Vector2 TextureCoordinate;

		// Token: 0x04000A86 RID: 2694
		public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
		});
	}
}
