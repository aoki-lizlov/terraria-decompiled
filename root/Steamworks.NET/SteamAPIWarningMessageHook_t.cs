using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x02000199 RID: 409
	// (Invoke) Token: 0x060009BC RID: 2492
	[UnmanagedFunctionPointer(2)]
	public delegate void SteamAPIWarningMessageHook_t(int nSeverity, StringBuilder pchDebugText);
}
