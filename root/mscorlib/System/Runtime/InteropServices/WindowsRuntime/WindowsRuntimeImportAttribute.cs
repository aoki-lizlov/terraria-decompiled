using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000753 RID: 1875
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[FriendAccessAllowed]
	internal sealed class WindowsRuntimeImportAttribute : Attribute
	{
		// Token: 0x06004412 RID: 17426 RVA: 0x00002050 File Offset: 0x00000250
		public WindowsRuntimeImportAttribute()
		{
		}
	}
}
