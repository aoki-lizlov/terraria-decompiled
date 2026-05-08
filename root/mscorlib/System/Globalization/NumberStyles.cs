using System;

namespace System.Globalization
{
	// Token: 0x020009C6 RID: 2502
	[Flags]
	public enum NumberStyles
	{
		// Token: 0x040036FF RID: 14079
		None = 0,
		// Token: 0x04003700 RID: 14080
		AllowLeadingWhite = 1,
		// Token: 0x04003701 RID: 14081
		AllowTrailingWhite = 2,
		// Token: 0x04003702 RID: 14082
		AllowLeadingSign = 4,
		// Token: 0x04003703 RID: 14083
		AllowTrailingSign = 8,
		// Token: 0x04003704 RID: 14084
		AllowParentheses = 16,
		// Token: 0x04003705 RID: 14085
		AllowDecimalPoint = 32,
		// Token: 0x04003706 RID: 14086
		AllowThousands = 64,
		// Token: 0x04003707 RID: 14087
		AllowExponent = 128,
		// Token: 0x04003708 RID: 14088
		AllowCurrencySymbol = 256,
		// Token: 0x04003709 RID: 14089
		AllowHexSpecifier = 512,
		// Token: 0x0400370A RID: 14090
		Integer = 7,
		// Token: 0x0400370B RID: 14091
		HexNumber = 515,
		// Token: 0x0400370C RID: 14092
		Number = 111,
		// Token: 0x0400370D RID: 14093
		Float = 167,
		// Token: 0x0400370E RID: 14094
		Currency = 383,
		// Token: 0x0400370F RID: 14095
		Any = 511
	}
}
