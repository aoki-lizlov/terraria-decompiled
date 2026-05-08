using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Terraria.Testing.ChatCommands
{
	// Token: 0x0200011A RID: 282
	public static class ArgumentHelper
	{
		// Token: 0x06001B50 RID: 6992 RVA: 0x004FAA9C File Offset: 0x004F8C9C
		public static ArgumentListResult ParseList(string arguments)
		{
			return new ArgumentListResult(from arg in arguments.Split(new char[] { ' ' })
				select arg.Trim() into arg
				where arg.Length != 0
				select arg);
		}

		// Token: 0x0200072E RID: 1838
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600408E RID: 16526 RVA: 0x0069E9A0 File Offset: 0x0069CBA0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600408F RID: 16527 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004090 RID: 16528 RVA: 0x0069E9AC File Offset: 0x0069CBAC
			internal string <ParseList>b__0_0(string arg)
			{
				return arg.Trim();
			}

			// Token: 0x06004091 RID: 16529 RVA: 0x0069E9B4 File Offset: 0x0069CBB4
			internal bool <ParseList>b__0_1(string arg)
			{
				return arg.Length != 0;
			}

			// Token: 0x04006995 RID: 27029
			public static readonly ArgumentHelper.<>c <>9 = new ArgumentHelper.<>c();

			// Token: 0x04006996 RID: 27030
			public static Func<string, string> <>9__0_0;

			// Token: 0x04006997 RID: 27031
			public static Func<string, bool> <>9__0_1;
		}
	}
}
