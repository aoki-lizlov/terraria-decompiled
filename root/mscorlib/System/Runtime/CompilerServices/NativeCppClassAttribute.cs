using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000816 RID: 2070
	[AttributeUsage(AttributeTargets.Struct, Inherited = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class NativeCppClassAttribute : Attribute
	{
		// Token: 0x06004657 RID: 18007 RVA: 0x00002050 File Offset: 0x00000250
		public NativeCppClassAttribute()
		{
		}
	}
}
