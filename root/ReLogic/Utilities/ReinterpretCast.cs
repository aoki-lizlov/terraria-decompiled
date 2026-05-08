using System;
using System.Runtime.InteropServices;

namespace ReLogic.Utilities
{
	// Token: 0x02000005 RID: 5
	public static class ReinterpretCast
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000246C File Offset: 0x0000066C
		public static float UIntAsFloat(uint value)
		{
			return new ReinterpretCast.UIntFloat(value).FloatValue;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002479 File Offset: 0x00000679
		public static float IntAsFloat(int value)
		{
			return new ReinterpretCast.IntFloat(value).FloatValue;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002486 File Offset: 0x00000686
		public static uint FloatAsUInt(float value)
		{
			return new ReinterpretCast.UIntFloat(value).UIntValue;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002493 File Offset: 0x00000693
		public static int FloatAsInt(float value)
		{
			return new ReinterpretCast.IntFloat(value).IntValue;
		}

		// Token: 0x020000AC RID: 172
		[StructLayout(2)]
		private struct IntFloat
		{
			// Token: 0x06000405 RID: 1029 RVA: 0x0000DE0E File Offset: 0x0000C00E
			public IntFloat(int value)
			{
				this.FloatValue = 0f;
				this.IntValue = value;
			}

			// Token: 0x06000406 RID: 1030 RVA: 0x0000DE22 File Offset: 0x0000C022
			public IntFloat(float value)
			{
				this.IntValue = 0;
				this.FloatValue = value;
			}

			// Token: 0x04000541 RID: 1345
			[FieldOffset(0)]
			public readonly int IntValue;

			// Token: 0x04000542 RID: 1346
			[FieldOffset(0)]
			public readonly float FloatValue;
		}

		// Token: 0x020000AD RID: 173
		[StructLayout(2)]
		private struct UIntFloat
		{
			// Token: 0x06000407 RID: 1031 RVA: 0x0000DE32 File Offset: 0x0000C032
			public UIntFloat(uint value)
			{
				this.FloatValue = 0f;
				this.UIntValue = value;
			}

			// Token: 0x06000408 RID: 1032 RVA: 0x0000DE46 File Offset: 0x0000C046
			public UIntFloat(float value)
			{
				this.UIntValue = 0U;
				this.FloatValue = value;
			}

			// Token: 0x04000543 RID: 1347
			[FieldOffset(0)]
			public readonly uint UIntValue;

			// Token: 0x04000544 RID: 1348
			[FieldOffset(0)]
			public readonly float FloatValue;
		}
	}
}
