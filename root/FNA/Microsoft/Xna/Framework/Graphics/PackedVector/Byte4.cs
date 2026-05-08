using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000E6 RID: 230
	public struct Byte4 : IPackedVector<uint>, IPackedVector, IEquatable<Byte4>
	{
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00035C2B File Offset: 0x00033E2B
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x00035C33 File Offset: 0x00033E33
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

		// Token: 0x060015FC RID: 5628 RVA: 0x00035C3C File Offset: 0x00033E3C
		public Byte4(Vector4 vector)
		{
			this.packedValue = Byte4.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00035C61 File Offset: 0x00033E61
		public Byte4(float x, float y, float z, float w)
		{
			this.packedValue = Byte4.Pack(x, y, z, w);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00035C74 File Offset: 0x00033E74
		public Vector4 ToVector4()
		{
			return new Vector4(this.packedValue & 255U, (this.packedValue >> 8) & 255U, (this.packedValue >> 16) & 255U, this.packedValue >> 24);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00035C3C File Offset: 0x00033E3C
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = Byte4.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00035CC0 File Offset: 0x00033EC0
		public static bool operator !=(Byte4 a, Byte4 b)
		{
			return a.packedValue != b.packedValue;
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00035CD3 File Offset: 0x00033ED3
		public static bool operator ==(Byte4 a, Byte4 b)
		{
			return a.packedValue == b.packedValue;
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00035CE3 File Offset: 0x00033EE3
		public override bool Equals(object obj)
		{
			return obj is Byte4 && this.Equals((Byte4)obj);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00035CFB File Offset: 0x00033EFB
		public bool Equals(Byte4 other)
		{
			return this == other;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00035D09 File Offset: 0x00033F09
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00035D16 File Offset: 0x00033F16
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00035D28 File Offset: 0x00033F28
		private static uint Pack(float x, float y, float z, float w)
		{
			return (uint)Math.Round((double)MathHelper.Clamp(x, 0f, 255f)) | ((uint)Math.Round((double)MathHelper.Clamp(y, 0f, 255f)) << 8) | ((uint)Math.Round((double)MathHelper.Clamp(z, 0f, 255f)) << 16) | ((uint)Math.Round((double)MathHelper.Clamp(w, 0f, 255f)) << 24);
		}

		// Token: 0x04000A8C RID: 2700
		private uint packedValue;
	}
}
