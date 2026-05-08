using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000F1 RID: 241
	public struct Rg32 : IPackedVector<uint>, IPackedVector, IEquatable<Rg32>
	{
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x00036837 File Offset: 0x00034A37
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x0003683F File Offset: 0x00034A3F
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

		// Token: 0x0600166D RID: 5741 RVA: 0x00036848 File Offset: 0x00034A48
		public Rg32(float x, float y)
		{
			this.packedValue = Rg32.Pack(x, y);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00036857 File Offset: 0x00034A57
		public Rg32(Vector2 vector)
		{
			this.packedValue = Rg32.Pack(vector.X, vector.Y);
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00036870 File Offset: 0x00034A70
		public Vector2 ToVector2()
		{
			return new Vector2((this.packedValue & 65535U) / 65535f, (this.packedValue >> 16) / 65535f);
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0003689C File Offset: 0x00034A9C
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = Rg32.Pack(vector.X, vector.Y);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x000368B5 File Offset: 0x00034AB5
		Vector4 IPackedVector.ToVector4()
		{
			return new Vector4(this.ToVector2(), 0f, 1f);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x000368CC File Offset: 0x00034ACC
		public override bool Equals(object obj)
		{
			return obj is Rg32 && this.Equals((Rg32)obj);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x000368E4 File Offset: 0x00034AE4
		public bool Equals(Rg32 other)
		{
			return this.packedValue == other.packedValue;
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x000368F4 File Offset: 0x00034AF4
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00036906 File Offset: 0x00034B06
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x000368E4 File Offset: 0x00034AE4
		public static bool operator ==(Rg32 lhs, Rg32 rhs)
		{
			return lhs.packedValue == rhs.packedValue;
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00036913 File Offset: 0x00034B13
		public static bool operator !=(Rg32 lhs, Rg32 rhs)
		{
			return lhs.packedValue != rhs.packedValue;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00036926 File Offset: 0x00034B26
		private static uint Pack(float x, float y)
		{
			return (uint)Math.Round((double)(MathHelper.Clamp(x, 0f, 1f) * 65535f)) | ((uint)Math.Round((double)(MathHelper.Clamp(y, 0f, 1f) * 65535f)) << 16);
		}

		// Token: 0x04000A94 RID: 2708
		private uint packedValue;
	}
}
