using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000F4 RID: 244
	public struct Short2 : IPackedVector<uint>, IPackedVector, IEquatable<Short2>
	{
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x00036C99 File Offset: 0x00034E99
		// (set) Token: 0x06001694 RID: 5780 RVA: 0x00036CA1 File Offset: 0x00034EA1
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

		// Token: 0x06001695 RID: 5781 RVA: 0x00036CAA File Offset: 0x00034EAA
		public Short2(Vector2 vector)
		{
			this.packedValue = Short2.Pack(vector.X, vector.Y);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00036CC3 File Offset: 0x00034EC3
		public Short2(float x, float y)
		{
			this.packedValue = Short2.Pack(x, y);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00036CD2 File Offset: 0x00034ED2
		public Vector2 ToVector2()
		{
			return new Vector2((float)((short)(this.packedValue & 65535U)), (float)((short)(this.packedValue >> 16)));
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00036CF2 File Offset: 0x00034EF2
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = Short2.Pack(vector.X, vector.Y);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00036D0B File Offset: 0x00034F0B
		Vector4 IPackedVector.ToVector4()
		{
			return new Vector4(this.ToVector2(), 0f, 1f);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00036D22 File Offset: 0x00034F22
		public static bool operator !=(Short2 a, Short2 b)
		{
			return a.packedValue != b.packedValue;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00036D35 File Offset: 0x00034F35
		public static bool operator ==(Short2 a, Short2 b)
		{
			return a.packedValue == b.packedValue;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00036D45 File Offset: 0x00034F45
		public override bool Equals(object obj)
		{
			return obj is Short2 && this.Equals((Short2)obj);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00036D5D File Offset: 0x00034F5D
		public bool Equals(Short2 other)
		{
			return this == other;
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00036D6B File Offset: 0x00034F6B
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00036D78 File Offset: 0x00034F78
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00036D8A File Offset: 0x00034F8A
		private static uint Pack(float x, float y)
		{
			return (uint)(((int)Math.Round((double)MathHelper.Clamp(x, -32768f, 32767f)) & 65535) | ((int)Math.Round((double)MathHelper.Clamp(y, -32768f, 32767f)) << 16));
		}

		// Token: 0x04000A97 RID: 2711
		private uint packedValue;
	}
}
