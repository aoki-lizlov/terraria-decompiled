using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000EE RID: 238
	public struct NormalizedByte4 : IPackedVector<uint>, IPackedVector, IEquatable<NormalizedByte4>
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x00036361 File Offset: 0x00034561
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x00036369 File Offset: 0x00034569
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

		// Token: 0x06001645 RID: 5701 RVA: 0x00036372 File Offset: 0x00034572
		public NormalizedByte4(Vector4 vector)
		{
			this.packedValue = NormalizedByte4.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00036397 File Offset: 0x00034597
		public NormalizedByte4(float x, float y, float z, float w)
		{
			this.packedValue = NormalizedByte4.Pack(x, y, z, w);
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x000363AC File Offset: 0x000345AC
		public Vector4 ToVector4()
		{
			return new Vector4((float)((sbyte)(this.packedValue & 255U)) / 127f, (float)((sbyte)((this.packedValue >> 8) & 255U)) / 127f, (float)((sbyte)((this.packedValue >> 16) & 255U)) / 127f, (float)((sbyte)((this.packedValue >> 24) & 255U)) / 127f);
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00036372 File Offset: 0x00034572
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = NormalizedByte4.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00036416 File Offset: 0x00034616
		public static bool operator !=(NormalizedByte4 a, NormalizedByte4 b)
		{
			return a.packedValue != b.packedValue;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00036429 File Offset: 0x00034629
		public static bool operator ==(NormalizedByte4 a, NormalizedByte4 b)
		{
			return a.packedValue == b.packedValue;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00036439 File Offset: 0x00034639
		public override bool Equals(object obj)
		{
			return obj is NormalizedByte4 && this.Equals((NormalizedByte4)obj);
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00036429 File Offset: 0x00034629
		public bool Equals(NormalizedByte4 other)
		{
			return this.packedValue == other.packedValue;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00036451 File Offset: 0x00034651
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0003645E File Offset: 0x0003465E
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00036470 File Offset: 0x00034670
		private static uint Pack(float x, float y, float z, float w)
		{
			uint num = (uint)Math.Round((double)(MathHelper.Clamp(x, -1f, 1f) * 127f)) & 255U;
			uint num2 = ((uint)Math.Round((double)(MathHelper.Clamp(y, -1f, 1f) * 127f)) << 8) & 65280U;
			uint num3 = ((uint)Math.Round((double)(MathHelper.Clamp(z, -1f, 1f) * 127f)) << 16) & 16711680U;
			uint num4 = ((uint)Math.Round((double)(MathHelper.Clamp(w, -1f, 1f) * 127f)) << 24) & 4278190080U;
			return num | num2 | num3 | num4;
		}

		// Token: 0x04000A91 RID: 2705
		private uint packedValue;
	}
}
