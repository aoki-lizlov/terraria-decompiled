using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Steamworks
{
	// Token: 0x02000184 RID: 388
	public sealed class CallResult<T> : CallResult, IDisposable
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060008D7 RID: 2263 RVA: 0x0000CF3C File Offset: 0x0000B13C
		// (remove) Token: 0x060008D8 RID: 2264 RVA: 0x0000CF74 File Offset: 0x0000B174
		private event CallResult<T>.APIDispatchDelegate m_Func
		{
			[CompilerGenerated]
			add
			{
				CallResult<T>.APIDispatchDelegate apidispatchDelegate = this.m_Func;
				CallResult<T>.APIDispatchDelegate apidispatchDelegate2;
				do
				{
					apidispatchDelegate2 = apidispatchDelegate;
					CallResult<T>.APIDispatchDelegate apidispatchDelegate3 = (CallResult<T>.APIDispatchDelegate)Delegate.Combine(apidispatchDelegate2, value);
					apidispatchDelegate = Interlocked.CompareExchange<CallResult<T>.APIDispatchDelegate>(ref this.m_Func, apidispatchDelegate3, apidispatchDelegate2);
				}
				while (apidispatchDelegate != apidispatchDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				CallResult<T>.APIDispatchDelegate apidispatchDelegate = this.m_Func;
				CallResult<T>.APIDispatchDelegate apidispatchDelegate2;
				do
				{
					apidispatchDelegate2 = apidispatchDelegate;
					CallResult<T>.APIDispatchDelegate apidispatchDelegate3 = (CallResult<T>.APIDispatchDelegate)Delegate.Remove(apidispatchDelegate2, value);
					apidispatchDelegate = Interlocked.CompareExchange<CallResult<T>.APIDispatchDelegate>(ref this.m_Func, apidispatchDelegate3, apidispatchDelegate2);
				}
				while (apidispatchDelegate != apidispatchDelegate2);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0000CFA9 File Offset: 0x0000B1A9
		public SteamAPICall_t Handle
		{
			get
			{
				return this.m_hAPICall;
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0000CFB1 File Offset: 0x0000B1B1
		public static CallResult<T> Create(CallResult<T>.APIDispatchDelegate func = null)
		{
			return new CallResult<T>(func);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0000CFB9 File Offset: 0x0000B1B9
		public CallResult(CallResult<T>.APIDispatchDelegate func = null)
		{
			this.m_Func = func;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		~CallResult()
		{
			this.Dispose();
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0000D000 File Offset: 0x0000B200
		public void Dispose()
		{
			if (this.m_bDisposed)
			{
				return;
			}
			GC.SuppressFinalize(this);
			this.Cancel();
			this.m_bDisposed = true;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0000D020 File Offset: 0x0000B220
		public void Set(SteamAPICall_t hAPICall, CallResult<T>.APIDispatchDelegate func = null)
		{
			if (func != null)
			{
				this.m_Func = func;
			}
			if (this.m_Func == null)
			{
				throw new Exception("CallResult function was null, you must either set it in the CallResult Constructor or via Set()");
			}
			if (this.m_hAPICall != SteamAPICall_t.Invalid)
			{
				CallbackDispatcher.Unregister(this.m_hAPICall, this);
			}
			this.m_hAPICall = hAPICall;
			if (hAPICall != SteamAPICall_t.Invalid)
			{
				CallbackDispatcher.Register(hAPICall, this);
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0000D083 File Offset: 0x0000B283
		public bool IsActive()
		{
			return this.m_hAPICall != SteamAPICall_t.Invalid;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0000D095 File Offset: 0x0000B295
		public void Cancel()
		{
			if (this.IsActive())
			{
				CallbackDispatcher.Unregister(this.m_hAPICall, this);
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0000D0AB File Offset: 0x0000B2AB
		internal override Type GetCallbackType()
		{
			return typeof(T);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		internal override void OnRunCallResult(IntPtr pvParam, bool bFailed, ulong hSteamAPICall_)
		{
			if ((SteamAPICall_t)hSteamAPICall_ == this.m_hAPICall)
			{
				try
				{
					this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))), bFailed);
				}
				catch (Exception ex)
				{
					CallbackDispatcher.ExceptionHandler(ex);
				}
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0000D114 File Offset: 0x0000B314
		internal override void SetUnregistered()
		{
			this.m_hAPICall = SteamAPICall_t.Invalid;
		}

		// Token: 0x04000A53 RID: 2643
		[CompilerGenerated]
		private CallResult<T>.APIDispatchDelegate m_Func;

		// Token: 0x04000A54 RID: 2644
		private SteamAPICall_t m_hAPICall = SteamAPICall_t.Invalid;

		// Token: 0x04000A55 RID: 2645
		private bool m_bDisposed;

		// Token: 0x020001D1 RID: 465
		// (Invoke) Token: 0x06000BA3 RID: 2979
		public delegate void APIDispatchDelegate(T param, bool bIOFailure);
	}
}
