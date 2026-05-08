using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x020001AF RID: 431
	// (Invoke) Token: 0x06000A62 RID: 2658
	[UnmanagedFunctionPointer(2)]
	public delegate void FSteamNetworkingSocketsDebugOutput(ESteamNetworkingSocketsDebugOutputType nType, StringBuilder pszMsg);
}
