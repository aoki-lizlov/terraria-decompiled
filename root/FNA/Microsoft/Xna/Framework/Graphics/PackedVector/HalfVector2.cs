using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000E9 RID: 233
	public struct HalfVector2 : IPackedVector<uint>, IPackedVector, IEquatable<HalfVector2>
	{
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x00035FE9 File Offset: 0x000341E9
		// (set) Token: 0x06001617 RID: 5655 RVA: 0x00035FF1 File Offset: 0x000341F1
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

		// Token: 0x06001618 RID: 5656 RVA: 0x00035FFA File Offset: 0x000341FA
		public HalfVector2(float x, float y)
		{
			this.packedValue = HalfVector2.PackHelper(x, y);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00036009 File Offset: 0x00034209
		public HalfVector2(Vector2 vector)
		{
			this.packedValue = HalfVector2.PackHelper(vector.X, vector.Y);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00036024 File Offset: 0x00034224
		public Vector2 ToVector2()
		{
			Vector2 vector;
			vector.X = HalfTypeHelper.Convert((ushort)this.packedValue);
			vector.Y = HalfTypeHelper.Convert((ushort)(this.packedValue >> 16));
			return vector;
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0003605B File Offset: 0x0003425B
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = HalfVector2.PackHelper(vector.X, vector.Y);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00036074 File Offset: 0x00034274
		Vector4 IPackedVector.ToVector4()
		{
			return new Vector4(this.ToVector2(), 0f, 1f);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0003608B File Offset: 0x0003428B
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0003609D File Offset: 0x0003429D
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x000360AA File Offset: 0x000342AA
		public override bool Equals(object obj)
		{
			return obj is HalfVector2 && this.Equals((HalfVector2)obj);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x000360C2 File Offset: 0x000342C2
		public bool Equals(HalfVector2 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x000360D5 File Offset: 0x000342D5
		public static bool operator ==(HalfVector2 a, HalfVector2 b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x000360DF File Offset: 0x000342DF
		public static bool operator !=(HalfVector2 a, HalfVector2 b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x000360EC File Offset: 0x000342EC
		private static uint PackHelper(float vectorX, float vectorY)
		{
			return (uint)((int)HalfTypeHelper.Convert(vectorX) | ((int)HalfTypeHelper.Convert(vectorY) << 16));
		}

		// Token: 0x04000A8E RID: 2702
		private uint packedValue;
	}
}
