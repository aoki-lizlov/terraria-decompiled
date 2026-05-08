using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018B RID: 395
	public class ISteamMatchmakingPingResponse
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x0000D570 File Offset: 0x0000B770
		public ISteamMatchmakingPingResponse(ISteamMatchmakingPingResponse.ServerResponded onServerResponded, ISteamMatchmakingPingResponse.ServerFailedToRespond onServerFailedToRespond)
		{
			if (onServerResponded == null || onServerFailedToRespond == null)
			{
				throw new ArgumentNullException();
			}
			this.m_ServerResponded = onServerResponded;
			this.m_ServerFailedToRespond = onServerFailedToRespond;
			this.m_VTable = new ISteamMatchmakingPingResponse.VTable
			{
				m_VTServerResponded = new ISteamMatchmakingPingResponse.InternalServerResponded(this.InternalOnServerResponded),
				m_VTServerFailedToRespond = new ISteamMatchmakingPingResponse.InternalServerFailedToRespond(this.InternalOnServerFailedToRespond)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingPingResponse.VTable)));
			Marshal.StructureToPtr(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, 3);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0000D610 File Offset: 0x0000B810
		~ISteamMatchmakingPingResponse()
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

		// Token: 0x060008FD RID: 2301 RVA: 0x0000D66C File Offset: 0x0000B86C
		private void InternalOnServerResponded(IntPtr thisptr, gameserveritem_t server)
		{
			this.m_ServerResponded(server);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0000D67A File Offset: 0x0000B87A
		private void InternalOnServerFailedToRespond(IntPtr thisptr)
		{
			this.m_ServerFailedToRespond();
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0000D687 File Offset: 0x0000B887
		public static explicit operator IntPtr(ISteamMatchmakingPingResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000A5F RID: 2655
		private ISteamMatchmakingPingResponse.VTable m_VTable;

		// Token: 0x04000A60 RID: 2656
		private IntPtr m_pVTable;

		// Token: 0x04000A61 RID: 2657
		private GCHandle m_pGCHandle;

		// Token: 0x04000A62 RID: 2658
		private ISteamMatchmakingPingResponse.ServerResponded m_ServerResponded;

		// Token: 0x04000A63 RID: 2659
		private ISteamMatchmakingPingResponse.ServerFailedToRespond m_ServerFailedToRespond;

		// Token: 0x020001DB RID: 475
		// (Invoke) Token: 0x06000BC5 RID: 3013
		public delegate void ServerResponded(gameserveritem_t server);

		// Token: 0x020001DC RID: 476
		// (Invoke) Token: 0x06000BC9 RID: 3017
		public delegate void ServerFailedToRespond();

		// Token: 0x020001DD RID: 477
		// (Invoke) Token: 0x06000BCD RID: 3021
		[UnmanagedFunctionPointer(4)]
		private delegate void InternalServerResponded(IntPtr thisptr, gameserveritem_t server);

		// Token: 0x020001DE RID: 478
		// (Invoke) Token: 0x06000BD1 RID: 3025
		[UnmanagedFunctionPointer(4)]
		private delegate void InternalServerFailedToRespond(IntPtr thisptr);

		// Token: 0x020001DF RID: 479
		[StructLayout(0)]
		private class VTable
		{
			// Token: 0x06000BD4 RID: 3028 RVA: 0x0000CD9E File Offset: 0x0000AF9E
			public VTable()
			{
			}

			// Token: 0x04000B4D RID: 2893
			[NonSerialized]
			[MarshalAs(38)]
			public ISteamMatchmakingPingResponse.InternalServerResponded m_VTServerResponded;

			// Token: 0x04000B4E RID: 2894
			[NonSerialized]
			[MarshalAs(38)]
			public ISteamMatchmakingPingResponse.InternalServerFailedToRespond m_VTServerFailedToRespond;
		}
	}
}
