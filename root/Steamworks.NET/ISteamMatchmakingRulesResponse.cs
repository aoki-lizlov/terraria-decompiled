using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018D RID: 397
	public class ISteamMatchmakingRulesResponse
	{
		// Token: 0x06000906 RID: 2310 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		public ISteamMatchmakingRulesResponse(ISteamMatchmakingRulesResponse.RulesResponded onRulesResponded, ISteamMatchmakingRulesResponse.RulesFailedToRespond onRulesFailedToRespond, ISteamMatchmakingRulesResponse.RulesRefreshComplete onRulesRefreshComplete)
		{
			if (onRulesResponded == null || onRulesFailedToRespond == null || onRulesRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_RulesResponded = onRulesResponded;
			this.m_RulesFailedToRespond = onRulesFailedToRespond;
			this.m_RulesRefreshComplete = onRulesRefreshComplete;
			this.m_VTable = new ISteamMatchmakingRulesResponse.VTable
			{
				m_VTRulesResponded = new ISteamMatchmakingRulesResponse.InternalRulesResponded(this.InternalOnRulesResponded),
				m_VTRulesFailedToRespond = new ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond(this.InternalOnRulesFailedToRespond),
				m_VTRulesRefreshComplete = new ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete(this.InternalOnRulesRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingRulesResponse.VTable)));
			Marshal.StructureToPtr(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, 3);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		~ISteamMatchmakingRulesResponse()
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

		// Token: 0x06000908 RID: 2312 RVA: 0x0000D904 File Offset: 0x0000BB04
		private void InternalOnRulesResponded(IntPtr thisptr, IntPtr pchRule, IntPtr pchValue)
		{
			this.m_RulesResponded(InteropHelp.PtrToStringUTF8(pchRule), InteropHelp.PtrToStringUTF8(pchValue));
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0000D91D File Offset: 0x0000BB1D
		private void InternalOnRulesFailedToRespond(IntPtr thisptr)
		{
			this.m_RulesFailedToRespond();
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0000D92A File Offset: 0x0000BB2A
		private void InternalOnRulesRefreshComplete(IntPtr thisptr)
		{
			this.m_RulesRefreshComplete();
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0000D937 File Offset: 0x0000BB37
		public static explicit operator IntPtr(ISteamMatchmakingRulesResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000A6A RID: 2666
		private ISteamMatchmakingRulesResponse.VTable m_VTable;

		// Token: 0x04000A6B RID: 2667
		private IntPtr m_pVTable;

		// Token: 0x04000A6C RID: 2668
		private GCHandle m_pGCHandle;

		// Token: 0x04000A6D RID: 2669
		private ISteamMatchmakingRulesResponse.RulesResponded m_RulesResponded;

		// Token: 0x04000A6E RID: 2670
		private ISteamMatchmakingRulesResponse.RulesFailedToRespond m_RulesFailedToRespond;

		// Token: 0x04000A6F RID: 2671
		private ISteamMatchmakingRulesResponse.RulesRefreshComplete m_RulesRefreshComplete;

		// Token: 0x020001E7 RID: 487
		// (Invoke) Token: 0x06000BEF RID: 3055
		public delegate void RulesResponded(string pchRule, string pchValue);

		// Token: 0x020001E8 RID: 488
		// (Invoke) Token: 0x06000BF3 RID: 3059
		public delegate void RulesFailedToRespond();

		// Token: 0x020001E9 RID: 489
		// (Invoke) Token: 0x06000BF7 RID: 3063
		public delegate void RulesRefreshComplete();

		// Token: 0x020001EA RID: 490
		// (Invoke) Token: 0x06000BFB RID: 3067
		[UnmanagedFunctionPointer(4)]
		public delegate void InternalRulesResponded(IntPtr thisptr, IntPtr pchRule, IntPtr pchValue);

		// Token: 0x020001EB RID: 491
		// (Invoke) Token: 0x06000BFF RID: 3071
		[UnmanagedFunctionPointer(4)]
		public delegate void InternalRulesFailedToRespond(IntPtr thisptr);

		// Token: 0x020001EC RID: 492
		// (Invoke) Token: 0x06000C03 RID: 3075
		[UnmanagedFunctionPointer(4)]
		public delegate void InternalRulesRefreshComplete(IntPtr thisptr);

		// Token: 0x020001ED RID: 493
		[StructLayout(0)]
		private class VTable
		{
			// Token: 0x06000C06 RID: 3078 RVA: 0x0000CD9E File Offset: 0x0000AF9E
			public VTable()
			{
			}

			// Token: 0x04000B52 RID: 2898
			[NonSerialized]
			[MarshalAs(38)]
			public ISteamMatchmakingRulesResponse.InternalRulesResponded m_VTRulesResponded;

			// Token: 0x04000B53 RID: 2899
			[NonSerialized]
			[MarshalAs(38)]
			public ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond m_VTRulesFailedToRespond;

			// Token: 0x04000B54 RID: 2900
			[NonSerialized]
			[MarshalAs(38)]
			public ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete m_VTRulesRefreshComplete;
		}
	}
}
