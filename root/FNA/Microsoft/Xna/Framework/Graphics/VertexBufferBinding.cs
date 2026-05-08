using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000D6 RID: 214
	public struct VertexBufferBinding
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x00034437 File Offset: 0x00032637
		public int InstanceFrequency
		{
			get
			{
				return this.instanceFrequency;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0003443F File Offset: 0x0003263F
		public VertexBuffer VertexBuffer
		{
			get
			{
				return this.vertexBuffer;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x00034447 File Offset: 0x00032647
		public int VertexOffset
		{
			get
			{
				return this.vertexOffset;
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0003444F File Offset: 0x0003264F
		public VertexBufferBinding(VertexBuffer vertexBuffer)
		{
			this.vertexBuffer = vertexBuffer;
			this.vertexOffset = 0;
			this.instanceFrequency = 0;
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00034466 File Offset: 0x00032666
		public VertexBufferBinding(VertexBuffer vertexBuffer, int vertexOffset)
		{
			this.vertexBuffer = vertexBuffer;
			this.vertexOffset = vertexOffset;
			this.instanceFrequency = 0;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0003447D File Offset: 0x0003267D
		public VertexBufferBinding(VertexBuffer vertexBuffer, int vertexOffset, int instanceFrequency)
		{
			this.vertexBuffer = vertexBuffer;
			this.vertexOffset = vertexOffset;
			this.instanceFrequency = instanceFrequency;
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00034494 File Offset: 0x00032694
		public static implicit operator VertexBufferBinding(VertexBuffer buffer)
		{
			return new VertexBufferBinding(buffer);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0003449C File Offset: 0x0003269C
		// Note: this type is marked as 'beforefieldinit'.
		static VertexBufferBinding()
		{
		}

		// Token: 0x04000A51 RID: 2641
		private VertexBuffer vertexBuffer;

		// Token: 0x04000A52 RID: 2642
		private int vertexOffset;

		// Token: 0x04000A53 RID: 2643
		private int instanceFrequency;

		// Token: 0x04000A54 RID: 2644
		internal static readonly VertexBufferBinding None = new VertexBufferBinding(null);
	}
}
