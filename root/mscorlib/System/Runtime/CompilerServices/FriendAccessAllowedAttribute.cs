using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000807 RID: 2055
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	[FriendAccessAllowed]
	internal sealed class FriendAccessAllowedAttribute : Attribute
	{
		// Token: 0x0600464F RID: 17999 RVA: 0x00002050 File Offset: 0x00000250
		public FriendAccessAllowedAttribute()
		{
		}
	}
}
