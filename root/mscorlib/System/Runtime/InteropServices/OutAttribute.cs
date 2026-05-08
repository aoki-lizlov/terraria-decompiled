using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006D2 RID: 1746
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	public sealed class OutAttribute : Attribute
	{
		// Token: 0x0600402B RID: 16427 RVA: 0x000E0C32 File Offset: 0x000DEE32
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsOut)
			{
				return null;
			}
			return new OutAttribute();
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x000E0C43 File Offset: 0x000DEE43
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsOut;
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x00002050 File Offset: 0x00000250
		public OutAttribute()
		{
		}
	}
}
