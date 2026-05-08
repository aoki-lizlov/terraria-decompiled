using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006E2 RID: 1762
	[Obsolete("This attribute has been deprecated.  Application Domains no longer respect Activation Context boundaries in IDispatch calls.", false)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class SetWin32ContextInIDispatchAttribute : Attribute
	{
		// Token: 0x0600405A RID: 16474 RVA: 0x00002050 File Offset: 0x00000250
		public SetWin32ContextInIDispatchAttribute()
		{
		}
	}
}
