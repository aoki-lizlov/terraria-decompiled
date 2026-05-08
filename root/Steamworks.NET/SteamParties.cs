using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000017 RID: 23
	public static class SteamParties
	{
		// Token: 0x060002EC RID: 748 RVA: 0x000085E1 File Offset: 0x000067E1
		public static uint GetNumActiveBeacons()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamParties_GetNumActiveBeacons(CSteamAPIContext.GetSteamParties());
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000085F2 File Offset: 0x000067F2
		public static PartyBeaconID_t GetBeaconByIndex(uint unIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (PartyBeaconID_t)NativeMethods.ISteamParties_GetBeaconByIndex(CSteamAPIContext.GetSteamParties(), unIndex);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000860C File Offset: 0x0000680C
		public static bool GetBeaconDetails(PartyBeaconID_t ulBeaconID, out CSteamID pSteamIDBeaconOwner, out SteamPartyBeaconLocation_t pLocation, out string pchMetadata, int cchMetadata)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchMetadata);
			bool flag = NativeMethods.ISteamParties_GetBeaconDetails(CSteamAPIContext.GetSteamParties(), ulBeaconID, out pSteamIDBeaconOwner, out pLocation, intPtr, cchMetadata);
			pchMetadata = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000864C File Offset: 0x0000684C
		public static SteamAPICall_t JoinParty(PartyBeaconID_t ulBeaconID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamParties_JoinParty(CSteamAPIContext.GetSteamParties(), ulBeaconID);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00008663 File Offset: 0x00006863
		public static bool GetNumAvailableBeaconLocations(out uint puNumLocations)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamParties_GetNumAvailableBeaconLocations(CSteamAPIContext.GetSteamParties(), out puNumLocations);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00008675 File Offset: 0x00006875
		public static bool GetAvailableBeaconLocations(SteamPartyBeaconLocation_t[] pLocationList, uint uMaxNumLocations)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamParties_GetAvailableBeaconLocations(CSteamAPIContext.GetSteamParties(), pLocationList, uMaxNumLocations);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00008688 File Offset: 0x00006888
		public static SteamAPICall_t CreateBeacon(uint unOpenSlots, ref SteamPartyBeaconLocation_t pBeaconLocation, string pchConnectString, string pchMetadata)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t steamAPICall_t;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchConnectString))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchMetadata))
				{
					steamAPICall_t = (SteamAPICall_t)NativeMethods.ISteamParties_CreateBeacon(CSteamAPIContext.GetSteamParties(), unOpenSlots, ref pBeaconLocation, utf8StringHandle, utf8StringHandle2);
				}
			}
			return steamAPICall_t;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000086F0 File Offset: 0x000068F0
		public static void OnReservationCompleted(PartyBeaconID_t ulBeacon, CSteamID steamIDUser)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamParties_OnReservationCompleted(CSteamAPIContext.GetSteamParties(), ulBeacon, steamIDUser);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00008703 File Offset: 0x00006903
		public static void CancelReservation(PartyBeaconID_t ulBeacon, CSteamID steamIDUser)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamParties_CancelReservation(CSteamAPIContext.GetSteamParties(), ulBeacon, steamIDUser);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00008716 File Offset: 0x00006916
		public static SteamAPICall_t ChangeNumOpenSlots(PartyBeaconID_t ulBeacon, uint unOpenSlots)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamParties_ChangeNumOpenSlots(CSteamAPIContext.GetSteamParties(), ulBeacon, unOpenSlots);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000872E File Offset: 0x0000692E
		public static bool DestroyBeacon(PartyBeaconID_t ulBeacon)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamParties_DestroyBeacon(CSteamAPIContext.GetSteamParties(), ulBeacon);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00008740 File Offset: 0x00006940
		public static bool GetBeaconLocationData(SteamPartyBeaconLocation_t BeaconLocation, ESteamPartyBeaconLocationData eData, out string pchDataStringOut, int cchDataStringOut)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchDataStringOut);
			bool flag = NativeMethods.ISteamParties_GetBeaconLocationData(CSteamAPIContext.GetSteamParties(), BeaconLocation, eData, intPtr, cchDataStringOut);
			pchDataStringOut = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}
	}
}
