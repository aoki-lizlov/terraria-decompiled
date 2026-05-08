using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000F2 RID: 242
	public struct Rgba64 : IPackedVector<ulong>, IPackedVector, IEquatable<Rgba64>
	{
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x00036966 File Offset: 0x00034B66
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x0003696E File Offset: 0x00034B6E
		[CLSCompliant(false)]
		public ulong PackedValue
		{
			get
			{
				return this.packedValue;
			}
			set
			{
				this.packedValue = value;
			}
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00036977 File Offset: 0x00034B77
		public Rgba64(float x, float y, float z, float w)
		{
			this.packedValue = Rgba64.Pack(x, y, z, w);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00036989 File Offset: 0x00034B89
		public Rgba64(Vector4 vector)
		{
			this.packedValue = Rgba64.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000369B0 File Offset: 0x00034BB0
		public Vector4 ToVector4()
		{
			return new Vector4((this.packedValue & 65535UL) / 65535f, ((this.packedValue >> 16) & 65535UL) / 65535f, ((this.packedValue >> 32) & 65535UL) / 65535f, (this.packedValue >> 48) / 65535f);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00036989 File Offset: 0x00034B89
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = Rgba64.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00036A18 File Offset: 0x00034C18
		public override bool Equals(object obj)
		{
			return obj is Rgba64 && this.Equals((Rgba64)obj);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00036A30 File Offset: 0x00034C30
		public bool Equals(Rgba64 other)
		{
			return this.packedValue == other.packedValue;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00036A40 File Offset: 0x00034C40
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00036A52 File Offset: 0x00034C52
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00036A30 File Offset: 0x00034C30
		public static bool operator ==(Rgba64 lhs, Rgba64 rhs)
		{
			return lhs.packedValue == rhs.packedValue;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00036A5F File Offset: 0x00034C5F
		public static bool operator !=(Rgba64 lhs, Rgba64 rhs)
		{
			return lhs.packedValue != rhs.packedValue;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00036A74 File Offset: 0x00034C74
		private static ulong Pack(float x, float y, float z, float w)
		{
			return (ulong)Math.Round((double)(MathHelper.Clamp(x, 0f, 1f) * 65535f)) | ((ulong)Math.Round((double)(MathHelper.Clamp(y, 0f, 1f) * 65535f)) << 16) | ((ulong)Math.Round((double)(MathHelper.Clamp(z, 0f, 1f) * 65535f)) << 32) | ((ulong)Math.Round((double)(MathHelper.Clamp(w, 0f, 1f) * 65535f)) << 48);
		}

		// Token: 0x04000A95 RID: 2709
		private ulong packedValue;
	}
}
