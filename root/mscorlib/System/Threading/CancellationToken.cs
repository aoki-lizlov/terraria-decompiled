using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x02000274 RID: 628
	[DebuggerDisplay("IsCancellationRequested = {IsCancellationRequested}")]
	public readonly struct CancellationToken
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0006F674 File Offset: 0x0006D874
		public static CancellationToken None
		{
			get
			{
				return default(CancellationToken);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0006F68A File Offset: 0x0006D88A
		public bool IsCancellationRequested
		{
			get
			{
				return this._source != null && this._source.IsCancellationRequested;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x0006F6A1 File Offset: 0x0006D8A1
		public bool CanBeCanceled
		{
			get
			{
				return this._source != null;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x0006F6AC File Offset: 0x0006D8AC
		public WaitHandle WaitHandle
		{
			get
			{
				return (this._source ?? CancellationTokenSource.s_neverCanceledSource).WaitHandle;
			}
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0006F6C2 File Offset: 0x0006D8C2
		internal CancellationToken(CancellationTokenSource source)
		{
			this._source = source;
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0006F6CB File Offset: 0x0006D8CB
		public CancellationToken(bool canceled)
		{
			this = new CancellationToken(canceled ? CancellationTokenSource.s_canceledSource : null);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x0006F6DE File Offset: 0x0006D8DE
		public CancellationTokenRegistration Register(Action callback)
		{
			Action<object> action = CancellationToken.s_actionToActionObjShunt;
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(action, callback, false, true);
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0006F6FD File Offset: 0x0006D8FD
		public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext)
		{
			Action<object> action = CancellationToken.s_actionToActionObjShunt;
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(action, callback, useSynchronizationContext, true);
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x0006F71C File Offset: 0x0006D91C
		public CancellationTokenRegistration Register(Action<object> callback, object state)
		{
			return this.Register(callback, state, false, true);
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0006F728 File Offset: 0x0006D928
		public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext)
		{
			return this.Register(callback, state, useSynchronizationContext, true);
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0006F734 File Offset: 0x0006D934
		internal CancellationTokenRegistration InternalRegisterWithoutEC(Action<object> callback, object state)
		{
			return this.Register(callback, state, false, false);
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0006F740 File Offset: 0x0006D940
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext, bool useExecutionContext)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			CancellationTokenSource source = this._source;
			if (source == null)
			{
				return default(CancellationTokenRegistration);
			}
			return source.InternalRegister(callback, state, useSynchronizationContext ? SynchronizationContext.Current : null, useExecutionContext ? ExecutionContext.Capture() : null);
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x0006F78E File Offset: 0x0006D98E
		public bool Equals(CancellationToken other)
		{
			return this._source == other._source;
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x0006F79E File Offset: 0x0006D99E
		public override bool Equals(object other)
		{
			return other is CancellationToken && this.Equals((CancellationToken)other);
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x0006F7B6 File Offset: 0x0006D9B6
		public override int GetHashCode()
		{
			return (this._source ?? CancellationTokenSource.s_neverCanceledSource).GetHashCode();
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x0006F7CC File Offset: 0x0006D9CC
		public static bool operator ==(CancellationToken left, CancellationToken right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0006F7D6 File Offset: 0x0006D9D6
		public static bool operator !=(CancellationToken left, CancellationToken right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x0006F7E3 File Offset: 0x0006D9E3
		public void ThrowIfCancellationRequested()
		{
			if (this.IsCancellationRequested)
			{
				this.ThrowOperationCanceledException();
			}
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x0006F7F3 File Offset: 0x0006D9F3
		private void ThrowOperationCanceledException()
		{
			throw new OperationCanceledException("The operation was canceled.", this);
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0006F805 File Offset: 0x0006DA05
		// Note: this type is marked as 'beforefieldinit'.
		static CancellationToken()
		{
		}

		// Token: 0x0400193B RID: 6459
		private readonly CancellationTokenSource _source;

		// Token: 0x0400193C RID: 6460
		private static readonly Action<object> s_actionToActionObjShunt = delegate(object obj)
		{
			((Action)obj)();
		};

		// Token: 0x02000275 RID: 629
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001DB1 RID: 7601 RVA: 0x0006F81C File Offset: 0x0006DA1C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001DB2 RID: 7602 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06001DB3 RID: 7603 RVA: 0x0006F828 File Offset: 0x0006DA28
			internal void <.cctor>b__26_0(object obj)
			{
				((Action)obj)();
			}

			// Token: 0x0400193D RID: 6461
			public static readonly CancellationToken.<>c <>9 = new CancellationToken.<>c();
		}
	}
}
