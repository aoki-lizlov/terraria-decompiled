using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000E4 RID: 228
	public struct Bgra4444 : IPackedVector<ushort>, IPackedVector, IEquatable<Bgra4444>
	{
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x0003592C File Offset: 0x00033B2C
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x00035934 File Offset: 0x00033B34
		[CLSCompliant(false)]
		public ushort PackedValue
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

		// Token: 0x060015E2 RID: 5602 RVA: 0x0003593D File Offset: 0x00033B3D
		public Bgra4444(float x, float y, float z, float w)
		{
			this.packedValue = Bgra4444.Pack(x, y, z, w);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0003594F File Offset: 0x00033B4F
		public Bgra4444(Vector4 vector)
		{
			this.packedValue = Bgra4444.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00035974 File Offset: 0x00033B74
		public Vector4 ToVector4()
		{
			return new Vector4((float)((this.packedValue >> 8) & 15) / 15f, (float)((this.packedValue >> 4) & 15) / 15f, (float)(this.packedValue & 15) / 15f, (float)(this.packedValue >> 12) / 15f);
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x0003594F File Offset: 0x00033B4F
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = Bgra4444.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x000359CA File Offset: 0x00033BCA
		public override bool Equals(object obj)
		{
			return obj is Bgra4444 && this.Equals((Bgra4444)obj);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000359E2 File Offset: 0x00033BE2
		public bool Equals(Bgra4444 other)
		{
			return this.packedValue == other.packedValue;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000359F2 File Offset: 0x00033BF2
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00035A04 File Offset: 0x00033C04
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x000359E2 File Offset: 0x00033BE2
		public static bool operator ==(Bgra4444 lhs, Bgra4444 rhs)
		{
			return lhs.packedValue == rhs.packedValue;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00035A11 File Offset: 0x00033C11
		public static bool operator !=(Bgra4444 lhs, Bgra4444 rhs)
		{
			return lhs.packedValue != rhs.packedValue;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00035A24 File Offset: 0x00033C24
		private static ushort Pack(float x, float y, float z, float w)
		{
			return (ushort)(((int)((ushort)Math.Round((double)(MathHelper.Clamp(x, 0f, 1f) * 15f))) << 8) | ((int)((ushort)Math.Round((double)(MathHelper.Clamp(y, 0f, 1f) * 15f))) << 4) | (int)((ushort)Math.Round((double)(MathHelper.Clamp(z, 0f, 1f) * 15f))) | ((int)((ushort)Math.Round((double)(MathHelper.Clamp(w, 0f, 1f) * 15f))) << 12));
		}

		// Token: 0x04000A8A RID: 2698
		private ushort packedValue;
	}
}
