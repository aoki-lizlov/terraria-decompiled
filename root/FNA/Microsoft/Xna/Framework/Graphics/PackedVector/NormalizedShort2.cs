using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000EF RID: 239
	public struct NormalizedShort2 : IPackedVector<uint>, IPackedVector, IEquatable<NormalizedShort2>
	{
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0003651A File Offset: 0x0003471A
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x00036522 File Offset: 0x00034722
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

		// Token: 0x06001652 RID: 5714 RVA: 0x0003652B File Offset: 0x0003472B
		public NormalizedShort2(Vector2 vector)
		{
			this.packedValue = NormalizedShort2.Pack(vector.X, vector.Y);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00036544 File Offset: 0x00034744
		public NormalizedShort2(float x, float y)
		{
			this.packedValue = NormalizedShort2.Pack(x, y);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00036553 File Offset: 0x00034753
		public Vector2 ToVector2()
		{
			return new Vector2((float)((short)(this.packedValue & 65535U)) / 32767f, (float)((short)(this.packedValue >> 16)) / 32767f);
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0003657F File Offset: 0x0003477F
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = NormalizedShort2.Pack(vector.X, vector.Y);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00036598 File Offset: 0x00034798
		Vector4 IPackedVector.ToVector4()
		{
			return new Vector4(this.ToVector2(), 0f, 1f);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x000365AF File Offset: 0x000347AF
		public static bool operator !=(NormalizedShort2 a, NormalizedShort2 b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x000365BC File Offset: 0x000347BC
		public static bool operator ==(NormalizedShort2 a, NormalizedShort2 b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x000365C6 File Offset: 0x000347C6
		public override bool Equals(object obj)
		{
			return obj is NormalizedShort2 && this.Equals((NormalizedShort2)obj);
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000365DE File Offset: 0x000347DE
		public bool Equals(NormalizedShort2 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x000365F1 File Offset: 0x000347F1
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000365FE File Offset: 0x000347FE
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00036610 File Offset: 0x00034810
		private static uint Pack(float x, float y)
		{
			uint num = (uint)((int)MathHelper.Clamp((float)Math.Round((double)(x * 32767f)), -32767f, 32767f) & 65535);
			uint num2 = (uint)((uint)((int)MathHelper.Clamp((float)Math.Round((double)(y * 32767f)), -32767f, 32767f) & 65535) << 16);
			return num | num2;
		}

		// Token: 0x04000A92 RID: 2706
		private uint packedValue;
	}
}
