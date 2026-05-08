using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000F3 RID: 243
	public struct Rgba1010102 : IPackedVector<uint>, IPackedVector, IEquatable<Rgba1010102>
	{
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00036B01 File Offset: 0x00034D01
		// (set) Token: 0x06001687 RID: 5767 RVA: 0x00036B09 File Offset: 0x00034D09
		[CLSCompliant(false)]
		public uint PackedValue
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

		// Token: 0x06001688 RID: 5768 RVA: 0x00036B12 File Offset: 0x00034D12
		public Rgba1010102(float x, float y, float z, float w)
		{
			this.packedValue = Rgba1010102.Pack(x, y, z, w);
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00036B24 File Offset: 0x00034D24
		public Rgba1010102(Vector4 vector)
		{
			this.packedValue = Rgba1010102.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00036B4C File Offset: 0x00034D4C
		public Vector4 ToVector4()
		{
			return new Vector4((this.packedValue & 1023U) / 1023f, ((this.packedValue >> 10) & 1023U) / 1023f, ((this.packedValue >> 20) & 1023U) / 1023f, (this.packedValue >> 30) / 3f);
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00036B24 File Offset: 0x00034D24
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = Rgba1010102.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00036BB1 File Offset: 0x00034DB1
		public override bool Equals(object obj)
		{
			return obj is Rgba1010102 && this.Equals((Rgba1010102)obj);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00036BC9 File Offset: 0x00034DC9
		public bool Equals(Rgba1010102 other)
		{
			return this.packedValue == other.packedValue;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00036BD9 File Offset: 0x00034DD9
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00036BEB File Offset: 0x00034DEB
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00036BC9 File Offset: 0x00034DC9
		public static bool operator ==(Rgba1010102 lhs, Rgba1010102 rhs)
		{
			return lhs.packedValue == rhs.packedValue;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00036BF8 File Offset: 0x00034DF8
		public static bool operator !=(Rgba1010102 lhs, Rgba1010102 rhs)
		{
			return lhs.packedValue != rhs.packedValue;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00036C0C File Offset: 0x00034E0C
		private static uint Pack(float x, float y, float z, float w)
		{
			return (uint)Math.Round((double)(MathHelper.Clamp(x, 0f, 1f) * 1023f)) | ((uint)Math.Round((double)(MathHelper.Clamp(y, 0f, 1f) * 1023f)) << 10) | ((uint)Math.Round((double)(MathHelper.Clamp(z, 0f, 1f) * 1023f)) << 20) | ((uint)Math.Round((double)(MathHelper.Clamp(w, 0f, 1f) * 3f)) << 30);
		}

		// Token: 0x04000A96 RID: 2710
		private uint packedValue;
	}
}
