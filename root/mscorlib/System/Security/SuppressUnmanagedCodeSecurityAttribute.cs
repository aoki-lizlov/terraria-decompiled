using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x0200039E RID: 926
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class SuppressUnmanagedCodeSecurityAttribute : Attribute
	{
		// Token: 0x06002817 RID: 10263 RVA: 0x00002050 File Offset: 0x00000250
		public SuppressUnmanagedCodeSecurityAttribute()
		{
		}
	}
}
