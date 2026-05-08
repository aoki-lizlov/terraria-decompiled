using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x020009D7 RID: 2519
	internal static class GlobalizationMode
	{
		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06005BBC RID: 23484 RVA: 0x0013A265 File Offset: 0x00138465
		internal static bool Invariant
		{
			[CompilerGenerated]
			get
			{
				return GlobalizationMode.<Invariant>k__BackingField;
			}
		} = GlobalizationMode.GetGlobalizationInvariantMode();

		// Token: 0x06005BBD RID: 23485 RVA: 0x0000408A File Offset: 0x0000228A
		private static bool GetGlobalizationInvariantMode()
		{
			return false;
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x0013A26C File Offset: 0x0013846C
		// Note: this type is marked as 'beforefieldinit'.
		static GlobalizationMode()
		{
		}

		// Token: 0x04003781 RID: 14209
		private const string c_InvariantModeConfigSwitch = "System.Globalization.Invariant";

		// Token: 0x04003782 RID: 14210
		[CompilerGenerated]
		private static readonly bool <Invariant>k__BackingField;
	}
}
