using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000E5 RID: 229
	public struct Bgra5551 : IPackedVector<ushort>, IPackedVector, IEquatable<Bgra5551>
	{
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x00035AB0 File Offset: 0x00033CB0
		// (set) Token: 0x060015EE RID: 5614 RVA: 0x00035AB8 File Offset: 0x00033CB8
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

		// Token: 0x060015EF RID: 5615 RVA: 0x00035AC1 File Offset: 0x00033CC1
		public Bgra5551(float x, float y, float z, float w)
		{
			this.packedValue = Bgra5551.Pack(x, y, z, w);
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00035AD3 File Offset: 0x00033CD3
		public Bgra5551(Vector4 vector)
		{
			this.packedValue = Bgra5551.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00035AF8 File Offset: 0x00033CF8
		public Vector4 ToVector4()
		{
			return new Vector4((float)((this.packedValue >> 10) & 31) / 31f, (float)((this.packedValue >> 5) & 31) / 31f, (float)(this.packedValue & 31) / 31f, (float)(this.packedValue >> 15));
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00035AD3 File Offset: 0x00033CD3
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = Bgra5551.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00035B49 File Offset: 0x00033D49
		public override bool Equals(object obj)
		{
			return obj is Bgra5551 && this.Equals((Bgra5551)obj);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00035B61 File Offset: 0x00033D61
		public bool Equals(Bgra5551 other)
		{
			return this.packedValue == other.packedValue;
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00035B71 File Offset: 0x00033D71
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00035B83 File Offset: 0x00033D83
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00035B61 File Offset: 0x00033D61
		public static bool operator ==(Bgra5551 lhs, Bgra5551 rhs)
		{
			return lhs.packedValue == rhs.packedValue;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00035B90 File Offset: 0x00033D90
		public static bool operator !=(Bgra5551 lhs, Bgra5551 rhs)
		{
			return lhs.packedValue != rhs.packedValue;
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00035BA4 File Offset: 0x00033DA4
		private static ushort Pack(float x, float y, float z, float w)
		{
			return (ushort)(((int)((ushort)Math.Round((double)(MathHelper.Clamp(x, 0f, 1f) * 31f))) << 10) | ((int)((ushort)Math.Round((double)(MathHelper.Clamp(y, 0f, 1f) * 31f))) << 5) | (int)((ushort)Math.Round((double)(MathHelper.Clamp(z, 0f, 1f) * 31f))) | ((int)((ushort)Math.Round((double)MathHelper.Clamp(w, 0f, 1f))) << 15));
		}

		// Token: 0x04000A8B RID: 2699
		private ushort packedValue;
	}
}
