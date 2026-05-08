using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000DE RID: 222
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VertexPositionNormalTexture : IVertexType
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x00034AAE File Offset: 0x00032CAE
		VertexDeclaration IVertexType.VertexDeclaration
		{
			get
			{
				return VertexPositionNormalTexture.VertexDeclaration;
			}
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00034AB8 File Offset: 0x00032CB8
		static VertexPositionNormalTexture()
		{
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00034B07 File Offset: 0x00032D07
		public VertexPositionNormalTexture(Vector3 position, Vector3 normal, Vector2 textureCoordinate)
		{
			this.Position = position;
			this.Normal = normal;
			this.TextureCoordinate = textureCoordinate;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x000136EB File Offset: 0x000118EB
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00034B20 File Offset: 0x00032D20
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{{Position:",
				this.Position.ToString(),
				" Normal:",
				this.Normal.ToString(),
				" TextureCoordinate:",
				this.TextureCoordinate.ToString(),
				"}}"
			});
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00034B94 File Offset: 0x00032D94
		public static bool operator ==(VertexPositionNormalTexture left, VertexPositionNormalTexture right)
		{
			return left.Position == right.Position && left.Normal == right.Normal && left.TextureCoordinate == right.TextureCoordinate;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00034BCF File Offset: 0x00032DCF
		public static bool operator !=(VertexPositionNormalTexture left, VertexPositionNormalTexture right)
		{
			return !(left == right);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00034BDB File Offset: 0x00032DDB
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != base.GetType()) && this == (VertexPositionNormalTexture)obj;
		}

		// Token: 0x04000A80 RID: 2688
		public Vector3 Position;

		// Token: 0x04000A81 RID: 2689
		public Vector3 Normal;

		// Token: 0x04000A82 RID: 2690
		public Vector2 TextureCoordinate;

		// Token: 0x04000A83 RID: 2691
		public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
			new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
		});
	}
}
