using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006D3 RID: 1747
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	public sealed class OptionalAttribute : Attribute
	{
		// Token: 0x0600402E RID: 16430 RVA: 0x000E0C4B File Offset: 0x000DEE4B
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsOptional)
			{
				return null;
			}
			return new OptionalAttribute();
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x000E0C5C File Offset: 0x000DEE5C
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsOptional;
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x00002050 File Offset: 0x00000250
		public OptionalAttribute()
		{
		}
	}
}
