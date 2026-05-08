using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000E7 RID: 231
	public struct HalfSingle : IPackedVector<ushort>, IPackedVector, IEquatable<HalfSingle>
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x00035D9C File Offset: 0x00033F9C
		// (set) Token: 0x06001608 RID: 5640 RVA: 0x00035DA4 File Offset: 0x00033FA4
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

		// Token: 0x06001609 RID: 5641 RVA: 0x00035DAD File Offset: 0x00033FAD
		public HalfSingle(float single)
		{
			this.packedValue = HalfTypeHelper.Convert(single);
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00035DBB File Offset: 0x00033FBB
		public float ToSingle()
		{
			return HalfTypeHelper.Convert(this.packedValue);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00035DC8 File Offset: 0x00033FC8
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = HalfTypeHelper.Convert(vector.X);
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00035DDB File Offset: 0x00033FDB
		Vector4 IPackedVector.ToVector4()
		{
			return new Vector4(this.ToSingle(), 0f, 0f, 1f);
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00035DF7 File Offset: 0x00033FF7
		public override bool Equals(object obj)
		{
			return obj is HalfSingle && this.Equals((HalfSingle)obj);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00035E0F File Offset: 0x0003400F
		public bool Equals(HalfSingle other)
		{
			return this.packedValue == other.packedValue;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00035E1F File Offset: 0x0003401F
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00035E31 File Offset: 0x00034031
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00035E0F File Offset: 0x0003400F
		public static bool operator ==(HalfSingle lhs, HalfSingle rhs)
		{
			return lhs.packedValue == rhs.packedValue;
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00035E3E File Offset: 0x0003403E
		public static bool operator !=(HalfSingle lhs, HalfSingle rhs)
		{
			return lhs.packedValue != rhs.packedValue;
		}

		// Token: 0x04000A8D RID: 2701
		private ushort packedValue;
	}
}
