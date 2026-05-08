using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Steamworks
{
	// Token: 0x02000182 RID: 386
	public sealed class Callback<T> : Callback, IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060008C6 RID: 2246 RVA: 0x0000CDA8 File Offset: 0x0000AFA8
		// (remove) Token: 0x060008C7 RID: 2247 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		private event Callback<T>.DispatchDelegate m_Func
		{
			[CompilerGenerated]
			add
			{
				Callback<T>.DispatchDelegate dispatchDelegate = this.m_Func;
				Callback<T>.DispatchDelegate dispatchDelegate2;
				do
				{
					dispatchDelegate2 = dispatchDelegate;
					Callback<T>.DispatchDelegate dispatchDelegate3 = (Callback<T>.DispatchDelegate)Delegate.Combine(dispatchDelegate2, value);
					dispatchDelegate = Interlocked.CompareExchange<Callback<T>.DispatchDelegate>(ref this.m_Func, dispatchDelegate3, dispatchDelegate2);
				}
				while (dispatchDelegate != dispatchDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				Callback<T>.DispatchDelegate dispatchDelegate = this.m_Func;
				Callback<T>.DispatchDelegate dispatchDelegate2;
				do
				{
					dispatchDelegate2 = dispatchDelegate;
					Callback<T>.DispatchDelegate dispatchDelegate3 = (Callback<T>.DispatchDelegate)Delegate.Remove(dispatchDelegate2, value);
					dispatchDelegate = Interlocked.CompareExchange<Callback<T>.DispatchDelegate>(ref this.m_Func, dispatchDelegate3, dispatchDelegate2);
				}
				while (dispatchDelegate != dispatchDelegate2);
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0000CE15 File Offset: 0x0000B015
		public static Callback<T> Create(Callback<T>.DispatchDelegate func)
		{
			return new Callback<T>(func, false);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0000CE1E File Offset: 0x0000B01E
		public static Callback<T> CreateGameServer(Callback<T>.DispatchDelegate func)
		{
			return new Callback<T>(func, true);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0000CE27 File Offset: 0x0000B027
		public Callback(Callback<T>.DispatchDelegate func, bool bGameServer = false)
		{
			this.m_bGameServer = bGameServer;
			this.Register(func);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0000CE40 File Offset: 0x0000B040
		~Callback()
		{
			this.Dispose();
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0000CE6C File Offset: 0x0000B06C
		public void Dispose()
		{
			if (this.m_bDisposed)
			{
				return;
			}
			GC.SuppressFinalize(this);
			if (this.m_bIsRegistered)
			{
				this.Unregister();
			}
			this.m_bDisposed = true;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0000CE92 File Offset: 0x0000B092
		public void Register(Callback<T>.DispatchDelegate func)
		{
			if (func == null)
			{
				throw new Exception("Callback function must not be null.");
			}
			if (this.m_bIsRegistered)
			{
				this.Unregister();
			}
			this.m_Func = func;
			CallbackDispatcher.Register(this);
			this.m_bIsRegistered = true;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
		public void Unregister()
		{
			CallbackDispatcher.Unregister(this);
			this.m_bIsRegistered = false;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0000CED3 File Offset: 0x0000B0D3
		public override bool IsGameServer
		{
			get
			{
				return this.m_bGameServer;
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0000CEDB File Offset: 0x0000B0DB
		internal override Type GetCallbackType()
		{
			return typeof(T);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0000CEE8 File Offset: 0x0000B0E8
		internal override void OnRunCallback(IntPtr pvParam)
		{
			try
			{
				this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))));
			}
			catch (Exception ex)
			{
				CallbackDispatcher.ExceptionHandler(ex);
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0000CF30 File Offset: 0x0000B130
		internal override void SetUnregistered()
		{
			this.m_bIsRegistered = false;
		}

		// Token: 0x04000A4F RID: 2639
		[CompilerGenerated]
		private Callback<T>.DispatchDelegate m_Func;

		// Token: 0x04000A50 RID: 2640
		private bool m_bGameServer;

		// Token: 0x04000A51 RID: 2641
		private bool m_bIsRegistered;

		// Token: 0x04000A52 RID: 2642
		private bool m_bDisposed;

		// Token: 0x020001D0 RID: 464
		// (Invoke) Token: 0x06000B9F RID: 2975
		public delegate void DispatchDelegate(T param);
	}
}
