using System;

namespace System.Runtime.Versioning
{
	// Token: 0x0200060B RID: 1547
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	internal sealed class NonVersionableAttribute : Attribute
	{
		// Token: 0x06003BB9 RID: 15289 RVA: 0x00002050 File Offset: 0x00000250
		public NonVersionableAttribute()
		{
		}
	}
}
