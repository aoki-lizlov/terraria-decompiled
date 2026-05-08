using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x0200063E RID: 1598
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class OnSerializingAttribute : Attribute
	{
		// Token: 0x06003CFD RID: 15613 RVA: 0x00002050 File Offset: 0x00000250
		public OnSerializingAttribute()
		{
		}
	}
}
