using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000DC RID: 220
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VertexPositionColor : IVertexType
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x0003483B File Offset: 0x00032A3B
		VertexDeclaration IVertexType.VertexDeclaration
		{
			get
			{
				return VertexPositionColor.VertexDeclaration;
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00034842 File Offset: 0x00032A42
		static VertexPositionColor()
		{
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00034875 File Offset: 0x00032A75
		public VertexPositionColor(Vector3 position, Color color)
		{
			this.Position = position;
			this.Color = color;
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x000136EB File Offset: 0x000118EB
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00034888 File Offset: 0x00032A88
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{{Position:",
				this.Position.ToString(),
				" Color:",
				this.Color.ToString(),
				"}}"
			});
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000348E0 File Offset: 0x00032AE0
		public static bool operator ==(VertexPositionColor left, VertexPositionColor right)
		{
			return left.Color == right.Color && left.Position == right.Position;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00034908 File Offset: 0x00032B08
		public static bool operator !=(VertexPositionColor left, VertexPositionColor right)
		{
			return !(left == right);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00034914 File Offset: 0x00032B14
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != base.GetType()) && this == (VertexPositionColor)obj;
		}

		// Token: 0x04000A79 RID: 2681
		public Vector3 Position;

		// Token: 0x04000A7A RID: 2682
		public Color Color;

		// Token: 0x04000A7B RID: 2683
		public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(12, VertexElementFormat.Color, VertexElementUsage.Color, 0)
		});
	}
}
