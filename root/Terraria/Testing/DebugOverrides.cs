using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Terraria.Testing
{
	// Token: 0x0200010F RID: 271
	public class DebugOverrides
	{
		// Token: 0x06001AB3 RID: 6835 RVA: 0x004F7C40 File Offset: 0x004F5E40
		[Conditional("DEBUG")]
		public static void Replace(string key, ref int value)
		{
			double num;
			if (!DebugOverrides.Overrides.TryGetValue(key, out num))
			{
				return;
			}
			value = (int)num;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x004F7C64 File Offset: 0x004F5E64
		[Conditional("DEBUG")]
		public static void Replace(string key, ref float value)
		{
			double num;
			if (!DebugOverrides.Overrides.TryGetValue(key, out num))
			{
				return;
			}
			value = (float)num;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x004F7C85 File Offset: 0x004F5E85
		[Conditional("DEBUG")]
		public static void Set(string key, double value)
		{
			DebugOverrides.Overrides[key] = value;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0000357B File Offset: 0x0000177B
		public DebugOverrides()
		{
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x004F7C93 File Offset: 0x004F5E93
		// Note: this type is marked as 'beforefieldinit'.
		static DebugOverrides()
		{
		}

		// Token: 0x04001510 RID: 5392
		public static Dictionary<string, double> Overrides = new Dictionary<string, double>();
	}
}
