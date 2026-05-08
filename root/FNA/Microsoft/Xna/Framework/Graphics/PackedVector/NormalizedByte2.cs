using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000ED RID: 237
	public struct NormalizedByte2 : IPackedVector<ushort>, IPackedVector, IEquatable<NormalizedByte2>
	{
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x00036211 File Offset: 0x00034411
		// (set) Token: 0x06001636 RID: 5686 RVA: 0x00036219 File Offset: 0x00034419
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

		// Token: 0x06001637 RID: 5687 RVA: 0x00036222 File Offset: 0x00034422
		public NormalizedByte2(Vector2 vector)
		{
			this.packedValue = NormalizedByte2.Pack(vector.X, vector.Y);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0003623B File Offset: 0x0003443B
		public NormalizedByte2(float x, float y)
		{
			this.packedValue = NormalizedByte2.Pack(x, y);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0003624A File Offset: 0x0003444A
		public Vector2 ToVector2()
		{
			return new Vector2((float)((sbyte)(this.packedValue & 255)) / 127f, (float)((sbyte)((this.packedValue >> 8) & 255)) / 127f);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0003627B File Offset: 0x0003447B
		void IPackedVector.PackFromVector4(Vector4 vector)
		{
			this.packedValue = NormalizedByte2.Pack(vector.X, vector.Y);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00036294 File Offset: 0x00034494
		Vector4 IPackedVector.ToVector4()
		{
			return new Vector4(this.ToVector2(), 0f, 1f);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000362AB File Offset: 0x000344AB
		public static bool operator !=(NormalizedByte2 a, NormalizedByte2 b)
		{
			return a.packedValue != b.packedValue;
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000362BE File Offset: 0x000344BE
		public static bool operator ==(NormalizedByte2 a, NormalizedByte2 b)
		{
			return a.packedValue == b.packedValue;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x000362CE File Offset: 0x000344CE
		public override bool Equals(object obj)
		{
			return obj is NormalizedByte2 && this.Equals((NormalizedByte2)obj);
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x000362BE File Offset: 0x000344BE
		public bool Equals(NormalizedByte2 other)
		{
			return this.packedValue == other.packedValue;
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x000362E6 File Offset: 0x000344E6
		public override int GetHashCode()
		{
			return this.packedValue.GetHashCode();
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000362F3 File Offset: 0x000344F3
		public override string ToString()
		{
			return this.packedValue.ToString("X");
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00036308 File Offset: 0x00034508
		private static ushort Pack(float x, float y)
		{
			int num = (int)((ushort)Math.Round((double)(MathHelper.Clamp(x, -1f, 1f) * 127f)) & 255);
			int num2 = ((int)((ushort)Math.Round((double)(MathHelper.Clamp(y, -1f, 1f) * 127f))) << 8) & 65280;
			return (ushort)(num | num2);
		}

		// Token: 0x04000A90 RID: 2704
		private ushort packedValue;
	}
}
