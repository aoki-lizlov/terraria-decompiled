using System;

namespace System.Reflection.Emit
{
	// Token: 0x020008D8 RID: 2264
	public enum OperandType
	{
		// Token: 0x0400300A RID: 12298
		InlineBrTarget,
		// Token: 0x0400300B RID: 12299
		InlineField,
		// Token: 0x0400300C RID: 12300
		InlineI,
		// Token: 0x0400300D RID: 12301
		InlineI8,
		// Token: 0x0400300E RID: 12302
		InlineMethod,
		// Token: 0x0400300F RID: 12303
		InlineNone,
		// Token: 0x04003010 RID: 12304
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		InlinePhi,
		// Token: 0x04003011 RID: 12305
		InlineR,
		// Token: 0x04003012 RID: 12306
		InlineSig = 9,
		// Token: 0x04003013 RID: 12307
		InlineString,
		// Token: 0x04003014 RID: 12308
		InlineSwitch,
		// Token: 0x04003015 RID: 12309
		InlineTok,
		// Token: 0x04003016 RID: 12310
		InlineType,
		// Token: 0x04003017 RID: 12311
		InlineVar,
		// Token: 0x04003018 RID: 12312
		ShortInlineBrTarget,
		// Token: 0x04003019 RID: 12313
		ShortInlineI,
		// Token: 0x0400301A RID: 12314
		ShortInlineR,
		// Token: 0x0400301B RID: 12315
		ShortInlineVar
	}
}
