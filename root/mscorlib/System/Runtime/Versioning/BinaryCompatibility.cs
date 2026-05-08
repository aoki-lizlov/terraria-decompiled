using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000616 RID: 1558
	internal static class BinaryCompatibility
	{
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06003BCE RID: 15310 RVA: 0x00003FB7 File Offset: 0x000021B7
		public static bool TargetsAtLeast_Desktop_V4_5_2
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x000D0E95 File Offset: 0x000CF095
		// Note: this type is marked as 'beforefieldinit'.
		static BinaryCompatibility()
		{
		}

		// Token: 0x04002697 RID: 9879
		public static readonly bool TargetsAtLeast_Desktop_V4_5 = true;

		// Token: 0x04002698 RID: 9880
		public static readonly bool TargetsAtLeast_Desktop_V4_5_1 = true;
	}
}
