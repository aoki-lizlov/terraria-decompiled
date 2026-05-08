using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000F0 RID: 240
	public struct NormalizedShort4 : IPackedVector<ulong>, IPackedVector, IEquatable<NormalizedShort4>
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0003666B File Offset: 0x0003486B
		// (set) Token: 0x0600165F RID: 5727 RVA: 0x00036673 File Offset: 0x00034873
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

		// Token: 0x06001660 RID: 5728 RVA: 0x0003667C File Offset: 0x0003487C
		public NormalizedShort4(Vector4 vector)
		{
			this.packedValue = NormalizedShort4.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000366A1 File Offset: 0x000348A1
		public NormalizedShort4(float x, float y, float z, float w)
		{
			this.packedValue = NormalizedShort4.Pack(x, y, z, w);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x000366B4 File Offset: 0x000348B4
		public Vector4 ToVector4()
		{
			return new Vector4((float)((short)(this.packedValue & 65535UL)) / 32767f, (float)((short)((this.packedValue >> 16) & 65535UL)) / 32767f, (float)((short)((this.packedValue >> 32) & 65535UL)) / 32767f, (float)((short)((this.packedValue >> 48) & 65535UL)) / 32767f);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0003667C File Offset: 0x0003487C
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = NormalizedShort4.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00036723 File Offset: 0x00034923
		public static bool operator !=(NormalizedShort4 a, NormalizedShort4 b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00036730 File Offset: 0x00034930
		public static bool operator ==(NormalizedShort4 a, NormalizedShort4 b)
		{
			return a.Equals(b);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0003673A File Offset: 0x0003493A
		public override bool Equals(object obj)
		{
			return obj is NormalizedShort4 && this.Equals((NormalizedShort4)obj);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00036752 File Offset: 0x00034952
		public bool Equals(NormalizedShort4 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00036765 File Offset: 0x00034965
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00036772 File Offset: 0x00034972
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00036784 File Offset: 0x00034984
		private static ulong Pack(float x, float y, float z, float w)
		{
			ulong num = (ulong)MathHelper.Clamp((float)Math.Round((double)(x * 32767f)), -32767f, 32767f) & 65535UL;
			ulong num2 = ((ulong)MathHelper.Clamp((float)Math.Round((double)(y * 32767f)), -32767f, 32767f) & 65535UL) << 16;
			ulong num3 = ((ulong)MathHelper.Clamp((float)Math.Round((double)(z * 32767f)), -32767f, 32767f) & 65535UL) << 32;
			ulong num4 = ((ulong)MathHelper.Clamp((float)Math.Round((double)(w * 32767f)), -32767f, 32767f) & 65535UL) << 48;
			return num | num2 | num3 | num4;
		}

		// Token: 0x04000A93 RID: 2707
		private ulong packedValue;
	}
}
