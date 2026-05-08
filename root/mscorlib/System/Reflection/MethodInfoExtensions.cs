using System;

namespace System.Reflection
{
	// Token: 0x020008A8 RID: 2216
	public static class MethodInfoExtensions
	{
		// Token: 0x06004AE1 RID: 19169 RVA: 0x000F0270 File Offset: 0x000EE470
		public static MethodInfo GetBaseDefinition(MethodInfo method)
		{
			Requires.NotNull(method, "method");
			return method.GetBaseDefinition();
		}
	}
}
