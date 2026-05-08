using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000641 RID: 1601
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class OnDeserializedAttribute : Attribute
	{
		// Token: 0x06003D00 RID: 15616 RVA: 0x00002050 File Offset: 0x00000250
		public OnDeserializedAttribute()
		{
		}
	}
}
