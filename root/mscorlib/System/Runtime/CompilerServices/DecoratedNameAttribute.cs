using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000803 RID: 2051
	[AttributeUsage(AttributeTargets.All)]
	[ComVisible(false)]
	internal sealed class DecoratedNameAttribute : Attribute
	{
		// Token: 0x06004647 RID: 17991 RVA: 0x00002050 File Offset: 0x00000250
		public DecoratedNameAttribute(string decoratedName)
		{
		}
	}
}
