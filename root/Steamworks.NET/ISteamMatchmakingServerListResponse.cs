using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018A RID: 394
	public class ISteamMatchmakingServerListResponse
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x0000D3AC File Offset: 0x0000B5AC
		public ISteamMatchmakingServerListResponse(ISteamMatchmakingServerListResponse.ServerResponded onServerResponded, ISteamMatchmakingServerListResponse.ServerFailedToRespond onServerFailedToRespond, ISteamMatchmakingServerListResponse.RefreshComplete onRefreshComplete)
		{
			if (onServerResponded == null || onServerFailedToRespond == null || onRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_ServerResponded = onServerResponded;
			this.m_ServerFailedToRespond = onServerFailedToRespond;
			this.m_RefreshComplete = onRefreshComplete;
			this.m_VTable = new ISteamMatchmakingServerListResponse.VTable
			{
				m_VTServerResponded = new ISteamMatchmakingServerListResponse.InternalServerResponded(this.InternalOnServerResponded),
				m_VTServerFailedToRespond = new ISteamMatchmakingServerListResponse.InternalServerFailedToRespond(this.InternalOnServerFailedToRespond),
				m_VTRefreshComplete = new ISteamMatchmakingServerListResponse.InternalRefreshComplete(this.InternalOnRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingServerListResponse.VTable)));
			Marshal.StructureToPtr(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, 3);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0000D468 File Offset: 0x0000B668
		~ISteamMatchmakingServerListResponse()
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

		// Token: 0x060008F7 RID: 2295 RVA: 0x0000D4C4 File Offset: 0x0000B6C4
		private void InternalOnServerResponded(IntPtr thisptr, HServerListRequest hRequest, int iServer)
		{
			try
			{
				this.m_ServerResponded(hRequest, iServer);
			}
			catch (Exception ex)
			{
				CallbackDispatcher.ExceptionHandler(ex);
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		private void InternalOnServerFailedToRespond(IntPtr thisptr, HServerListRequest hRequest, int iServer)
		{
			try
			{
				this.m_ServerFailedToRespond(hRequest, iServer);
			}
			catch (Exception ex)
			{
				CallbackDispatcher.ExceptionHandler(ex);
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0000D52C File Offset: 0x0000B72C
		private void InternalOnRefreshComplete(IntPtr thisptr, HServerListRequest hRequest, EMatchMakingServerResponse response)
		{
			try
			{
				this.m_RefreshComplete(hRequest, response);
			}
			catch (Exception ex)
			{
				CallbackDispatcher.ExceptionHandler(ex);
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0000D560 File Offset: 0x0000B760
		public static explicit operator IntPtr(ISteamMatchmakingServerListResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000A59 RID: 2649
		private ISteamMatchmakingServerListResponse.VTable m_VTable;

		// Token: 0x04000A5A RID: 2650
		private IntPtr m_pVTable;

		// Token: 0x04000A5B RID: 2651
		private GCHandle m_pGCHandle;

		// Token: 0x04000A5C RID: 2652
		private ISteamMatchmakingServerListResponse.ServerResponded m_ServerResponded;

		// Token: 0x04000A5D RID: 2653
		private ISteamMatchmakingServerListResponse.ServerFailedToRespond m_ServerFailedToRespond;

		// Token: 0x04000A5E RID: 2654
		private ISteamMatchmakingServerListResponse.RefreshComplete m_RefreshComplete;

		// Token: 0x020001D4 RID: 468
		// (Invoke) Token: 0x06000BAC RID: 2988
		public delegate void ServerResponded(HServerListRequest hRequest, int iServer);

		// Token: 0x020001D5 RID: 469
		// (Invoke) Token: 0x06000BB0 RID: 2992
		public delegate void ServerFailedToRespond(HServerListRequest hRequest, int iServer);

		// Token: 0x020001D6 RID: 470
		// (Invoke) Token: 0x06000BB4 RID: 2996
		public delegate void RefreshComplete(HServerListRequest hRequest, EMatchMakingServerResponse response);

		// Token: 0x020001D7 RID: 471
		// (Invoke) Token: 0x06000BB8 RID: 3000
		[UnmanagedFunctionPointer(4)]
		private delegate void InternalServerResponded(IntPtr thisptr, HServerListRequest hRequest, int iServer);

		// Token: 0x020001D8 RID: 472
		// (Invoke) Token: 0x06000BBC RID: 3004
		[UnmanagedFunctionPointer(4)]
		private delegate void InternalServerFailedToRespond(IntPtr thisptr, HServerListRequest hRequest, int iServer);

		// Token: 0x020001D9 RID: 473
		// (Invoke) Token: 0x06000BC0 RID: 3008
		[UnmanagedFunctionPointer(4)]
		private delegate void InternalRefreshComplete(IntPtr thisptr, HServerListRequest hRequest, EMatchMakingServerResponse response);

		// Token: 0x020001DA RID: 474
		[StructLayout(0)]
		private class VTable
		{
			// Token: 0x06000BC3 RID: 3011 RVA: 0x0000CD9E File Offset: 0x0000AF9E
			public VTable()
			{
			}

			// Token: 0x04000B4A RID: 2890
			[NonSerialized]
			[MarshalAs(38)]
			public ISteamMatchmakingServerListResponse.InternalServerResponded m_VTServerResponded;

			// Token: 0x04000B4B RID: 2891
			[NonSerialized]
			[MarshalAs(38)]
			public ISteamMatchmakingServerListResponse.InternalServerFailedToRespond m_VTServerFailedToRespond;

			// Token: 0x04000B4C RID: 2892
			[NonSerialized]
			[MarshalAs(38)]
			public ISteamMatchmakingServerListResponse.InternalRefreshComplete m_VTRefreshComplete;
		}
	}
}
