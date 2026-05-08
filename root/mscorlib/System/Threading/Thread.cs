using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Principal;
using Internal.Runtime.Augments;

namespace System.Threading
{
	// Token: 0x020002A3 RID: 675
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Thread : CriticalFinalizerObject, _Thread
	{
		// Token: 0x06001F14 RID: 7956 RVA: 0x00073E13 File Offset: 0x00072013
		private static void AsyncLocalSetCurrentCulture(AsyncLocalValueChangedArgs<CultureInfo> args)
		{
			Thread.m_CurrentCulture = args.CurrentValue;
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00073E21 File Offset: 0x00072021
		private static void AsyncLocalSetCurrentUICulture(AsyncLocalValueChangedArgs<CultureInfo> args)
		{
			Thread.m_CurrentUICulture = args.CurrentValue;
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00073E2F File Offset: 0x0007202F
		[SecuritySafeCritical]
		public Thread(ThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.SetStartHelper(start, 0);
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x00073E4D File Offset: 0x0007204D
		[SecuritySafeCritical]
		public Thread(ThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (0 > maxStackSize)
			{
				throw new ArgumentOutOfRangeException("maxStackSize", Environment.GetResourceString("Non-negative number required."));
			}
			this.SetStartHelper(start, maxStackSize);
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x00073E2F File Offset: 0x0007202F
		[SecuritySafeCritical]
		public Thread(ParameterizedThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.SetStartHelper(start, 0);
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00073E4D File Offset: 0x0007204D
		[SecuritySafeCritical]
		public Thread(ParameterizedThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (0 > maxStackSize)
			{
				throw new ArgumentOutOfRangeException("maxStackSize", Environment.GetResourceString("Non-negative number required."));
			}
			this.SetStartHelper(start, maxStackSize);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x00073E84 File Offset: 0x00072084
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Start()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Start(ref stackCrawlMark);
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x00073E9C File Offset: 0x0007209C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Start(object parameter)
		{
			if (this.m_Delegate is ThreadStart)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The thread was created with a ThreadStart delegate that does not accept a parameter."));
			}
			this.m_ThreadStartArg = parameter;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Start(ref stackCrawlMark);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x00073ED8 File Offset: 0x000720D8
		[SecuritySafeCritical]
		private void Start(ref StackCrawlMark stackMark)
		{
			if (this.m_Delegate != null)
			{
				ThreadHelper threadHelper = (ThreadHelper)this.m_Delegate.Target;
				ExecutionContext executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx);
				threadHelper.SetExecutionContextHelper(executionContext);
			}
			object obj = null;
			this.StartInternal(obj, ref stackMark);
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x00073F15 File Offset: 0x00072115
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext.Reader GetExecutionContextReader()
		{
			return new ExecutionContext.Reader(this.m_ExecutionContext);
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00073F22 File Offset: 0x00072122
		// (set) Token: 0x06001F1F RID: 7967 RVA: 0x00073F2D File Offset: 0x0007212D
		internal bool ExecutionContextBelongsToCurrentScope
		{
			get
			{
				return !this.m_ExecutionContextBelongsToOuterScope;
			}
			set
			{
				this.m_ExecutionContextBelongsToOuterScope = !value;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x00073F3C File Offset: 0x0007213C
		public ExecutionContext ExecutionContext
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				ExecutionContext executionContext;
				if (this == Thread.CurrentThread)
				{
					executionContext = this.GetMutableExecutionContext();
				}
				else
				{
					executionContext = this.m_ExecutionContext;
				}
				return executionContext;
			}
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x00073F64 File Offset: 0x00072164
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal ExecutionContext GetMutableExecutionContext()
		{
			if (this.m_ExecutionContext == null)
			{
				this.m_ExecutionContext = new ExecutionContext();
			}
			else if (!this.ExecutionContextBelongsToCurrentScope)
			{
				ExecutionContext executionContext = this.m_ExecutionContext.CreateMutableCopy();
				this.m_ExecutionContext = executionContext;
			}
			this.ExecutionContextBelongsToCurrentScope = true;
			return this.m_ExecutionContext;
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00073FAE File Offset: 0x000721AE
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetExecutionContext(ExecutionContext value, bool belongsToCurrentScope)
		{
			this.m_ExecutionContext = value;
			this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x00073FBE File Offset: 0x000721BE
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetExecutionContext(ExecutionContext.Reader value, bool belongsToCurrentScope)
		{
			this.m_ExecutionContext = value.DangerousGetRawExecutionContext();
			this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00073FD4 File Offset: 0x000721D4
		[Obsolete("Thread.SetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
		public void SetCompressedStack(CompressedStack stack)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Use CompressedStack.(Capture/Run) or ExecutionContext.(Capture/Run) APIs instead."));
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00073FD4 File Offset: 0x000721D4
		[SecurityCritical]
		[Obsolete("Thread.GetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
		public CompressedStack GetCompressedStack()
		{
			throw new InvalidOperationException(Environment.GetResourceString("Use CompressedStack.(Capture/Run) or ExecutionContext.(Capture/Run) APIs instead."));
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00073FE5 File Offset: 0x000721E5
		public static void ResetAbort()
		{
			Thread currentThread = Thread.CurrentThread;
			if ((currentThread.ThreadState & ThreadState.AbortRequested) == ThreadState.Running)
			{
				throw new ThreadStateException(Environment.GetResourceString("Unable to reset abort because no abort was requested."));
			}
			currentThread.ResetAbortNative();
			currentThread.ClearAbortReason();
		}

		// Token: 0x06001F27 RID: 7975
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetAbortNative();

		// Token: 0x06001F28 RID: 7976 RVA: 0x00074015 File Offset: 0x00072215
		[SecuritySafeCritical]
		[Obsolete("Thread.Suspend has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		public void Suspend()
		{
			this.SuspendInternal();
		}

		// Token: 0x06001F29 RID: 7977
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SuspendInternal();

		// Token: 0x06001F2A RID: 7978 RVA: 0x0007401D File Offset: 0x0007221D
		[SecuritySafeCritical]
		[Obsolete("Thread.Resume has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		public void Resume()
		{
			this.ResumeInternal();
		}

		// Token: 0x06001F2B RID: 7979
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResumeInternal();

		// Token: 0x06001F2C RID: 7980 RVA: 0x00074025 File Offset: 0x00072225
		public void Interrupt()
		{
			this.InterruptInternal();
		}

		// Token: 0x06001F2D RID: 7981
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InterruptInternal();

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x0007402D File Offset: 0x0007222D
		// (set) Token: 0x06001F2F RID: 7983 RVA: 0x00074035 File Offset: 0x00072235
		public ThreadPriority Priority
		{
			[SecuritySafeCritical]
			get
			{
				return (ThreadPriority)this.GetPriorityNative();
			}
			set
			{
				this.SetPriorityNative((int)value);
			}
		}

		// Token: 0x06001F30 RID: 7984
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPriorityNative();

		// Token: 0x06001F31 RID: 7985
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPriorityNative(int priority);

		// Token: 0x06001F32 RID: 7986
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool JoinInternal(int millisecondsTimeout);

		// Token: 0x06001F33 RID: 7987 RVA: 0x0007403E File Offset: 0x0007223E
		public void Join()
		{
			this.JoinInternal(-1);
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00074048 File Offset: 0x00072248
		public bool Join(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return this.JoinInternal(millisecondsTimeout);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x0007406C File Offset: 0x0007226C
		public bool Join(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return this.Join((int)num);
		}

		// Token: 0x06001F36 RID: 7990
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SleepInternal(int millisecondsTimeout);

		// Token: 0x06001F37 RID: 7991 RVA: 0x000740AD File Offset: 0x000722AD
		[SecuritySafeCritical]
		public static void Sleep(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			Thread.SleepInternal(millisecondsTimeout);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x000740D0 File Offset: 0x000722D0
		public static void Sleep(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			Thread.Sleep((int)num);
		}

		// Token: 0x06001F39 RID: 7993
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool YieldInternal();

		// Token: 0x06001F3A RID: 7994 RVA: 0x00074110 File Offset: 0x00072310
		public static bool Yield()
		{
			return Thread.YieldInternal();
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00074118 File Offset: 0x00072318
		[SecurityCritical]
		private void SetStartHelper(Delegate start, int maxStackSize)
		{
			maxStackSize = Thread.GetProcessDefaultStackSize(maxStackSize);
			ThreadHelper threadHelper = new ThreadHelper(start);
			if (start is ThreadStart)
			{
				this.SetStart(new ThreadStart(threadHelper.ThreadStart), maxStackSize);
				return;
			}
			this.SetStart(new ParameterizedThreadStart(threadHelper.ThreadStart), maxStackSize);
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x00074163 File Offset: 0x00072363
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return Thread.LocalDataStoreManager.AllocateDataSlot();
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x0007416F File Offset: 0x0007236F
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			return Thread.LocalDataStoreManager.AllocateNamedDataSlot(name);
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x0007417C File Offset: 0x0007237C
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			return Thread.LocalDataStoreManager.GetNamedDataSlot(name);
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00074189 File Offset: 0x00072389
		public static void FreeNamedDataSlot(string name)
		{
			Thread.LocalDataStoreManager.FreeNamedDataSlot(name);
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00074198 File Offset: 0x00072398
		public static object GetData(LocalDataStoreSlot slot)
		{
			LocalDataStoreHolder localDataStoreHolder = Thread.s_LocalDataStore;
			if (localDataStoreHolder == null)
			{
				Thread.LocalDataStoreManager.ValidateSlot(slot);
				return null;
			}
			return localDataStoreHolder.Store.GetData(slot);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x000741C8 File Offset: 0x000723C8
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			LocalDataStoreHolder localDataStoreHolder = Thread.s_LocalDataStore;
			if (localDataStoreHolder == null)
			{
				localDataStoreHolder = Thread.LocalDataStoreManager.CreateLocalDataStore();
				Thread.s_LocalDataStore = localDataStoreHolder;
			}
			localDataStoreHolder.Store.SetData(slot, data);
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x000741FC File Offset: 0x000723FC
		// (set) Token: 0x06001F43 RID: 8003 RVA: 0x00074204 File Offset: 0x00072404
		public CultureInfo CurrentUICulture
		{
			get
			{
				return this.GetCurrentUICultureNoAppX();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				CultureInfo.VerifyCultureName(value, true);
				if (Thread.m_CurrentUICulture == null && Thread.m_CurrentCulture == null)
				{
					Thread.nativeInitCultureAccessors();
				}
				if (!AppContextSwitches.NoAsyncCurrentCulture)
				{
					if (Thread.s_asyncLocalCurrentUICulture == null)
					{
						Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentUICulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentUICulture)), null);
					}
					Thread.s_asyncLocalCurrentUICulture.Value = value;
					return;
				}
				Thread.m_CurrentUICulture = value;
			}
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00074278 File Offset: 0x00072478
		internal CultureInfo GetCurrentUICultureNoAppX()
		{
			if (Thread.m_CurrentUICulture != null)
			{
				return Thread.m_CurrentUICulture;
			}
			CultureInfo defaultThreadCurrentUICulture = CultureInfo.DefaultThreadCurrentUICulture;
			if (defaultThreadCurrentUICulture == null)
			{
				return CultureInfo.UserDefaultUICulture;
			}
			return defaultThreadCurrentUICulture;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x000742A2 File Offset: 0x000724A2
		// (set) Token: 0x06001F46 RID: 8006 RVA: 0x000742AC File Offset: 0x000724AC
		public CultureInfo CurrentCulture
		{
			get
			{
				return this.GetCurrentCultureNoAppX();
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (Thread.m_CurrentCulture == null && Thread.m_CurrentUICulture == null)
				{
					Thread.nativeInitCultureAccessors();
				}
				if (!AppContextSwitches.NoAsyncCurrentCulture)
				{
					if (Thread.s_asyncLocalCurrentCulture == null)
					{
						Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentCulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentCulture)), null);
					}
					Thread.s_asyncLocalCurrentCulture.Value = value;
					return;
				}
				Thread.m_CurrentCulture = value;
			}
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x00074318 File Offset: 0x00072518
		private CultureInfo GetCurrentCultureNoAppX()
		{
			if (Thread.m_CurrentCulture != null)
			{
				return Thread.m_CurrentCulture;
			}
			CultureInfo defaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentCulture;
			if (defaultThreadCurrentCulture == null)
			{
				return CultureInfo.UserDefaultCulture;
			}
			return defaultThreadCurrentCulture;
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00074342 File Offset: 0x00072542
		private static void nativeInitCultureAccessors()
		{
			Thread.m_CurrentCulture = CultureInfo.ConstructCurrentCulture();
			Thread.m_CurrentUICulture = CultureInfo.ConstructCurrentUICulture();
		}

		// Token: 0x06001F49 RID: 8009
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MemoryBarrier();

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x00074358 File Offset: 0x00072558
		private static LocalDataStoreMgr LocalDataStoreManager
		{
			get
			{
				if (Thread.s_LocalDataStoreMgr == null)
				{
					Interlocked.CompareExchange<LocalDataStoreMgr>(ref Thread.s_LocalDataStoreMgr, new LocalDataStoreMgr(), null);
				}
				return Thread.s_LocalDataStoreMgr;
			}
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x000174FB File Offset: 0x000156FB
		void _Thread.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x000174FB File Offset: 0x000156FB
		void _Thread.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x000174FB File Offset: 0x000156FB
		void _Thread.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x000174FB File Offset: 0x000156FB
		void _Thread.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F4F RID: 8015
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ConstructInternalThread();

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x00074377 File Offset: 0x00072577
		private InternalThread Internal
		{
			get
			{
				if (this.internal_thread == null)
				{
					this.ConstructInternalThread();
				}
				return this.internal_thread;
			}
		}

		// Token: 0x06001F51 RID: 8017
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] ByteArrayToRootDomain(byte[] arr);

		// Token: 0x06001F52 RID: 8018
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] ByteArrayToCurrentDomain(byte[] arr);

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x0007438D File Offset: 0x0007258D
		public static Context CurrentContext
		{
			get
			{
				return AppDomain.InternalGetContext();
			}
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x00074394 File Offset: 0x00072594
		private static void DeserializePrincipal(Thread th)
		{
			MemoryStream memoryStream = new MemoryStream(Thread.ByteArrayToCurrentDomain(th.Internal._serialized_principal));
			int num = memoryStream.ReadByte();
			if (num == 0)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				th.principal = (IPrincipal)binaryFormatter.Deserialize(memoryStream);
				th.principal_version = th.Internal._serialized_principal_version;
				return;
			}
			if (num == 1)
			{
				BinaryReader binaryReader = new BinaryReader(memoryStream);
				string text = binaryReader.ReadString();
				string text2 = binaryReader.ReadString();
				int num2 = binaryReader.ReadInt32();
				string[] array = null;
				if (num2 >= 0)
				{
					array = new string[num2];
					for (int i = 0; i < num2; i++)
					{
						array[i] = binaryReader.ReadString();
					}
				}
				th.principal = new GenericPrincipal(new GenericIdentity(text, text2), array);
				return;
			}
			if (num == 2 || num == 3)
			{
				string[] array2 = ((num == 2) ? null : new string[0]);
				th.principal = new GenericPrincipal(new GenericIdentity("", ""), array2);
			}
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00074488 File Offset: 0x00072688
		private static void SerializePrincipal(Thread th, IPrincipal value)
		{
			MemoryStream memoryStream = new MemoryStream();
			bool flag = false;
			if (value.GetType() == typeof(GenericPrincipal))
			{
				GenericPrincipal genericPrincipal = (GenericPrincipal)value;
				if (genericPrincipal.Identity != null && genericPrincipal.Identity.GetType() == typeof(GenericIdentity))
				{
					GenericIdentity genericIdentity = (GenericIdentity)genericPrincipal.Identity;
					if (genericIdentity.Name == "" && genericIdentity.AuthenticationType == "")
					{
						if (genericPrincipal.Roles == null)
						{
							memoryStream.WriteByte(2);
							flag = true;
						}
						else if (genericPrincipal.Roles.Length == 0)
						{
							memoryStream.WriteByte(3);
							flag = true;
						}
					}
					else
					{
						memoryStream.WriteByte(1);
						BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
						binaryWriter.Write(genericPrincipal.Identity.Name);
						binaryWriter.Write(genericPrincipal.Identity.AuthenticationType);
						string[] roles = genericPrincipal.Roles;
						if (roles == null)
						{
							binaryWriter.Write(-1);
						}
						else
						{
							binaryWriter.Write(roles.Length);
							foreach (string text in roles)
							{
								binaryWriter.Write(text);
							}
						}
						binaryWriter.Flush();
						flag = true;
					}
				}
			}
			if (!flag)
			{
				memoryStream.WriteByte(0);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				try
				{
					binaryFormatter.Serialize(memoryStream, value);
				}
				catch
				{
				}
			}
			th.Internal._serialized_principal = Thread.ByteArrayToRootDomain(memoryStream.ToArray());
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x00074614 File Offset: 0x00072814
		// (set) Token: 0x06001F57 RID: 8023 RVA: 0x000746C8 File Offset: 0x000728C8
		public static IPrincipal CurrentPrincipal
		{
			get
			{
				Thread currentThread = Thread.CurrentThread;
				IPrincipal principal = currentThread.GetExecutionContextReader().LogicalCallContext.Principal;
				if (principal != null)
				{
					return principal;
				}
				if (currentThread.principal_version != currentThread.Internal._serialized_principal_version)
				{
					currentThread.principal = null;
				}
				if (currentThread.principal != null)
				{
					return currentThread.principal;
				}
				if (currentThread.Internal._serialized_principal != null)
				{
					try
					{
						Thread.DeserializePrincipal(currentThread);
						return currentThread.principal;
					}
					catch
					{
					}
				}
				currentThread.principal = Thread.GetDomain().DefaultPrincipal;
				currentThread.principal_version = currentThread.Internal._serialized_principal_version;
				return currentThread.principal;
			}
			set
			{
				Thread currentThread = Thread.CurrentThread;
				currentThread.GetMutableExecutionContext().LogicalCallContext.Principal = value;
				if (value != Thread.GetDomain().DefaultPrincipal)
				{
					currentThread.Internal._serialized_principal_version++;
					try
					{
						Thread.SerializePrincipal(currentThread, value);
					}
					catch (Exception)
					{
						currentThread.Internal._serialized_principal = null;
					}
					currentThread.principal_version = currentThread.Internal._serialized_principal_version;
				}
				else
				{
					currentThread.Internal._serialized_principal = null;
				}
				currentThread.principal = value;
			}
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x0007475C File Offset: 0x0007295C
		public static AppDomain GetDomain()
		{
			return AppDomain.CurrentDomain;
		}

		// Token: 0x06001F59 RID: 8025
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCurrentThread_icall(ref Thread thread);

		// Token: 0x06001F5A RID: 8026 RVA: 0x00074764 File Offset: 0x00072964
		private static Thread GetCurrentThread()
		{
			Thread thread = null;
			Thread.GetCurrentThread_icall(ref thread);
			return thread;
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x0007477C File Offset: 0x0007297C
		public static Thread CurrentThread
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				Thread thread = Thread.current_thread;
				if (thread != null)
				{
					return thread;
				}
				return Thread.GetCurrentThread();
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001F5C RID: 8028 RVA: 0x00074799 File Offset: 0x00072999
		internal static int CurrentThreadId
		{
			get
			{
				return (int)Thread.CurrentThread.internal_thread.thread_id;
			}
		}

		// Token: 0x06001F5D RID: 8029
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetDomainID();

		// Token: 0x06001F5E RID: 8030
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Thread_internal(MulticastDelegate start);

		// Token: 0x06001F5F RID: 8031 RVA: 0x000747AB File Offset: 0x000729AB
		private Thread(InternalThread it)
		{
			this.internal_thread = it;
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x000747BC File Offset: 0x000729BC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~Thread()
		{
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x000747E4 File Offset: 0x000729E4
		// (set) Token: 0x06001F62 RID: 8034 RVA: 0x000747F8 File Offset: 0x000729F8
		[Obsolete("Deprecated in favor of GetApartmentState, SetApartmentState and TrySetApartmentState.")]
		public ApartmentState ApartmentState
		{
			get
			{
				this.ValidateThreadState();
				return (ApartmentState)this.Internal.apartment_state;
			}
			set
			{
				this.ValidateThreadState();
				this.TrySetApartmentState(value);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x00074809 File Offset: 0x00072A09
		public bool IsThreadPoolThread
		{
			get
			{
				return this.IsThreadPoolThreadInternal;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x00074811 File Offset: 0x00072A11
		// (set) Token: 0x06001F65 RID: 8037 RVA: 0x0007481E File Offset: 0x00072A1E
		internal bool IsThreadPoolThreadInternal
		{
			get
			{
				return this.Internal.threadpool_thread;
			}
			set
			{
				this.Internal.threadpool_thread = value;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x0007482C File Offset: 0x00072A2C
		public bool IsAlive
		{
			get
			{
				ThreadState state = Thread.GetState(this.Internal);
				return (state & ThreadState.Aborted) == ThreadState.Running && (state & ThreadState.Stopped) == ThreadState.Running && (state & ThreadState.Unstarted) == ThreadState.Running;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x0007485C File Offset: 0x00072A5C
		// (set) Token: 0x06001F68 RID: 8040 RVA: 0x00074869 File Offset: 0x00072A69
		public bool IsBackground
		{
			get
			{
				return (this.ValidateThreadState() & ThreadState.Background) > ThreadState.Running;
			}
			set
			{
				this.ValidateThreadState();
				if (value)
				{
					Thread.SetState(this.Internal, ThreadState.Background);
					return;
				}
				Thread.ClrState(this.Internal, ThreadState.Background);
			}
		}

		// Token: 0x06001F69 RID: 8041
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetName_internal(InternalThread thread);

		// Token: 0x06001F6A RID: 8042
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetName_icall(InternalThread thread, char* name, int nameLength);

		// Token: 0x06001F6B RID: 8043 RVA: 0x00074890 File Offset: 0x00072A90
		private unsafe static void SetName_internal(InternalThread thread, string name)
		{
			fixed (string text = name)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				Thread.SetName_icall(thread, ptr, (name != null) ? name.Length : 0);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x000748C2 File Offset: 0x00072AC2
		// (set) Token: 0x06001F6D RID: 8045 RVA: 0x000748CF File Offset: 0x00072ACF
		public string Name
		{
			get
			{
				return Thread.GetName_internal(this.Internal);
			}
			set
			{
				Thread.SetName_internal(this.Internal, value);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x000748DD File Offset: 0x00072ADD
		public ThreadState ThreadState
		{
			get
			{
				return Thread.GetState(this.Internal);
			}
		}

		// Token: 0x06001F6F RID: 8047
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Abort_internal(InternalThread thread, object stateInfo);

		// Token: 0x06001F70 RID: 8048 RVA: 0x000748EA File Offset: 0x00072AEA
		public void Abort()
		{
			Thread.Abort_internal(this.Internal, null);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000748F8 File Offset: 0x00072AF8
		public void Abort(object stateInfo)
		{
			Thread.Abort_internal(this.Internal, stateInfo);
		}

		// Token: 0x06001F72 RID: 8050
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetAbortExceptionState();

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x00074906 File Offset: 0x00072B06
		internal object AbortReason
		{
			get
			{
				return this.GetAbortExceptionState();
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x00004088 File Offset: 0x00002288
		private void ClearAbortReason()
		{
		}

		// Token: 0x06001F75 RID: 8053
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SpinWait_nop();

		// Token: 0x06001F76 RID: 8054 RVA: 0x0007490E File Offset: 0x00072B0E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void SpinWait(int iterations)
		{
			if (iterations < 0)
			{
				return;
			}
			while (iterations-- > 0)
			{
				Thread.SpinWait_nop();
			}
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x00074923 File Offset: 0x00072B23
		private void StartInternal(object principal, ref StackCrawlMark stackMark)
		{
			this.Internal._serialized_principal = Thread.CurrentThread.Internal._serialized_principal;
			if (!this.Thread_internal(this.m_Delegate))
			{
				throw new SystemException("Thread creation failed.");
			}
			this.m_ThreadStartArg = null;
		}

		// Token: 0x06001F78 RID: 8056
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetState(InternalThread thread, ThreadState set);

		// Token: 0x06001F79 RID: 8057
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClrState(InternalThread thread, ThreadState clr);

		// Token: 0x06001F7A RID: 8058
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ThreadState GetState(InternalThread thread);

		// Token: 0x06001F7B RID: 8059
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte VolatileRead(ref byte address);

		// Token: 0x06001F7C RID: 8060
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double VolatileRead(ref double address);

		// Token: 0x06001F7D RID: 8061
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short VolatileRead(ref short address);

		// Token: 0x06001F7E RID: 8062
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int VolatileRead(ref int address);

		// Token: 0x06001F7F RID: 8063
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long VolatileRead(ref long address);

		// Token: 0x06001F80 RID: 8064
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr VolatileRead(ref IntPtr address);

		// Token: 0x06001F81 RID: 8065
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object VolatileRead(ref object address);

		// Token: 0x06001F82 RID: 8066
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte VolatileRead(ref sbyte address);

		// Token: 0x06001F83 RID: 8067
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float VolatileRead(ref float address);

		// Token: 0x06001F84 RID: 8068
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ushort VolatileRead(ref ushort address);

		// Token: 0x06001F85 RID: 8069
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint VolatileRead(ref uint address);

		// Token: 0x06001F86 RID: 8070
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong VolatileRead(ref ulong address);

		// Token: 0x06001F87 RID: 8071
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern UIntPtr VolatileRead(ref UIntPtr address);

		// Token: 0x06001F88 RID: 8072
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref byte address, byte value);

		// Token: 0x06001F89 RID: 8073
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref double address, double value);

		// Token: 0x06001F8A RID: 8074
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref short address, short value);

		// Token: 0x06001F8B RID: 8075
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref int address, int value);

		// Token: 0x06001F8C RID: 8076
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref long address, long value);

		// Token: 0x06001F8D RID: 8077
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref IntPtr address, IntPtr value);

		// Token: 0x06001F8E RID: 8078
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref object address, object value);

		// Token: 0x06001F8F RID: 8079
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref sbyte address, sbyte value);

		// Token: 0x06001F90 RID: 8080
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref float address, float value);

		// Token: 0x06001F91 RID: 8081
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref ushort address, ushort value);

		// Token: 0x06001F92 RID: 8082
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref uint address, uint value);

		// Token: 0x06001F93 RID: 8083
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref ulong address, ulong value);

		// Token: 0x06001F94 RID: 8084
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref UIntPtr address, UIntPtr value);

		// Token: 0x06001F95 RID: 8085
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SystemMaxStackStize();

		// Token: 0x06001F96 RID: 8086 RVA: 0x00074960 File Offset: 0x00072B60
		private static int GetProcessDefaultStackSize(int maxStackSize)
		{
			if (maxStackSize == 0)
			{
				return 0;
			}
			if (maxStackSize < 131072)
			{
				return 131072;
			}
			int pageSize = Environment.GetPageSize();
			if (maxStackSize % pageSize != 0)
			{
				maxStackSize = maxStackSize / (pageSize - 1) * pageSize;
			}
			return Math.Min(maxStackSize, Thread.SystemMaxStackStize());
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0007499F File Offset: 0x00072B9F
		private void SetStart(MulticastDelegate start, int maxStackSize)
		{
			this.m_Delegate = start;
			this.Internal.stack_size = maxStackSize;
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x000749B4 File Offset: 0x00072BB4
		public int ManagedThreadId
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.Internal.managed_id;
			}
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x000749C1 File Offset: 0x00072BC1
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void BeginCriticalRegion()
		{
			Thread.CurrentThread.Internal.critical_region_level++;
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x000749DE File Offset: 0x00072BDE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void EndCriticalRegion()
		{
			Thread.CurrentThread.Internal.critical_region_level--;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x00004088 File Offset: 0x00002288
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void BeginThreadAffinity()
		{
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x00004088 File Offset: 0x00002288
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void EndThreadAffinity()
		{
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000747E4 File Offset: 0x000729E4
		public ApartmentState GetApartmentState()
		{
			this.ValidateThreadState();
			return (ApartmentState)this.Internal.apartment_state;
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000749FB File Offset: 0x00072BFB
		public void SetApartmentState(ApartmentState state)
		{
			if (!this.TrySetApartmentState(state))
			{
				throw new InvalidOperationException("Failed to set the specified COM apartment state.");
			}
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x00074A14 File Offset: 0x00072C14
		public bool TrySetApartmentState(ApartmentState state)
		{
			if ((this.ThreadState & ThreadState.Unstarted) == ThreadState.Running)
			{
				throw new ThreadStateException("Thread was in an invalid state for the operation being executed.");
			}
			if (this.Internal.apartment_state != 2 && (ApartmentState)this.Internal.apartment_state != state)
			{
				return false;
			}
			this.Internal.apartment_state = (byte)state;
			return true;
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x00074A62 File Offset: 0x00072C62
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.ManagedThreadId;
		}

		// Token: 0x06001FA1 RID: 8097
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetStackTraces(out Thread[] threads, out object[] stack_frames);

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00074A6C File Offset: 0x00072C6C
		internal static Dictionary<Thread, StackTrace> Mono_GetStackTraces()
		{
			Thread[] array;
			object[] array2;
			Thread.GetStackTraces(out array, out array2);
			Dictionary<Thread, StackTrace> dictionary = new Dictionary<Thread, StackTrace>();
			for (int i = 0; i < array.Length; i++)
			{
				dictionary[array[i]] = new StackTrace((StackFrame[])array2[i]);
			}
			return dictionary;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public void DisableComObjectEagerCleanup()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x00074AAD File Offset: 0x00072CAD
		private ThreadState ValidateThreadState()
		{
			ThreadState state = Thread.GetState(this.Internal);
			if ((state & ThreadState.Stopped) != ThreadState.Running)
			{
				throw new ThreadStateException("Thread is dead; state can not be accessed.");
			}
			return state;
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00074ACB File Offset: 0x00072CCB
		public static int GetCurrentProcessorId()
		{
			return RuntimeThread.GetCurrentProcessorId();
		}

		// Token: 0x040019E0 RID: 6624
		private static LocalDataStoreMgr s_LocalDataStoreMgr;

		// Token: 0x040019E1 RID: 6625
		[ThreadStatic]
		private static LocalDataStoreHolder s_LocalDataStore;

		// Token: 0x040019E2 RID: 6626
		[ThreadStatic]
		internal static CultureInfo m_CurrentCulture;

		// Token: 0x040019E3 RID: 6627
		[ThreadStatic]
		internal static CultureInfo m_CurrentUICulture;

		// Token: 0x040019E4 RID: 6628
		private static AsyncLocal<CultureInfo> s_asyncLocalCurrentCulture;

		// Token: 0x040019E5 RID: 6629
		private static AsyncLocal<CultureInfo> s_asyncLocalCurrentUICulture;

		// Token: 0x040019E6 RID: 6630
		private InternalThread internal_thread;

		// Token: 0x040019E7 RID: 6631
		private object m_ThreadStartArg;

		// Token: 0x040019E8 RID: 6632
		private object pending_exception;

		// Token: 0x040019E9 RID: 6633
		[ThreadStatic]
		private static Thread current_thread;

		// Token: 0x040019EA RID: 6634
		private MulticastDelegate m_Delegate;

		// Token: 0x040019EB RID: 6635
		private ExecutionContext m_ExecutionContext;

		// Token: 0x040019EC RID: 6636
		private bool m_ExecutionContextBelongsToOuterScope;

		// Token: 0x040019ED RID: 6637
		private IPrincipal principal;

		// Token: 0x040019EE RID: 6638
		private int principal_version;
	}
}
