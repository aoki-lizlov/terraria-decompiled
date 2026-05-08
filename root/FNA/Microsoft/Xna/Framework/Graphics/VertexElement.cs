using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000D9 RID: 217
	public struct VertexElement
	{
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x000346AE File Offset: 0x000328AE
		// (set) Token: 0x06001577 RID: 5495 RVA: 0x000346B6 File Offset: 0x000328B6
		public int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x000346BF File Offset: 0x000328BF
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x000346C7 File Offset: 0x000328C7
		public VertexElementFormat VertexElementFormat
		{
			get
			{
				return this.vertexElementFormat;
			}
			set
			{
				this.vertexElementFormat = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x000346D0 File Offset: 0x000328D0
		// (set) Token: 0x0600157B RID: 5499 RVA: 0x000346D8 File Offset: 0x000328D8
		public VertexElementUsage VertexElementUsage
		{
			get
			{
				return this.vertexElementUsage;
			}
			set
			{
				this.vertexElementUsage = value;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x000346E1 File Offset: 0x000328E1
		// (set) Token: 0x0600157D RID: 5501 RVA: 0x000346E9 File Offset: 0x000328E9
		public int UsageIndex
		{
			get
			{
				return this.usageIndex;
			}
			set
			{
				this.usageIndex = value;
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x000346F2 File Offset: 0x000328F2
		public VertexElement(int offset, VertexElementFormat elementFormat, VertexElementUsage elementUsage, int usageIndex)
		{
			this = default(VertexElement);
			this.Offset = offset;
			this.UsageIndex = usageIndex;
			this.VertexElementFormat = elementFormat;
			this.VertexElementUsage = elementUsage;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x000136EB File Offset: 0x000118EB
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00034718 File Offset: 0x00032918
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{{Offset:",
				this.Offset.ToString(),
				" Format:",
				this.VertexElementFormat.ToString(),
				" Usage:",
				this.VertexElementUsage.ToString(),
				" UsageIndex: ",
				this.UsageIndex.ToString(),
				"}}"
			});
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x000347A9 File Offset: 0x000329A9
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != base.GetType()) && this == (VertexElement)obj;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x000347E0 File Offset: 0x000329E0
		public static bool operator ==(VertexElement left, VertexElement right)
		{
			return left.Offset == right.Offset && left.UsageIndex == right.UsageIndex && left.VertexElementUsage == right.VertexElementUsage && left.VertexElementFormat == right.VertexElementFormat;
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0003482F File Offset: 0x00032A2F
		public static bool operator !=(VertexElement left, VertexElement right)
		{
			return !(left == right);
		}

		// Token: 0x04000A5A RID: 2650
		private int offset;

		// Token: 0x04000A5B RID: 2651
		private VertexElementFormat vertexElementFormat;

		// Token: 0x04000A5C RID: 2652
		private VertexElementUsage vertexElementUsage;

		// Token: 0x04000A5D RID: 2653
		private int usageIndex;
	}
}
