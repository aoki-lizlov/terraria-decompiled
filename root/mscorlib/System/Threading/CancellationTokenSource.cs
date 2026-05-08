using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x0200027C RID: 636
	public class CancellationTokenSource : IDisposable
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x00070339 File Offset: 0x0006E539
		public bool IsCancellationRequested
		{
			get
			{
				return this._state >= 2;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x00070349 File Offset: 0x0006E549
		internal bool IsCancellationCompleted
		{
			get
			{
				return this._state == 3;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x00070356 File Offset: 0x0006E556
		internal bool IsDisposed
		{
			get
			{
				return this._disposed;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x0007035E File Offset: 0x0006E55E
		// (set) Token: 0x06001DEF RID: 7663 RVA: 0x00070368 File Offset: 0x0006E568
		internal int ThreadIDExecutingCallbacks
		{
			get
			{
				return this._threadIDExecutingCallbacks;
			}
			set
			{
				this._threadIDExecutingCallbacks = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x00070373 File Offset: 0x0006E573
		public CancellationToken Token
		{
			get
			{
				this.ThrowIfDisposed();
				return new CancellationToken(this);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x00070381 File Offset: 0x0006E581
		internal bool CanBeCanceled
		{
			get
			{
				return this._state != 0;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x00070390 File Offset: 0x0006E590
		internal WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				if (this._kernelEvent != null)
				{
					return this._kernelEvent;
				}
				ManualResetEvent manualResetEvent = new ManualResetEvent(false);
				if (Interlocked.CompareExchange<ManualResetEvent>(ref this._kernelEvent, manualResetEvent, null) != null)
				{
					manualResetEvent.Dispose();
				}
				if (this.IsCancellationRequested)
				{
					this._kernelEvent.Set();
				}
				return this._kernelEvent;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x000703F0 File Offset: 0x0006E5F0
		internal CancellationCallbackInfo ExecutingCallback
		{
			get
			{
				return this._executingCallback;
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000703FA File Offset: 0x0006E5FA
		public CancellationTokenSource()
		{
			this._state = 1;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00070414 File Offset: 0x0006E614
		public CancellationTokenSource(TimeSpan delay)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay");
			}
			this.InitializeWithTimer((int)num);
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x0007045A File Offset: 0x0006E65A
		public CancellationTokenSource(int millisecondsDelay)
		{
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay");
			}
			this.InitializeWithTimer(millisecondsDelay);
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x00070481 File Offset: 0x0006E681
		private void InitializeWithTimer(int millisecondsDelay)
		{
			this._state = 1;
			this._timer = new Timer(CancellationTokenSource.s_timerCallback, this, millisecondsDelay, -1);
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000704A1 File Offset: 0x0006E6A1
		public void Cancel()
		{
			this.Cancel(false);
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x000704AA File Offset: 0x0006E6AA
		public void Cancel(bool throwOnFirstException)
		{
			this.ThrowIfDisposed();
			this.NotifyCancellation(throwOnFirstException);
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x000704BC File Offset: 0x0006E6BC
		public void CancelAfter(TimeSpan delay)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay");
			}
			this.CancelAfter((int)num);
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x000704F4 File Offset: 0x0006E6F4
		public void CancelAfter(int millisecondsDelay)
		{
			this.ThrowIfDisposed();
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay");
			}
			if (this.IsCancellationRequested)
			{
				return;
			}
			if (this._timer == null)
			{
				Timer timer = new Timer(CancellationTokenSource.s_timerCallback, this, -1, -1);
				if (Interlocked.CompareExchange<Timer>(ref this._timer, timer, null) != null)
				{
					timer.Dispose();
				}
			}
			try
			{
				this._timer.Change(millisecondsDelay, -1);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x00070574 File Offset: 0x0006E774
		private static void TimerCallbackLogic(object obj)
		{
			CancellationTokenSource cancellationTokenSource = (CancellationTokenSource)obj;
			if (!cancellationTokenSource.IsDisposed)
			{
				try
				{
					cancellationTokenSource.Cancel();
				}
				catch (ObjectDisposedException)
				{
					if (!cancellationTokenSource.IsDisposed)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x000705B8 File Offset: 0x0006E7B8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x000705C8 File Offset: 0x0006E7C8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._disposed)
			{
				Timer timer = this._timer;
				if (timer != null)
				{
					timer.Dispose();
				}
				this._registeredCallbacksLists = null;
				if (this._kernelEvent != null)
				{
					ManualResetEvent manualResetEvent = Interlocked.Exchange<ManualResetEvent>(ref this._kernelEvent, null);
					if (manualResetEvent != null && this._state != 2)
					{
						manualResetEvent.Dispose();
					}
				}
				this._disposed = true;
			}
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x0007062E File Offset: 0x0006E82E
		internal void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				CancellationTokenSource.ThrowObjectDisposedException();
			}
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x0007063D File Offset: 0x0006E83D
		private static void ThrowObjectDisposedException()
		{
			throw new ObjectDisposedException(null, "The CancellationTokenSource has been disposed.");
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x0007064C File Offset: 0x0006E84C
		internal CancellationTokenRegistration InternalRegister(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext executionContext)
		{
			if (!this.IsCancellationRequested)
			{
				if (this._disposed)
				{
					return default(CancellationTokenRegistration);
				}
				int num = Environment.CurrentManagedThreadId % CancellationTokenSource.s_nLists;
				CancellationCallbackInfo cancellationCallbackInfo = ((targetSyncContext != null) ? new CancellationCallbackInfo.WithSyncContext(callback, stateForCallback, executionContext, this, targetSyncContext) : new CancellationCallbackInfo(callback, stateForCallback, executionContext, this));
				SparselyPopulatedArray<CancellationCallbackInfo>[] array = this._registeredCallbacksLists;
				if (array == null)
				{
					SparselyPopulatedArray<CancellationCallbackInfo>[] array2 = new SparselyPopulatedArray<CancellationCallbackInfo>[CancellationTokenSource.s_nLists];
					array = Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>[]>(ref this._registeredCallbacksLists, array2, null);
					if (array == null)
					{
						array = array2;
					}
				}
				SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref array[num]);
				if (sparselyPopulatedArray == null)
				{
					SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray2 = new SparselyPopulatedArray<CancellationCallbackInfo>(4);
					Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>>(ref array[num], sparselyPopulatedArray2, null);
					sparselyPopulatedArray = array[num];
				}
				SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> sparselyPopulatedArrayAddInfo = sparselyPopulatedArray.Add(cancellationCallbackInfo);
				CancellationTokenRegistration cancellationTokenRegistration = new CancellationTokenRegistration(cancellationCallbackInfo, sparselyPopulatedArrayAddInfo);
				if (!this.IsCancellationRequested)
				{
					return cancellationTokenRegistration;
				}
				if (!cancellationTokenRegistration.Unregister())
				{
					return cancellationTokenRegistration;
				}
			}
			callback(stateForCallback);
			return default(CancellationTokenRegistration);
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x00070734 File Offset: 0x0006E934
		private void NotifyCancellation(bool throwOnFirstException)
		{
			if (!this.IsCancellationRequested && Interlocked.CompareExchange(ref this._state, 2, 1) == 1)
			{
				Timer timer = this._timer;
				if (timer != null)
				{
					timer.Dispose();
				}
				this.ThreadIDExecutingCallbacks = Environment.CurrentManagedThreadId;
				ManualResetEvent kernelEvent = this._kernelEvent;
				if (kernelEvent != null)
				{
					kernelEvent.Set();
				}
				this.ExecuteCallbackHandlers(throwOnFirstException);
			}
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x00070794 File Offset: 0x0006E994
		private void ExecuteCallbackHandlers(bool throwOnFirstException)
		{
			LowLevelListWithIList<Exception> lowLevelListWithIList = null;
			SparselyPopulatedArray<CancellationCallbackInfo>[] registeredCallbacksLists = this._registeredCallbacksLists;
			if (registeredCallbacksLists == null)
			{
				Interlocked.Exchange(ref this._state, 3);
				return;
			}
			try
			{
				for (int i = 0; i < registeredCallbacksLists.Length; i++)
				{
					SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref registeredCallbacksLists[i]);
					if (sparselyPopulatedArray != null)
					{
						for (SparselyPopulatedArrayFragment<CancellationCallbackInfo> sparselyPopulatedArrayFragment = sparselyPopulatedArray.Tail; sparselyPopulatedArrayFragment != null; sparselyPopulatedArrayFragment = sparselyPopulatedArrayFragment.Prev)
						{
							for (int j = sparselyPopulatedArrayFragment.Length - 1; j >= 0; j--)
							{
								this._executingCallback = sparselyPopulatedArrayFragment[j];
								if (this._executingCallback != null)
								{
									CancellationCallbackCoreWorkArguments cancellationCallbackCoreWorkArguments = new CancellationCallbackCoreWorkArguments(sparselyPopulatedArrayFragment, j);
									try
									{
										CancellationCallbackInfo.WithSyncContext withSyncContext = this._executingCallback as CancellationCallbackInfo.WithSyncContext;
										if (withSyncContext != null)
										{
											withSyncContext.TargetSyncContext.Send(new SendOrPostCallback(this.CancellationCallbackCoreWork_OnSyncContext), cancellationCallbackCoreWorkArguments);
											this.ThreadIDExecutingCallbacks = Environment.CurrentManagedThreadId;
										}
										else
										{
											this.CancellationCallbackCoreWork(cancellationCallbackCoreWorkArguments);
										}
									}
									catch (Exception ex)
									{
										if (throwOnFirstException)
										{
											throw;
										}
										if (lowLevelListWithIList == null)
										{
											lowLevelListWithIList = new LowLevelListWithIList<Exception>();
										}
										lowLevelListWithIList.Add(ex);
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				this._state = 3;
				this._executingCallback = null;
				Interlocked.MemoryBarrier();
			}
			if (lowLevelListWithIList != null)
			{
				throw new AggregateException(lowLevelListWithIList);
			}
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x000708EC File Offset: 0x0006EAEC
		private void CancellationCallbackCoreWork_OnSyncContext(object obj)
		{
			this.CancellationCallbackCoreWork((CancellationCallbackCoreWorkArguments)obj);
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x000708FC File Offset: 0x0006EAFC
		private void CancellationCallbackCoreWork(CancellationCallbackCoreWorkArguments args)
		{
			CancellationCallbackInfo cancellationCallbackInfo = args._currArrayFragment.SafeAtomicRemove(args._currArrayIndex, this._executingCallback);
			if (cancellationCallbackInfo == this._executingCallback)
			{
				cancellationCallbackInfo.CancellationTokenSource.ThreadIDExecutingCallbacks = Environment.CurrentManagedThreadId;
				cancellationCallbackInfo.ExecuteCallback();
			}
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00070944 File Offset: 0x0006EB44
		public static CancellationTokenSource CreateLinkedTokenSource(CancellationToken token1, CancellationToken token2)
		{
			if (!token1.CanBeCanceled)
			{
				return CancellationTokenSource.CreateLinkedTokenSource(token2);
			}
			if (!token2.CanBeCanceled)
			{
				return new CancellationTokenSource.Linked1CancellationTokenSource(token1);
			}
			return new CancellationTokenSource.Linked2CancellationTokenSource(token1, token2);
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x0007096D File Offset: 0x0006EB6D
		internal static CancellationTokenSource CreateLinkedTokenSource(CancellationToken token)
		{
			if (!token.CanBeCanceled)
			{
				return new CancellationTokenSource();
			}
			return new CancellationTokenSource.Linked1CancellationTokenSource(token);
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00070984 File Offset: 0x0006EB84
		public static CancellationTokenSource CreateLinkedTokenSource(params CancellationToken[] tokens)
		{
			if (tokens == null)
			{
				throw new ArgumentNullException("tokens");
			}
			switch (tokens.Length)
			{
			case 0:
				throw new ArgumentException("No tokens were supplied.");
			case 1:
				return CancellationTokenSource.CreateLinkedTokenSource(tokens[0]);
			case 2:
				return CancellationTokenSource.CreateLinkedTokenSource(tokens[0], tokens[1]);
			default:
				return new CancellationTokenSource.LinkedNCancellationTokenSource(tokens);
			}
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x000709EC File Offset: 0x0006EBEC
		internal void WaitForCallbackToComplete(CancellationCallbackInfo callbackInfo)
		{
			SpinWait spinWait = default(SpinWait);
			while (this.ExecutingCallback == callbackInfo)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00070A14 File Offset: 0x0006EC14
		// Note: this type is marked as 'beforefieldinit'.
		static CancellationTokenSource()
		{
		}

		// Token: 0x0400195D RID: 6493
		internal static readonly CancellationTokenSource s_canceledSource = new CancellationTokenSource
		{
			_state = 3
		};

		// Token: 0x0400195E RID: 6494
		internal static readonly CancellationTokenSource s_neverCanceledSource = new CancellationTokenSource
		{
			_state = 0
		};

		// Token: 0x0400195F RID: 6495
		private static readonly int s_nLists = ((PlatformHelper.ProcessorCount > 24) ? 24 : PlatformHelper.ProcessorCount);

		// Token: 0x04001960 RID: 6496
		private volatile ManualResetEvent _kernelEvent;

		// Token: 0x04001961 RID: 6497
		private volatile SparselyPopulatedArray<CancellationCallbackInfo>[] _registeredCallbacksLists;

		// Token: 0x04001962 RID: 6498
		private const int CannotBeCanceled = 0;

		// Token: 0x04001963 RID: 6499
		private const int NotCanceledState = 1;

		// Token: 0x04001964 RID: 6500
		private const int NotifyingState = 2;

		// Token: 0x04001965 RID: 6501
		private const int NotifyingCompleteState = 3;

		// Token: 0x04001966 RID: 6502
		private volatile int _state;

		// Token: 0x04001967 RID: 6503
		private volatile int _threadIDExecutingCallbacks = -1;

		// Token: 0x04001968 RID: 6504
		private bool _disposed;

		// Token: 0x04001969 RID: 6505
		private volatile CancellationCallbackInfo _executingCallback;

		// Token: 0x0400196A RID: 6506
		private volatile Timer _timer;

		// Token: 0x0400196B RID: 6507
		private static readonly TimerCallback s_timerCallback = new TimerCallback(CancellationTokenSource.TimerCallbackLogic);

		// Token: 0x0200027D RID: 637
		private sealed class Linked1CancellationTokenSource : CancellationTokenSource
		{
			// Token: 0x06001E0B RID: 7691 RVA: 0x00070A6F File Offset: 0x0006EC6F
			internal Linked1CancellationTokenSource(CancellationToken token1)
			{
				this._reg1 = token1.InternalRegisterWithoutEC(CancellationTokenSource.LinkedNCancellationTokenSource.s_linkedTokenCancelDelegate, this);
			}

			// Token: 0x06001E0C RID: 7692 RVA: 0x00070A8A File Offset: 0x0006EC8A
			protected override void Dispose(bool disposing)
			{
				if (!disposing || this._disposed)
				{
					return;
				}
				this._reg1.Dispose();
				base.Dispose(disposing);
			}

			// Token: 0x0400196C RID: 6508
			private readonly CancellationTokenRegistration _reg1;
		}

		// Token: 0x0200027E RID: 638
		private sealed class Linked2CancellationTokenSource : CancellationTokenSource
		{
			// Token: 0x06001E0D RID: 7693 RVA: 0x00070AAA File Offset: 0x0006ECAA
			internal Linked2CancellationTokenSource(CancellationToken token1, CancellationToken token2)
			{
				this._reg1 = token1.InternalRegisterWithoutEC(CancellationTokenSource.LinkedNCancellationTokenSource.s_linkedTokenCancelDelegate, this);
				this._reg2 = token2.InternalRegisterWithoutEC(CancellationTokenSource.LinkedNCancellationTokenSource.s_linkedTokenCancelDelegate, this);
			}

			// Token: 0x06001E0E RID: 7694 RVA: 0x00070AD8 File Offset: 0x0006ECD8
			protected override void Dispose(bool disposing)
			{
				if (!disposing || this._disposed)
				{
					return;
				}
				this._reg1.Dispose();
				this._reg2.Dispose();
				base.Dispose(disposing);
			}

			// Token: 0x0400196D RID: 6509
			private readonly CancellationTokenRegistration _reg1;

			// Token: 0x0400196E RID: 6510
			private readonly CancellationTokenRegistration _reg2;
		}

		// Token: 0x0200027F RID: 639
		private sealed class LinkedNCancellationTokenSource : CancellationTokenSource
		{
			// Token: 0x06001E0F RID: 7695 RVA: 0x00070B04 File Offset: 0x0006ED04
			internal LinkedNCancellationTokenSource(params CancellationToken[] tokens)
			{
				this._linkingRegistrations = new CancellationTokenRegistration[tokens.Length];
				for (int i = 0; i < tokens.Length; i++)
				{
					if (tokens[i].CanBeCanceled)
					{
						this._linkingRegistrations[i] = tokens[i].InternalRegisterWithoutEC(CancellationTokenSource.LinkedNCancellationTokenSource.s_linkedTokenCancelDelegate, this);
					}
				}
			}

			// Token: 0x06001E10 RID: 7696 RVA: 0x00070B60 File Offset: 0x0006ED60
			protected override void Dispose(bool disposing)
			{
				if (!disposing || this._disposed)
				{
					return;
				}
				CancellationTokenRegistration[] linkingRegistrations = this._linkingRegistrations;
				if (linkingRegistrations != null)
				{
					this._linkingRegistrations = null;
					for (int i = 0; i < linkingRegistrations.Length; i++)
					{
						linkingRegistrations[i].Dispose();
					}
				}
				base.Dispose(disposing);
			}

			// Token: 0x06001E11 RID: 7697 RVA: 0x00070BAB File Offset: 0x0006EDAB
			// Note: this type is marked as 'beforefieldinit'.
			static LinkedNCancellationTokenSource()
			{
			}

			// Token: 0x0400196F RID: 6511
			internal static readonly Action<object> s_linkedTokenCancelDelegate = delegate(object s)
			{
				((CancellationTokenSource)s).NotifyCancellation(false);
			};

			// Token: 0x04001970 RID: 6512
			private CancellationTokenRegistration[] _linkingRegistrations;

			// Token: 0x02000280 RID: 640
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06001E12 RID: 7698 RVA: 0x00070BC2 File Offset: 0x0006EDC2
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06001E13 RID: 7699 RVA: 0x000025BE File Offset: 0x000007BE
				public <>c()
				{
				}

				// Token: 0x06001E14 RID: 7700 RVA: 0x00070BCE File Offset: 0x0006EDCE
				internal void <.cctor>b__4_0(object s)
				{
					((CancellationTokenSource)s).NotifyCancellation(false);
				}

				// Token: 0x04001971 RID: 6513
				public static readonly CancellationTokenSource.LinkedNCancellationTokenSource.<>c <>9 = new CancellationTokenSource.LinkedNCancellationTokenSource.<>c();
			}
		}
	}
}
