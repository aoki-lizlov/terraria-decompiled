using System;
using System.Text;

namespace ReLogic.Text
{
	// Token: 0x02000013 RID: 19
	internal static class StringBuilderExtension
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00003DB7 File Offset: 0x00001FB7
		internal static bool IsEmpty(this StringBuilder stringBuilder)
		{
			return stringBuilder.Length == 0;
		}
	}
}
