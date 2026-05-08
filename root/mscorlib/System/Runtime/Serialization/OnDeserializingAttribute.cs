using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000640 RID: 1600
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class OnDeserializingAttribute : Attribute
	{
		// Token: 0x06003CFF RID: 15615 RVA: 0x00002050 File Offset: 0x00000250
		public OnDeserializingAttribute()
		{
		}
	}
}
