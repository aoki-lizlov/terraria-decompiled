using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x020000E8 RID: 232
	internal static class HalfTypeHelper
	{
		// Token: 0x06001613 RID: 5651 RVA: 0x00035E54 File Offset: 0x00034054
		internal static ushort Convert(float f)
		{
			return HalfTypeHelper.Convert(new HalfTypeHelper.uif
			{
				f = f
			}.i);
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00035E7C File Offset: 0x0003407C
		internal static ushort Convert(int i)
		{
			int num = (i >> 16) & 32768;
			int num2 = ((i >> 23) & 255) - 112;
			int num3 = i & 8388607;
			if (num2 <= 0)
			{
				if (num2 < -10)
				{
					return (ushort)num;
				}
				num3 |= 8388608;
				int num4 = 14 - num2;
				int num5 = (1 << num4 - 1) - 1;
				int num6 = (num3 >> num4) & 1;
				num3 = num3 + num5 + num6 >> num4;
				return (ushort)(num | num3);
			}
			else if (num2 == 143)
			{
				if (num3 == 0)
				{
					return (ushort)(num | 31744);
				}
				num3 >>= 13;
				return (ushort)(num | 31744 | num3 | ((num3 == 0) ? 1 : 0));
			}
			else
			{
				num3 = num3 + 4095 + ((num3 >> 13) & 1);
				if ((num3 & 8388608) != 0)
				{
					num3 = 0;
					num2++;
				}
				if (num2 > 30)
				{
					return (ushort)(num | 31744);
				}
				return (ushort)(num | (num2 << 10) | (num3 >> 13));
			}
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00035F4C File Offset: 0x0003414C
		internal static float Convert(ushort value)
		{
			uint num = (uint)(value & 1023);
			uint num2 = 4294967282U;
			uint num3;
			if (((int)value & -33792) == 0)
			{
				if (num != 0U)
				{
					while ((num & 1024U) == 0U)
					{
						num2 -= 1U;
						num <<= 1;
					}
					num &= 4294966271U;
					num3 = (uint)(((int)(value & 32768) << 16) | (int)((int)(num2 + 127U) << 23) | (int)((int)num << 13));
				}
				else
				{
					num3 = (uint)((uint)(value & 32768) << 16);
				}
			}
			else
			{
				num3 = (uint)(((int)(value & 32768) << 16) | (int)((int)((((uint)value >> 10) & 31U) - 15U + 127U) << 23) | (int)((int)num << 13));
			}
			return new HalfTypeHelper.uif
			{
				u = num3
			}.f;
		}

		// Token: 0x020003D9 RID: 985
		[StructLayout(LayoutKind.Explicit)]
		private struct uif
		{
			// Token: 0x04001DE2 RID: 7650
			[FieldOffset(0)]
			public float f;

			// Token: 0x04001DE3 RID: 7651
			[FieldOffset(0)]
			public int i;

			// Token: 0x04001DE4 RID: 7652
			[FieldOffset(0)]
			public uint u;
		}
	}
}
