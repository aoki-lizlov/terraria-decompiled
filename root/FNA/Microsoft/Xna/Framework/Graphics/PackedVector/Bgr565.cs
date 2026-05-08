using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000E3 RID: 227
	public struct Bgr565 : IPackedVector<ushort>, IPackedVector, IEquatable<Bgr565>
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x000357BF File Offset: 0x000339BF
		// (set) Token: 0x060015D3 RID: 5587 RVA: 0x000357C7 File Offset: 0x000339C7
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

		// Token: 0x060015D4 RID: 5588 RVA: 0x000357D0 File Offset: 0x000339D0
		public Bgr565(float x, float y, float z)
		{
			this.packedValue = Bgr565.Pack(x, y, z);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000357E0 File Offset: 0x000339E0
		public Bgr565(Vector3 vector)
		{
			this.packedValue = Bgr565.Pack(vector.X, vector.Y, vector.Z);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x000357FF File Offset: 0x000339FF
		public Vector3 ToVector3()
		{
			return new Vector3((float)(this.packedValue >> 11) / 31f, (float)((this.packedValue >> 5) & 63) / 63f, (float)(this.packedValue & 31) / 31f);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00035838 File Offset: 0x00033A38
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			Bgr565.Pack(vector.X, vector.Y, vector.Z);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00035852 File Offset: 0x00033A52
		Vector4 IPackedVector.ToVector4()
		{
			return new Vector4(this.ToVector3(), 1f);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00035864 File Offset: 0x00033A64
		public override bool Equals(object obj)
		{
			return obj is Bgr565 && this.Equals((Bgr565)obj);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0003587C File Offset: 0x00033A7C
		public bool Equals(Bgr565 other)
		{
			return this.packedValue == other.packedValue;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0003588C File Offset: 0x00033A8C
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0003589E File Offset: 0x00033A9E
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x0003587C File Offset: 0x00033A7C
		public static bool operator ==(Bgr565 lhs, Bgr565 rhs)
		{
			return lhs.packedValue == rhs.packedValue;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000358AB File Offset: 0x00033AAB
		public static bool operator !=(Bgr565 lhs, Bgr565 rhs)
		{
			return lhs.packedValue != rhs.packedValue;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000358C0 File Offset: 0x00033AC0
		private static ushort Pack(float x, float y, float z)
		{
			return (ushort)(((int)((ushort)Math.Round((double)(MathHelper.Clamp(x, 0f, 1f) * 31f))) << 11) | ((int)((ushort)Math.Round((double)(MathHelper.Clamp(y, 0f, 1f) * 63f))) << 5) | (int)((ushort)Math.Round((double)(MathHelper.Clamp(z, 0f, 1f) * 31f))));
		}

		// Token: 0x04000A89 RID: 2697
		private ushort packedValue;
	}
}
