using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000617 RID: 1559
	public static class CompatibilitySwitch
	{
		// Token: 0x06003BD0 RID: 15312 RVA: 0x0000408A File Offset: 0x0000228A
		public static bool IsEnabled(string compatibilitySwitchName)
		{
			return false;
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public static string GetValue(string compatibilitySwitchName)
		{
			return null;
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		internal static string GetValueInternal(string compatibilitySwitchName)
		{
			return null;
		}
	}
}
