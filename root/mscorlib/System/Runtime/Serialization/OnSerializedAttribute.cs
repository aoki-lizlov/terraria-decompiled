using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x0200063F RID: 1599
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class OnSerializedAttribute : Attribute
	{
		// Token: 0x06003CFE RID: 15614 RVA: 0x00002050 File Offset: 0x00000250
		public OnSerializedAttribute()
		{
		}
	}
}
