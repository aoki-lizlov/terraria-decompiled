using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000F5 RID: 245
	public struct Short4 : IPackedVector<ulong>, IPackedVector, IEquatable<Short4>
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00036DC4 File Offset: 0x00034FC4
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x00036DCC File Offset: 0x00034FCC
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

		// Token: 0x060016A3 RID: 5795 RVA: 0x00036DD5 File Offset: 0x00034FD5
		public Short4(Vector4 vector)
		{
			this.packedValue = Short4.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00036DFA File Offset: 0x00034FFA
		public Short4(float x, float y, float z, float w)
		{
			this.packedValue = Short4.Pack(x, y, z, w);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00036E0C File Offset: 0x0003500C
		public Vector4 ToVector4()
		{
			return new Vector4((float)((short)(this.packedValue & 65535UL)), (float)((short)((this.packedValue >> 16) & 65535UL)), (float)((short)((this.packedValue >> 32) & 65535UL)), (float)((short)(this.packedValue >> 48)));
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00036DD5 File Offset: 0x00034FD5
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = Short4.Pack(vector.X, vector.Y, vector.Z, vector.W);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00036E5C File Offset: 0x0003505C
		public static bool operator !=(Short4 a, Short4 b)
		{
			return a.PackedValue != b.PackedValue;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00036E71 File Offset: 0x00035071
		public static bool operator ==(Short4 a, Short4 b)
		{
			return a.PackedValue == b.PackedValue;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00036E83 File Offset: 0x00035083
		public override bool Equals(object obj)
		{
			return obj is Short4 && this.Equals((Short4)obj);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00036E9B File Offset: 0x0003509B
		public bool Equals(Short4 other)
		{
			return this == other;
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00036EA9 File Offset: 0x000350A9
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00036EB6 File Offset: 0x000350B6
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00036EC8 File Offset: 0x000350C8
		private static ulong Pack(float x, float y, float z, float w)
		{
			return (ulong)(((long)Math.Round((double)MathHelper.Clamp(x, -32768f, 32767f)) & 65535L) | (((long)Math.Round((double)MathHelper.Clamp(y, -32768f, 32767f)) << 16) & (long)((ulong)(-65536))) | (((long)Math.Round((double)MathHelper.Clamp(z, -32768f, 32767f)) << 32) & 281470681743360L) | ((long)Math.Round((double)MathHelper.Clamp(w, -32768f, 32767f)) << 48));
		}

		// Token: 0x04000A98 RID: 2712
		private ulong packedValue;
	}
}
