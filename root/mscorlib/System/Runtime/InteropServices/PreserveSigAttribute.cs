using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006D0 RID: 1744
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class PreserveSigAttribute : Attribute
	{
		// Token: 0x06004025 RID: 16421 RVA: 0x000E0BF1 File Offset: 0x000DEDF1
		internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) == MethodImplAttributes.IL)
			{
				return null;
			}
			return new PreserveSigAttribute();
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x000E0C08 File Offset: 0x000DEE08
		internal static bool IsDefined(RuntimeMethodInfo method)
		{
			return (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x00002050 File Offset: 0x00000250
		public PreserveSigAttribute()
		{
		}
	}
}
