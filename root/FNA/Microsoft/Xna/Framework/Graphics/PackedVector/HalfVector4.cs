using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000EA RID: 234
	public struct HalfVector4 : IPackedVector<ulong>, IPackedVector, IEquatable<HalfVector4>
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x000360FE File Offset: 0x000342FE
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x00036106 File Offset: 0x00034306
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

		// Token: 0x06001626 RID: 5670 RVA: 0x0003610F File Offset: 0x0003430F
		public HalfVector4(float x, float y, float z, float w)
		{
			this.packedValue = HalfVector4.Pack(x, y, z, w);
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00036121 File Offset: 0x00034321
		public HalfVector4(Vector4 vector)
		{
			this.packedValue = HalfVector4.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00036146 File Offset: 0x00034346
		public Vector4 ToVector4()
		{
			return new Vector4(HalfTypeHelper.Convert((ushort)this.packedValue), HalfTypeHelper.Convert((ushort)(this.packedValue >> 16)), HalfTypeHelper.Convert((ushort)(this.packedValue >> 32)), HalfTypeHelper.Convert((ushort)(this.packedValue >> 48)));
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00036121 File Offset: 0x00034321
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = HalfVector4.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00036186 File Offset: 0x00034386
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00036198 File Offset: 0x00034398
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000361A5 File Offset: 0x000343A5
		public override bool Equals(object obj)
		{
			return obj is HalfVector4 && this.Equals((HalfVector4)obj);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000361BD File Offset: 0x000343BD
		public bool Equals(HalfVector4 other)
		{
			return this.packedValue.Equals(other.packedValue);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x000361D0 File Offset: 0x000343D0
		public static bool operator ==(HalfVector4 a, HalfVector4 b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000361DA File Offset: 0x000343DA
		public static bool operator !=(HalfVector4 a, HalfVector4 b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000361E7 File Offset: 0x000343E7
		private static ulong Pack(float x, float y, float z, float w)
		{
			return (ulong)HalfTypeHelper.Convert(x) | ((ulong)HalfTypeHelper.Convert(y) << 16) | ((ulong)HalfTypeHelper.Convert(z) << 32) | ((ulong)HalfTypeHelper.Convert(w) << 48);
		}

		// Token: 0x04000A8F RID: 2703
		private ulong packedValue;
	}
}
