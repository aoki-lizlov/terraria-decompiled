using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000E2 RID: 226
	public struct Alpha8 : IPackedVector<byte>, IPackedVector, IEquatable<Alpha8>
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x000356E0 File Offset: 0x000338E0
		// (set) Token: 0x060015C6 RID: 5574 RVA: 0x000356E8 File Offset: 0x000338E8
		[CLSCompliant(false)]
		public byte PackedValue
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

		// Token: 0x060015C7 RID: 5575 RVA: 0x000356F1 File Offset: 0x000338F1
		public Alpha8(float alpha)
		{
			this.packedValue = Alpha8.Pack(alpha);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x000356FF File Offset: 0x000338FF
		public float ToAlpha()
		{
			return (float)this.packedValue / 255f;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0003570F File Offset: 0x0003390F
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = Alpha8.Pack(vector.W);
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00035722 File Offset: 0x00033922
		Vector4 IPackedVector.ToVector4()
		{
			return new Vector4(0f, 0f, 0f, (float)this.packedValue / 255f);
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00035746 File Offset: 0x00033946
		public override bool Equals(object obj)
		{
			return obj is Alpha8 && this.Equals((Alpha8)obj);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0003575E File Offset: 0x0003395E
		public bool Equals(Alpha8 other)
		{
			return this.packedValue == other.packedValue;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0003576E File Offset: 0x0003396E
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00035780 File Offset: 0x00033980
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0003575E File Offset: 0x0003395E
		public static bool operator ==(Alpha8 lhs, Alpha8 rhs)
		{
			return lhs.packedValue == rhs.packedValue;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0003578D File Offset: 0x0003398D
		public static bool operator !=(Alpha8 lhs, Alpha8 rhs)
		{
			return lhs.packedValue != rhs.packedValue;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000357A0 File Offset: 0x000339A0
		private static byte Pack(float alpha)
		{
			return (byte)Math.Round((double)(MathHelper.Clamp(alpha, 0f, 1f) * 255f));
		}

		// Token: 0x04000A88 RID: 2696
		private byte packedValue;
	}
}
