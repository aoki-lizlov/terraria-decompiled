using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000299 RID: 665
	[Serializable]
	public sealed class ExecutionContext : IDisposable, ISerializable
	{
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x00072EBF File Offset: 0x000710BF
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x00072ECC File Offset: 0x000710CC
		internal bool isNewCapture
		{
			get
			{
				return (this._flags & (ExecutionContext.Flags)5) > ExecutionContext.Flags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= ExecutionContext.Flags.IsNewCapture;
					return;
				}
				this._flags &= (ExecutionContext.Flags)(-2);
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x00072EEF File Offset: 0x000710EF
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x00072EFC File Offset: 0x000710FC
		internal bool isFlowSuppressed
		{
			get
			{
				return (this._flags & ExecutionContext.Flags.IsFlowSuppressed) > ExecutionContext.Flags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= ExecutionContext.Flags.IsFlowSuppressed;
					return;
				}
				this._flags &= (ExecutionContext.Flags)(-3);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x00072F1F File Offset: 0x0007111F
		internal static ExecutionContext PreAllocatedDefault
		{
			[SecuritySafeCritical]
			get
			{
				return ExecutionContext.s_dummyDefaultEC;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x00072F26 File Offset: 0x00071126
		internal bool IsPreAllocatedDefault
		{
			get
			{
				return (this._flags & ExecutionContext.Flags.IsPreAllocatedDefault) != ExecutionContext.Flags.None;
			}
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000025BE File Offset: 0x000007BE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext()
		{
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x00072F35 File Offset: 0x00071135
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext(bool isPreAllocatedDefault)
		{
			if (isPreAllocatedDefault)
			{
				this._flags = ExecutionContext.Flags.IsPreAllocatedDefault;
			}
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00072F48 File Offset: 0x00071148
		[SecurityCritical]
		internal static object GetLocalValue(IAsyncLocal local)
		{
			return Thread.CurrentThread.GetExecutionContextReader().GetLocalValue(local);
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x00072F68 File Offset: 0x00071168
		[SecurityCritical]
		internal static void SetLocalValue(IAsyncLocal local, object newValue, bool needChangeNotifications)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			object obj = null;
			bool flag = mutableExecutionContext._localValues != null && mutableExecutionContext._localValues.TryGetValue(local, out obj);
			if (obj == newValue)
			{
				return;
			}
			if (mutableExecutionContext._localValues == null)
			{
				mutableExecutionContext._localValues = new Dictionary<IAsyncLocal, object>();
			}
			else
			{
				mutableExecutionContext._localValues = new Dictionary<IAsyncLocal, object>(mutableExecutionContext._localValues);
			}
			mutableExecutionContext._localValues[local] = newValue;
			if (needChangeNotifications)
			{
				if (!flag)
				{
					if (mutableExecutionContext._localChangeNotifications == null)
					{
						mutableExecutionContext._localChangeNotifications = new List<IAsyncLocal>();
					}
					else
					{
						mutableExecutionContext._localChangeNotifications = new List<IAsyncLocal>(mutableExecutionContext._localChangeNotifications);
					}
					mutableExecutionContext._localChangeNotifications.Add(local);
				}
				local.OnValueChanged(obj, newValue, false);
			}
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00073018 File Offset: 0x00071218
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static void OnAsyncLocalContextChanged(ExecutionContext previous, ExecutionContext current)
		{
			List<IAsyncLocal> list = ((previous == null) ? null : previous._localChangeNotifications);
			if (list != null)
			{
				foreach (IAsyncLocal asyncLocal in list)
				{
					object obj = null;
					if (previous != null && previous._localValues != null)
					{
						previous._localValues.TryGetValue(asyncLocal, out obj);
					}
					object obj2 = null;
					if (current != null && current._localValues != null)
					{
						current._localValues.TryGetValue(asyncLocal, out obj2);
					}
					if (obj != obj2)
					{
						asyncLocal.OnValueChanged(obj, obj2, true);
					}
				}
			}
			List<IAsyncLocal> list2 = ((current == null) ? null : current._localChangeNotifications);
			if (list2 != null && list2 != list)
			{
				try
				{
					foreach (IAsyncLocal asyncLocal2 in list2)
					{
						object obj3 = null;
						if (previous == null || previous._localValues == null || !previous._localValues.TryGetValue(asyncLocal2, out obj3))
						{
							object obj4 = null;
							if (current != null && current._localValues != null)
							{
								current._localValues.TryGetValue(asyncLocal2, out obj4);
							}
							if (obj3 != obj4)
							{
								asyncLocal2.OnValueChanged(obj3, obj4, true);
							}
						}
					}
				}
				catch (Exception ex)
				{
					Environment.FailFast(Environment.GetResourceString("An exception was not handled in an AsyncLocal<T> notification callback."), ex);
				}
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x00073180 File Offset: 0x00071380
		// (set) Token: 0x06001EAD RID: 7853 RVA: 0x0007319B File Offset: 0x0007139B
		internal LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				if (this._logicalCallContext == null)
				{
					this._logicalCallContext = new LogicalCallContext();
				}
				return this._logicalCallContext;
			}
			[SecurityCritical]
			set
			{
				this._logicalCallContext = value;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001EAE RID: 7854 RVA: 0x000731A4 File Offset: 0x000713A4
		// (set) Token: 0x06001EAF RID: 7855 RVA: 0x000731BF File Offset: 0x000713BF
		internal IllogicalCallContext IllogicalCallContext
		{
			get
			{
				if (this._illogicalCallContext == null)
				{
					this._illogicalCallContext = new IllogicalCallContext();
				}
				return this._illogicalCallContext;
			}
			set
			{
				this._illogicalCallContext = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x000731C8 File Offset: 0x000713C8
		// (set) Token: 0x06001EB1 RID: 7857 RVA: 0x000731D0 File Offset: 0x000713D0
		internal SynchronizationContext SynchronizationContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._syncContext;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._syncContext = value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x000731D9 File Offset: 0x000713D9
		// (set) Token: 0x06001EB3 RID: 7859 RVA: 0x000731E1 File Offset: 0x000713E1
		internal SynchronizationContext SynchronizationContextNoFlow
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._syncContextNoFlow;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._syncContextNoFlow = value;
			}
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x000731EA File Offset: 0x000713EA
		public void Dispose()
		{
			bool isPreAllocatedDefault = this.IsPreAllocatedDefault;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x000731F3 File Offset: 0x000713F3
		[SecurityCritical]
		public static void Run(ExecutionContext executionContext, ContextCallback callback, object state)
		{
			if (executionContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cannot call Set on a null context"));
			}
			if (!executionContext.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cannot apply a context that has been marshaled across AppDomains, that was not acquired through a Capture operation or that has already been the argument to a Set call."));
			}
			ExecutionContext.Run(executionContext, callback, state, false);
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00073229 File Offset: 0x00071429
		[SecurityCritical]
		[FriendAccessAllowed]
		internal static void Run(ExecutionContext executionContext, ContextCallback callback, object state, bool preserveSyncCtx)
		{
			ExecutionContext.RunInternal(executionContext, callback, state, preserveSyncCtx);
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x00073234 File Offset: 0x00071434
		internal static void RunInternal(ExecutionContext executionContext, ContextCallback callback, object state)
		{
			ExecutionContext.RunInternal(executionContext, callback, state, false);
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x00073240 File Offset: 0x00071440
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static void RunInternal(ExecutionContext executionContext, ContextCallback callback, object state, bool preserveSyncCtx)
		{
			if (!executionContext.IsPreAllocatedDefault)
			{
				executionContext.isNewCapture = false;
			}
			Thread currentThread = Thread.CurrentThread;
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
				if ((executionContextReader.IsNull || executionContextReader.IsDefaultFTContext(preserveSyncCtx)) && executionContext.IsDefaultFTContext(preserveSyncCtx) && executionContextReader.HasSameLocalValues(executionContext))
				{
					ExecutionContext.EstablishCopyOnWriteScope(currentThread, true, ref executionContextSwitcher);
				}
				else
				{
					if (executionContext.IsPreAllocatedDefault)
					{
						executionContext = new ExecutionContext();
					}
					executionContextSwitcher = ExecutionContext.SetExecutionContext(executionContext, preserveSyncCtx);
				}
				callback(state);
			}
			finally
			{
				executionContextSwitcher.Undo();
			}
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000732E0 File Offset: 0x000714E0
		internal static void RunInternal<TState>(ExecutionContext executionContext, ContextCallback<TState> callback, ref TState state)
		{
			ExecutionContext.RunInternal<TState>(executionContext, callback, ref state, false);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x000732EC File Offset: 0x000714EC
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static void RunInternal<TState>(ExecutionContext executionContext, ContextCallback<TState> callback, ref TState state, bool preserveSyncCtx)
		{
			if (!executionContext.IsPreAllocatedDefault)
			{
				executionContext.isNewCapture = false;
			}
			Thread currentThread = Thread.CurrentThread;
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
				if ((executionContextReader.IsNull || executionContextReader.IsDefaultFTContext(preserveSyncCtx)) && executionContext.IsDefaultFTContext(preserveSyncCtx) && executionContextReader.HasSameLocalValues(executionContext))
				{
					ExecutionContext.EstablishCopyOnWriteScope(currentThread, true, ref executionContextSwitcher);
				}
				else
				{
					if (executionContext.IsPreAllocatedDefault)
					{
						executionContext = new ExecutionContext();
					}
					executionContextSwitcher = ExecutionContext.SetExecutionContext(executionContext, preserveSyncCtx);
				}
				callback(ref state);
			}
			finally
			{
				executionContextSwitcher.Undo();
			}
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x0007338C File Offset: 0x0007158C
		[SecurityCritical]
		internal static void EstablishCopyOnWriteScope(ref ExecutionContextSwitcher ecsw)
		{
			ExecutionContext.EstablishCopyOnWriteScope(Thread.CurrentThread, false, ref ecsw);
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x0007339A File Offset: 0x0007159A
		[SecurityCritical]
		private static void EstablishCopyOnWriteScope(Thread currentThread, bool knownNullWindowsIdentity, ref ExecutionContextSwitcher ecsw)
		{
			ecsw.outerEC = currentThread.GetExecutionContextReader();
			ecsw.outerECBelongsToScope = currentThread.ExecutionContextBelongsToCurrentScope;
			currentThread.ExecutionContextBelongsToCurrentScope = false;
			ecsw.thread = currentThread;
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x000733C4 File Offset: 0x000715C4
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static ExecutionContextSwitcher SetExecutionContext(ExecutionContext executionContext, bool preserveSyncCtx)
		{
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			Thread currentThread = Thread.CurrentThread;
			ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
			executionContextSwitcher.thread = currentThread;
			executionContextSwitcher.outerEC = executionContextReader;
			executionContextSwitcher.outerECBelongsToScope = currentThread.ExecutionContextBelongsToCurrentScope;
			if (preserveSyncCtx)
			{
				executionContext.SynchronizationContext = executionContextReader.SynchronizationContext;
			}
			executionContext.SynchronizationContextNoFlow = executionContextReader.SynchronizationContextNoFlow;
			currentThread.SetExecutionContext(executionContext, true);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.OnAsyncLocalContextChanged(executionContextReader.DangerousGetRawExecutionContext(), executionContext);
			}
			catch
			{
				executionContextSwitcher.UndoNoThrow();
				throw;
			}
			return executionContextSwitcher;
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00073458 File Offset: 0x00071658
		[SecuritySafeCritical]
		public ExecutionContext CreateCopy()
		{
			if (!this.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Only newly captured contexts can be copied"));
			}
			ExecutionContext executionContext = new ExecutionContext();
			executionContext.isNewCapture = true;
			executionContext._syncContext = ((this._syncContext == null) ? null : this._syncContext.CreateCopy());
			executionContext._localValues = this._localValues;
			executionContext._localChangeNotifications = this._localChangeNotifications;
			if (this._logicalCallContext != null)
			{
				executionContext.LogicalCallContext = (LogicalCallContext)this.LogicalCallContext.Clone();
			}
			return executionContext;
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x000734E0 File Offset: 0x000716E0
		[SecuritySafeCritical]
		internal ExecutionContext CreateMutableCopy()
		{
			ExecutionContext executionContext = new ExecutionContext();
			executionContext._syncContext = this._syncContext;
			executionContext._syncContextNoFlow = this._syncContextNoFlow;
			if (this._logicalCallContext != null)
			{
				executionContext.LogicalCallContext = (LogicalCallContext)this.LogicalCallContext.Clone();
			}
			if (this._illogicalCallContext != null)
			{
				executionContext.IllogicalCallContext = this.IllogicalCallContext.CreateCopy();
			}
			executionContext._localValues = this._localValues;
			executionContext._localChangeNotifications = this._localChangeNotifications;
			executionContext.isFlowSuppressed = this.isFlowSuppressed;
			return executionContext;
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00073568 File Offset: 0x00071768
		[SecurityCritical]
		public static AsyncFlowControl SuppressFlow()
		{
			if (ExecutionContext.IsFlowSuppressed())
			{
				throw new InvalidOperationException(Environment.GetResourceString("Context flow is already suppressed."));
			}
			AsyncFlowControl asyncFlowControl = default(AsyncFlowControl);
			asyncFlowControl.Setup();
			return asyncFlowControl;
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x0007359C File Offset: 0x0007179C
		[SecuritySafeCritical]
		public static void RestoreFlow()
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			if (!mutableExecutionContext.isFlowSuppressed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cannot restore context flow when it is not suppressed."));
			}
			mutableExecutionContext.isFlowSuppressed = false;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000735C8 File Offset: 0x000717C8
		public static bool IsFlowSuppressed()
		{
			return Thread.CurrentThread.GetExecutionContextReader().IsFlowSuppressed;
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000735E8 File Offset: 0x000717E8
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ExecutionContext Capture()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.None);
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00073600 File Offset: 0x00071800
		[SecuritySafeCritical]
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static ExecutionContext FastCapture()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00073618 File Offset: 0x00071818
		[SecurityCritical]
		internal static ExecutionContext Capture(ref StackCrawlMark stackMark, ExecutionContext.CaptureOptions options)
		{
			ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
			if (executionContextReader.IsFlowSuppressed)
			{
				return null;
			}
			SynchronizationContext synchronizationContext = null;
			LogicalCallContext logicalCallContext = null;
			if (!executionContextReader.IsNull)
			{
				if ((options & ExecutionContext.CaptureOptions.IgnoreSyncCtx) == ExecutionContext.CaptureOptions.None)
				{
					synchronizationContext = ((executionContextReader.SynchronizationContext == null) ? null : executionContextReader.SynchronizationContext.CreateCopy());
				}
				if (executionContextReader.LogicalCallContext.HasInfo)
				{
					logicalCallContext = executionContextReader.LogicalCallContext.Clone();
				}
			}
			Dictionary<IAsyncLocal, object> dictionary = null;
			List<IAsyncLocal> list = null;
			if (!executionContextReader.IsNull)
			{
				dictionary = executionContextReader.DangerousGetRawExecutionContext()._localValues;
				list = executionContextReader.DangerousGetRawExecutionContext()._localChangeNotifications;
			}
			if ((options & ExecutionContext.CaptureOptions.OptimizeDefaultCase) != ExecutionContext.CaptureOptions.None && synchronizationContext == null && (logicalCallContext == null || !logicalCallContext.HasInfo) && dictionary == null && list == null)
			{
				return ExecutionContext.s_dummyDefaultEC;
			}
			return new ExecutionContext
			{
				_syncContext = synchronizationContext,
				LogicalCallContext = logicalCallContext,
				_localValues = dictionary,
				_localChangeNotifications = list,
				isNewCapture = true
			};
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000736FB File Offset: 0x000718FB
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this._logicalCallContext != null)
			{
				info.AddValue("LogicalCallContext", this._logicalCallContext, typeof(LogicalCallContext));
			}
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00073730 File Offset: 0x00071930
		[SecurityCritical]
		private ExecutionContext(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("LogicalCallContext"))
				{
					this._logicalCallContext = (LogicalCallContext)enumerator.Value;
				}
			}
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00073777 File Offset: 0x00071977
		[SecurityCritical]
		internal bool IsDefaultFTContext(bool ignoreSyncCtx)
		{
			return (ignoreSyncCtx || this._syncContext == null) && (this._logicalCallContext == null || !this._logicalCallContext.HasInfo) && (this._illogicalCallContext == null || !this._illogicalCallContext.HasUserData);
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x000737B5 File Offset: 0x000719B5
		// Note: this type is marked as 'beforefieldinit'.
		static ExecutionContext()
		{
		}

		// Token: 0x040019C0 RID: 6592
		private SynchronizationContext _syncContext;

		// Token: 0x040019C1 RID: 6593
		private SynchronizationContext _syncContextNoFlow;

		// Token: 0x040019C2 RID: 6594
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x040019C3 RID: 6595
		private IllogicalCallContext _illogicalCallContext;

		// Token: 0x040019C4 RID: 6596
		private ExecutionContext.Flags _flags;

		// Token: 0x040019C5 RID: 6597
		private Dictionary<IAsyncLocal, object> _localValues;

		// Token: 0x040019C6 RID: 6598
		private List<IAsyncLocal> _localChangeNotifications;

		// Token: 0x040019C7 RID: 6599
		private static readonly ExecutionContext s_dummyDefaultEC = new ExecutionContext(true);

		// Token: 0x040019C8 RID: 6600
		internal static readonly ExecutionContext Default = new ExecutionContext();

		// Token: 0x0200029A RID: 666
		private enum Flags
		{
			// Token: 0x040019CA RID: 6602
			None,
			// Token: 0x040019CB RID: 6603
			IsNewCapture,
			// Token: 0x040019CC RID: 6604
			IsFlowSuppressed,
			// Token: 0x040019CD RID: 6605
			IsPreAllocatedDefault = 4
		}

		// Token: 0x0200029B RID: 667
		internal struct Reader
		{
			// Token: 0x06001ECA RID: 7882 RVA: 0x000737CC File Offset: 0x000719CC
			public Reader(ExecutionContext ec)
			{
				this.m_ec = ec;
			}

			// Token: 0x06001ECB RID: 7883 RVA: 0x000737D5 File Offset: 0x000719D5
			public ExecutionContext DangerousGetRawExecutionContext()
			{
				return this.m_ec;
			}

			// Token: 0x170003AD RID: 941
			// (get) Token: 0x06001ECC RID: 7884 RVA: 0x000737DD File Offset: 0x000719DD
			public bool IsNull
			{
				get
				{
					return this.m_ec == null;
				}
			}

			// Token: 0x06001ECD RID: 7885 RVA: 0x000737E8 File Offset: 0x000719E8
			[SecurityCritical]
			public bool IsDefaultFTContext(bool ignoreSyncCtx)
			{
				return this.m_ec.IsDefaultFTContext(ignoreSyncCtx);
			}

			// Token: 0x170003AE RID: 942
			// (get) Token: 0x06001ECE RID: 7886 RVA: 0x000737F6 File Offset: 0x000719F6
			public bool IsFlowSuppressed
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsNull && this.m_ec.isFlowSuppressed;
				}
			}

			// Token: 0x06001ECF RID: 7887 RVA: 0x0007380D File Offset: 0x00071A0D
			public bool IsSame(ExecutionContext.Reader other)
			{
				return this.m_ec == other.m_ec;
			}

			// Token: 0x170003AF RID: 943
			// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x0007381D File Offset: 0x00071A1D
			public SynchronizationContext SynchronizationContext
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ec.SynchronizationContext;
					}
					return null;
				}
			}

			// Token: 0x170003B0 RID: 944
			// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x00073834 File Offset: 0x00071A34
			public SynchronizationContext SynchronizationContextNoFlow
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ec.SynchronizationContextNoFlow;
					}
					return null;
				}
			}

			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x0007384B File Offset: 0x00071A4B
			public LogicalCallContext.Reader LogicalCallContext
			{
				[SecurityCritical]
				get
				{
					return new LogicalCallContext.Reader(this.IsNull ? null : this.m_ec.LogicalCallContext);
				}
			}

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x00073868 File Offset: 0x00071A68
			public IllogicalCallContext.Reader IllogicalCallContext
			{
				[SecurityCritical]
				get
				{
					return new IllogicalCallContext.Reader(this.IsNull ? null : this.m_ec.IllogicalCallContext);
				}
			}

			// Token: 0x06001ED4 RID: 7892 RVA: 0x00073888 File Offset: 0x00071A88
			[SecurityCritical]
			public object GetLocalValue(IAsyncLocal local)
			{
				if (this.IsNull)
				{
					return null;
				}
				if (this.m_ec._localValues == null)
				{
					return null;
				}
				object obj;
				this.m_ec._localValues.TryGetValue(local, out obj);
				return obj;
			}

			// Token: 0x06001ED5 RID: 7893 RVA: 0x000738C4 File Offset: 0x00071AC4
			[SecurityCritical]
			public bool HasSameLocalValues(ExecutionContext other)
			{
				Dictionary<IAsyncLocal, object> dictionary = (this.IsNull ? null : this.m_ec._localValues);
				Dictionary<IAsyncLocal, object> dictionary2 = ((other == null) ? null : other._localValues);
				return dictionary == dictionary2;
			}

			// Token: 0x06001ED6 RID: 7894 RVA: 0x000738F7 File Offset: 0x00071AF7
			[SecurityCritical]
			public bool HasLocalValues()
			{
				return !this.IsNull && this.m_ec._localValues != null;
			}

			// Token: 0x040019CE RID: 6606
			private ExecutionContext m_ec;
		}

		// Token: 0x0200029C RID: 668
		[Flags]
		internal enum CaptureOptions
		{
			// Token: 0x040019D0 RID: 6608
			None = 0,
			// Token: 0x040019D1 RID: 6609
			IgnoreSyncCtx = 1,
			// Token: 0x040019D2 RID: 6610
			OptimizeDefaultCase = 2
		}
	}
}
