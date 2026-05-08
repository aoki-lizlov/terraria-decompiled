using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018C RID: 396
	public class ISteamMatchmakingPlayersResponse
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x0000D694 File Offset: 0x0000B894
		public ISteamMatchmakingPlayersResponse(ISteamMatchmakingPlayersResponse.AddPlayerToList onAddPlayerToList, ISteamMatchmakingPlayersResponse.PlayersFailedToRespond onPlayersFailedToRespond, ISteamMatchmakingPlayersResponse.PlayersRefreshComplete onPlayersRefreshComplete)
		{
			if (onAddPlayerToList == null || onPlayersFailedToRespond == null || onPlayersRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_AddPlayerToList = onAddPlayerToList;
			this.m_PlayersFailedToRespond = onPlayersFailedToRespond;
			this.m_PlayersRefreshComplete = onPlayersRefreshComplete;
			this.m_VTable = new ISteamMatchmakingPlayersResponse.VTable
			{
				m_VTAddPlayerToList = new ISteamMatchmakingPlayersResponse.InternalAddPlayerToList(this.InternalOnAddPlayerToList),
				m_VTPlayersFailedToRespond = new ISteamMatchmakingPlayersResponse.InternalPlayersFailedToRespond(this.InternalOnPlayersFailedToRespond),
				m_VTPlayersRefreshComplete = new ISteamMatchmakingPlayersResponse.InternalPlayersRefreshComplete(this.InternalOnPlayersRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingPlayersResponse.VTable)));
			Marshal.StructureToPtr(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, 3);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0000D750 File Offset: 0x0000B950
		~ISteamMatchmakingPlayersResponse()
		{
			if (this.m_pVTable != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pVTable);
			}
			if (this.m_pGCHandle.IsAllocated)
			{
				this.m_pGCHandle.Free();
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		private void InternalOnAddPlayerToList(IntPtr thisptr, IntPtr pchName, int nScore, float flTimePlayed)
		{
			this.m_AddPlayerToList(InteropHelp.PtrToStringUTF8(pchName), nScore, flTimePlayed);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0000D7C2 File Offset: 0x0000B9C2
		private void InternalOnPlayersFailedToRespond(IntPtr thisptr)
		{
			this.m_PlayersFailedToRespond();
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0000D7CF File Offset: 0x0000B9CF
		private void InternalOnPlayersRefreshComplete(IntPtr thisptr)
		{
			this.m_PlayersRefreshComplete();
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		public static explicit operator IntPtr(ISteamMatchmakingPlayersResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000A64 RID: 2660
		private ISteamMatchmakingPlayersResponse.VTable m_VTable;

		// Token: 0x04000A65 RID: 2661
		private IntPtr m_pVTable;

		// Token: 0x04000A66 RID: 2662
		private GCHandle m_pGCHandle;

		// Token: 0x04000A67 RID: 2663
		private ISteamMatchmakingPlayersResponse.AddPlayerToList m_AddPlayerToList;

		// Token: 0x04000A68 RID: 2664
		private ISteamMatchmakingPlayersResponse.PlayersFailedToRespond m_PlayersFailedToRespond;

		// Token: 0x04000A69 RID: 2665
		private ISteamMatchmakingPlayersResponse.PlayersRefreshComplete m_PlayersRefreshComplete;

		// Token: 0x020001E0 RID: 480
		// (Invoke) Token: 0x06000BD6 RID: 3030
		public delegate void AddPlayerToList(string pchName, int nScore, float flTimePlayed);

		// Token: 0x020001E1 RID: 481
		// (Invoke) Token: 0x06000BDA RID: 3034
		public delegate void PlayersFailedToRespond();

		// Token: 0x020001E2 RID: 482
		// (Invoke) Token: 0x06000BDE RID: 3038
		public delegate void PlayersRefreshComplete();

		// Token: 0x020001E3 RID: 483
		// (Invoke) Token: 0x06000BE2 RID: 3042
		[UnmanagedFunctionPointer(4)]
		public delegate void InternalAddPlayerToList(IntPtr thisptr, IntPtr pchName, int nScore, float flTimePlayed);

		// Token: 0x020001E4 RID: 484
		// (Invoke) Token: 0x06000BE6 RID: 3046
		[UnmanagedFunctionPointer(4)]
		public delegate void InternalPlayersFailedToRespond(IntPtr thisptr);

		// Token: 0x020001E5 RID: 485
		// (Invoke) Token: 0x06000BEA RID: 3050
		[UnmanagedFunctionPointer(4)]
		public delegate void InternalPlayersRefreshComplete(IntPtr thisptr);

		// Token: 0x020001E6 RID: 486
		[StructLayout(0)]
		private class VTable
		{
			// Token: 0x06000BED RID: 3053 RVA: 0x0000CD9E File Offset: 0x0000AF9E
			public VTable()
			{
			}

			// Token: 0x04000B4F RID: 2895
			[NonSerialized]
			[MarshalAs(38)]
			public ISteamMatchmakingPlayersResponse.InternalAddPlayerToList m_VTAddPlayerToList;

			// Token: 0x04000B50 RID: 2896
			[NonSerialized]
			[MarshalAs(38)]
			public ISteamMatchmakingPlayersResponse.InternalPlayersFailedToRespond m_VTPlayersFailedToRespond;

			// Token: 0x04000B51 RID: 2897
			[NonSerialized]
			[MarshalAs(38)]
			public ISteamMatchmakingPlayersResponse.InternalPlayersRefreshComplete m_VTPlayersRefreshComplete;
		}
	}
}
