using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x0200039F RID: 927
	[AttributeUsage(AttributeTargets.Module, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class UnverifiableCodeAttribute : Attribute
	{
		// Token: 0x06002818 RID: 10264 RVA: 0x00002050 File Offset: 0x00000250
		public UnverifiableCodeAttribute()
		{
		}
	}
}
