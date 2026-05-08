using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006D1 RID: 1745
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	public sealed class InAttribute : Attribute
	{
		// Token: 0x06004028 RID: 16424 RVA: 0x000E0C19 File Offset: 0x000DEE19
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsIn)
			{
				return null;
			}
			return new InAttribute();
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x000E0C2A File Offset: 0x000DEE2A
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsIn;
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x00002050 File Offset: 0x00000250
		public InAttribute()
		{
		}
	}
}
