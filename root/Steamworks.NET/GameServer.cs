using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x02000190 RID: 400
	public static class GameServer
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
		public static ESteamAPIInitResult InitEx(uint unIP, ushort usGamePort, ushort usQueryPort, EServerMode eServerMode, string pchVersionString, out string OutSteamErrMsg)
		{
			InteropHelp.TestIfPlatformSupported();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SteamUtils010").Append('\0');
			stringBuilder.Append("SteamNetworkingUtils004").Append('\0');
			stringBuilder.Append("SteamGameServer015").Append('\0');
			stringBuilder.Append("SteamGameServerStats001").Append('\0');
			stringBuilder.Append("STEAMHTTP_INTERFACE_VERSION003").Append('\0');
			stringBuilder.Append("STEAMINVENTORY_INTERFACE_V003").Append('\0');
			stringBuilder.Append("SteamNetworking006").Append('\0');
			stringBuilder.Append("SteamNetworkingMessages002").Append('\0');
			stringBuilder.Append("SteamNetworkingSockets012").Append('\0');
			stringBuilder.Append("STEAMUGC_INTERFACE_VERSION020").Append('\0');
			ESteamAPIInitResult esteamAPIInitResult2;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchVersionString))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(stringBuilder.ToString()))
				{
					IntPtr intPtr = Marshal.AllocHGlobal(1024);
					ESteamAPIInitResult esteamAPIInitResult = NativeMethods.SteamInternal_GameServer_Init_V2(unIP, usGamePort, usQueryPort, eServerMode, utf8StringHandle, utf8StringHandle2, intPtr);
					OutSteamErrMsg = InteropHelp.PtrToStringUTF8(intPtr);
					Marshal.FreeHGlobal(intPtr);
					if (esteamAPIInitResult == ESteamAPIInitResult.k_ESteamAPIInitResult_OK)
					{
						if (CSteamGameServerAPIContext.Init())
						{
							CallbackDispatcher.Initialize();
						}
						else
						{
							esteamAPIInitResult = ESteamAPIInitResult.k_ESteamAPIInitResult_FailedGeneric;
							OutSteamErrMsg = "[Steamworks.NET] Failed to initialize CSteamAPIContext";
						}
					}
					esteamAPIInitResult2 = esteamAPIInitResult;
				}
			}
			return esteamAPIInitResult2;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0000DE08 File Offset: 0x0000C008
		public static bool Init(uint unIP, ushort usGamePort, ushort usQueryPort, EServerMode eServerMode, string pchVersionString)
		{
			InteropHelp.TestIfPlatformSupported();
			string text;
			return GameServer.InitEx(unIP, usGamePort, usQueryPort, eServerMode, pchVersionString, out text) == ESteamAPIInitResult.k_ESteamAPIInitResult_OK;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0000DE2A File Offset: 0x0000C02A
		public static void Shutdown()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamGameServer_Shutdown();
			CSteamGameServerAPIContext.Clear();
			CallbackDispatcher.Shutdown();
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0000DE40 File Offset: 0x0000C040
		public static void RunCallbacks()
		{
			CallbackDispatcher.RunFrame(true);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0000DE48 File Offset: 0x0000C048
		public static void ReleaseCurrentThreadMemory()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamGameServer_ReleaseCurrentThreadMemory();
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0000DE54 File Offset: 0x0000C054
		public static bool BSecure()
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamGameServer_BSecure();
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0000DE60 File Offset: 0x0000C060
		public static CSteamID GetSteamID()
		{
			InteropHelp.TestIfPlatformSupported();
			return (CSteamID)NativeMethods.SteamGameServer_GetSteamID();
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0000DE71 File Offset: 0x0000C071
		public static HSteamPipe GetHSteamPipe()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamPipe)NativeMethods.SteamGameServer_GetHSteamPipe();
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0000DE82 File Offset: 0x0000C082
		public static HSteamUser GetHSteamUser()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamUser)NativeMethods.SteamGameServer_GetHSteamUser();
		}
	}
}
