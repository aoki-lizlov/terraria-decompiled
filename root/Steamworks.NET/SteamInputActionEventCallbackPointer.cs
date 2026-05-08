using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A5 RID: 421
	// (Invoke) Token: 0x06000A1A RID: 2586
	[UnmanagedFunctionPointer(2)]
	public delegate void SteamInputActionEventCallbackPointer(IntPtr SteamInputActionEvent);
}
