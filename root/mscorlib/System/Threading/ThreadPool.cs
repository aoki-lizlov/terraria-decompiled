using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002B4 RID: 692
	public static class ThreadPool
	{
		// Token: 0x06001FE6 RID: 8166 RVA: 0x00075904 File Offset: 0x00073B04
		[SecuritySafeCritical]
		public static bool SetMaxThreads(int workerThreads, int completionPortThreads)
		{
			return ThreadPool.SetMaxThreadsNative(workerThreads, completionPortThreads);
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x0007590D File Offset: 0x00073B0D
		[SecuritySafeCritical]
		public static void GetMaxThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetMaxThreadsNative(out workerThreads, out completionPortThreads);
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x00075916 File Offset: 0x00073B16
		[SecuritySafeCritical]
		public static bool SetMinThreads(int workerThreads, int completionPortThreads)
		{
			return ThreadPool.SetMinThreadsNative(workerThreads, completionPortThreads);
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x0007591F File Offset: 0x00073B1F
		[SecuritySafeCritical]
		public static void GetMinThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetMinThreadsNative(out workerThreads, out completionPortThreads);
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x00075928 File Offset: 0x00073B28
		[SecuritySafeCritical]
		public static void GetAvailableThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetAvailableThreadsNative(out workerThreads, out completionPortThreads);
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x00075934 File Offset: 0x00073B34
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, ref stackCrawlMark, true);
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x00075954 File Offset: 0x00073B54
		[SecurityCritical]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, ref stackCrawlMark, false);
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x00075974 File Offset: 0x00073B74
		[SecurityCritical]
		private static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce, ref StackCrawlMark stackMark, bool compressStack)
		{
			if (waitObject == null)
			{
				throw new ArgumentNullException("waitObject");
			}
			if (callBack == null)
			{
				throw new ArgumentNullException("callBack");
			}
			if (millisecondsTimeOutInterval != 4294967295U && millisecondsTimeOutInterval > 2147483647U)
			{
				throw new NotSupportedException("Timeout is too big. Maximum is Int32.MaxValue");
			}
			RegisteredWaitHandle registeredWaitHandle = new RegisteredWaitHandle(waitObject, callBack, state, new TimeSpan(0, 0, 0, 0, (int)millisecondsTimeOutInterval), executeOnlyOnce);
			if (compressStack)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(registeredWaitHandle.Wait), null);
			}
			else
			{
				ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(registeredWaitHandle.Wait), null);
			}
			return registeredWaitHandle;
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x000759F8 File Offset: 0x00073BF8
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)((millisecondsTimeOutInterval == -1) ? (-1) : millisecondsTimeOutInterval), executeOnlyOnce, ref stackCrawlMark, true);
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x00075A38 File Offset: 0x00073C38
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)((millisecondsTimeOutInterval == -1) ? (-1) : millisecondsTimeOutInterval), executeOnlyOnce, ref stackCrawlMark, false);
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x00075A78 File Offset: 0x00073C78
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1L)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (millisecondsTimeOutInterval == -1L) ? uint.MaxValue : ((uint)millisecondsTimeOutInterval), executeOnlyOnce, ref stackCrawlMark, true);
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x00075AB8 File Offset: 0x00073CB8
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1L)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (millisecondsTimeOutInterval == -1L) ? uint.MaxValue : ((uint)millisecondsTimeOutInterval), executeOnlyOnce, ref stackCrawlMark, false);
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x00075AF8 File Offset: 0x00073CF8
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Argument must be less than or equal to 2^31 - 1 milliseconds."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)num, executeOnlyOnce, ref stackCrawlMark, true);
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x00075B58 File Offset: 0x00073D58
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Argument must be less than or equal to 2^31 - 1 milliseconds."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)num, executeOnlyOnce, ref stackCrawlMark, false);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x00075BB8 File Offset: 0x00073DB8
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool QueueUserWorkItem(WaitCallback callBack, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(callBack, state, ref stackCrawlMark, true, true);
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x00075BD4 File Offset: 0x00073DD4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool QueueUserWorkItem(WaitCallback callBack)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(callBack, null, ref stackCrawlMark, true, true);
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x00075BF0 File Offset: 0x00073DF0
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool UnsafeQueueUserWorkItem(WaitCallback callBack, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(callBack, state, ref stackCrawlMark, false, true);
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x00075C0C File Offset: 0x00073E0C
		public static bool QueueUserWorkItem<TState>(Action<TState> callBack, TState state, bool preferLocal)
		{
			if (callBack == null)
			{
				throw new ArgumentNullException("callBack");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(delegate(object x)
			{
				callBack((TState)((object)x));
			}, state, ref stackCrawlMark, true, !preferLocal);
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x00075C58 File Offset: 0x00073E58
		public static bool UnsafeQueueUserWorkItem<TState>(Action<TState> callBack, TState state, bool preferLocal)
		{
			if (callBack == null)
			{
				throw new ArgumentNullException("callBack");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(delegate(object x)
			{
				callBack((TState)((object)x));
			}, state, ref stackCrawlMark, false, !preferLocal);
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00075CA4 File Offset: 0x00073EA4
		[SecurityCritical]
		private static bool QueueUserWorkItemHelper(WaitCallback callBack, object state, ref StackCrawlMark stackMark, bool compressStack, bool forceGlobal = true)
		{
			bool flag = true;
			if (callBack != null)
			{
				ThreadPool.EnsureVMInitialized();
				try
				{
					return flag;
				}
				finally
				{
					QueueUserWorkItemCallback queueUserWorkItemCallback = new QueueUserWorkItemCallback(callBack, state, compressStack, ref stackMark);
					ThreadPoolGlobals.workQueue.Enqueue(queueUserWorkItemCallback, forceGlobal);
					flag = true;
				}
			}
			throw new ArgumentNullException("WaitCallback");
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00075CF4 File Offset: 0x00073EF4
		[SecurityCritical]
		internal static void UnsafeQueueCustomWorkItem(IThreadPoolWorkItem workItem, bool forceGlobal)
		{
			ThreadPool.EnsureVMInitialized();
			try
			{
			}
			finally
			{
				ThreadPoolGlobals.workQueue.Enqueue(workItem, forceGlobal);
			}
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x00075D28 File Offset: 0x00073F28
		[SecurityCritical]
		internal static bool TryPopCustomWorkItem(IThreadPoolWorkItem workItem)
		{
			return ThreadPoolGlobals.vmTpInitialized && ThreadPoolGlobals.workQueue.LocalFindAndPop(workItem);
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x00075D40 File Offset: 0x00073F40
		[SecurityCritical]
		internal static IEnumerable<IThreadPoolWorkItem> GetQueuedWorkItems()
		{
			return ThreadPool.EnumerateQueuedWorkItems(ThreadPoolWorkQueue.allThreadQueues.Current, ThreadPoolGlobals.workQueue.queueTail);
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x00075D5D File Offset: 0x00073F5D
		internal static IEnumerable<IThreadPoolWorkItem> EnumerateQueuedWorkItems(ThreadPoolWorkQueue.WorkStealingQueue[] wsQueues, ThreadPoolWorkQueue.QueueSegment globalQueueTail)
		{
			if (wsQueues != null)
			{
				foreach (ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue in wsQueues)
				{
					if (workStealingQueue != null && workStealingQueue.m_array != null)
					{
						IThreadPoolWorkItem[] items = workStealingQueue.m_array;
						int num;
						for (int i = 0; i < items.Length; i = num + 1)
						{
							IThreadPoolWorkItem threadPoolWorkItem = items[i];
							if (threadPoolWorkItem != null)
							{
								yield return threadPoolWorkItem;
							}
							num = i;
						}
						items = null;
					}
				}
				ThreadPoolWorkQueue.WorkStealingQueue[] array = null;
			}
			if (globalQueueTail != null)
			{
				ThreadPoolWorkQueue.QueueSegment segment;
				for (segment = globalQueueTail; segment != null; segment = segment.Next)
				{
					IThreadPoolWorkItem[] items = segment.nodes;
					int num;
					for (int j = 0; j < items.Length; j = num + 1)
					{
						IThreadPoolWorkItem threadPoolWorkItem2 = items[j];
						if (threadPoolWorkItem2 != null)
						{
							yield return threadPoolWorkItem2;
						}
						num = j;
					}
					items = null;
				}
				segment = null;
			}
			yield break;
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x00075D74 File Offset: 0x00073F74
		[SecurityCritical]
		internal static IEnumerable<IThreadPoolWorkItem> GetLocallyQueuedWorkItems()
		{
			return ThreadPool.EnumerateQueuedWorkItems(new ThreadPoolWorkQueue.WorkStealingQueue[] { ThreadPoolWorkQueueThreadLocals.threadLocals.workStealingQueue }, null);
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x00075D8F File Offset: 0x00073F8F
		[SecurityCritical]
		internal static IEnumerable<IThreadPoolWorkItem> GetGloballyQueuedWorkItems()
		{
			return ThreadPool.EnumerateQueuedWorkItems(null, ThreadPoolGlobals.workQueue.queueTail);
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x00075DA4 File Offset: 0x00073FA4
		private static object[] ToObjectArray(IEnumerable<IThreadPoolWorkItem> workitems)
		{
			int num = 0;
			foreach (IThreadPoolWorkItem threadPoolWorkItem in workitems)
			{
				num++;
			}
			object[] array = new object[num];
			num = 0;
			foreach (IThreadPoolWorkItem threadPoolWorkItem2 in workitems)
			{
				if (num < array.Length)
				{
					array[num] = threadPoolWorkItem2;
				}
				num++;
			}
			return array;
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x00075E34 File Offset: 0x00074034
		[SecurityCritical]
		internal static object[] GetQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetQueuedWorkItems());
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x00075E40 File Offset: 0x00074040
		[SecurityCritical]
		internal static object[] GetGloballyQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetGloballyQueuedWorkItems());
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x00075E4C File Offset: 0x0007404C
		[SecurityCritical]
		internal static object[] GetLocallyQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetLocallyQueuedWorkItems());
		}

		// Token: 0x06002004 RID: 8196
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool RequestWorkerThread();

		// Token: 0x06002005 RID: 8197
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool PostQueuedCompletionStatus(NativeOverlapped* overlapped);

		// Token: 0x06002006 RID: 8198 RVA: 0x00075E58 File Offset: 0x00074058
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe static bool UnsafeQueueNativeOverlapped(NativeOverlapped* overlapped)
		{
			throw new NotImplementedException("");
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00075E64 File Offset: 0x00074064
		[SecurityCritical]
		private static void EnsureVMInitialized()
		{
			if (!ThreadPoolGlobals.vmTpInitialized)
			{
				ThreadPool.InitializeVMTp(ref ThreadPoolGlobals.enableWorkerTracking);
				ThreadPoolGlobals.vmTpInitialized = true;
			}
		}

		// Token: 0x06002008 RID: 8200
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetMinThreadsNative(int workerThreads, int completionPortThreads);

		// Token: 0x06002009 RID: 8201
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetMaxThreadsNative(int workerThreads, int completionPortThreads);

		// Token: 0x0600200A RID: 8202
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMinThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x0600200B RID: 8203
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMaxThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x0600200C RID: 8204
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAvailableThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x0600200D RID: 8205
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool NotifyWorkItemComplete();

		// Token: 0x0600200E RID: 8206
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportThreadStatus(bool isWorking);

		// Token: 0x0600200F RID: 8207 RVA: 0x00075E81 File Offset: 0x00074081
		[SecuritySafeCritical]
		internal static void NotifyWorkItemProgress()
		{
			ThreadPool.EnsureVMInitialized();
			ThreadPool.NotifyWorkItemProgressNative();
		}

		// Token: 0x06002010 RID: 8208
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NotifyWorkItemProgressNative();

		// Token: 0x06002011 RID: 8209
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NotifyWorkItemQueued();

		// Token: 0x06002012 RID: 8210 RVA: 0x0000408A File Offset: 0x0000228A
		[SecurityCritical]
		internal static bool IsThreadPoolHosted()
		{
			return false;
		}

		// Token: 0x06002013 RID: 8211
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeVMTp(ref bool enableWorkerTracking);

		// Token: 0x06002014 RID: 8212 RVA: 0x00075E8D File Offset: 0x0007408D
		[SecuritySafeCritical]
		[Obsolete("ThreadPool.BindHandle(IntPtr) has been deprecated.  Please use ThreadPool.BindHandle(SafeHandle) instead.", false)]
		public static bool BindHandle(IntPtr osHandle)
		{
			return ThreadPool.BindIOCompletionCallbackNative(osHandle);
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x00075E98 File Offset: 0x00074098
		[SecuritySafeCritical]
		public static bool BindHandle(SafeHandle osHandle)
		{
			if (osHandle == null)
			{
				throw new ArgumentNullException("osHandle");
			}
			bool flag = false;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				osHandle.DangerousAddRef(ref flag2);
				flag = ThreadPool.BindIOCompletionCallbackNative(osHandle.DangerousGetHandle());
			}
			finally
			{
				if (flag2)
				{
					osHandle.DangerousRelease();
				}
			}
			return flag;
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x00003FB7 File Offset: 0x000021B7
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private static bool BindIOCompletionCallbackNative(IntPtr fileHandle)
		{
			return true;
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x00075EF0 File Offset: 0x000740F0
		internal static bool IsThreadPoolThread
		{
			get
			{
				return Thread.CurrentThread.IsThreadPoolThread;
			}
		}

		// Token: 0x020002B5 RID: 693
		[CompilerGenerated]
		private sealed class <>c__DisplayClass17_0<TState>
		{
			// Token: 0x06002018 RID: 8216 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass17_0()
			{
			}

			// Token: 0x06002019 RID: 8217 RVA: 0x00075EFC File Offset: 0x000740FC
			internal void <QueueUserWorkItem>b__0(object x)
			{
				this.callBack((TState)((object)x));
			}

			// Token: 0x04001A17 RID: 6679
			public Action<TState> callBack;
		}

		// Token: 0x020002B6 RID: 694
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0<TState>
		{
			// Token: 0x0600201A RID: 8218 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x0600201B RID: 8219 RVA: 0x00075F0F File Offset: 0x0007410F
			internal void <UnsafeQueueUserWorkItem>b__0(object x)
			{
				this.callBack((TState)((object)x));
			}

			// Token: 0x04001A18 RID: 6680
			public Action<TState> callBack;
		}

		// Token: 0x020002B7 RID: 695
		[CompilerGenerated]
		private sealed class <EnumerateQueuedWorkItems>d__23 : IEnumerable<IThreadPoolWorkItem>, IEnumerable, IEnumerator<IThreadPoolWorkItem>, IDisposable, IEnumerator
		{
			// Token: 0x0600201C RID: 8220 RVA: 0x00075F22 File Offset: 0x00074122
			[DebuggerHidden]
			public <EnumerateQueuedWorkItems>d__23(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600201D RID: 8221 RVA: 0x00004088 File Offset: 0x00002288
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600201E RID: 8222 RVA: 0x00075F3C File Offset: 0x0007413C
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					if (wsQueues != null)
					{
						array = wsQueues;
						j = 0;
						goto IL_00D4;
					}
					goto IL_00EE;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_014C;
				default:
					return false;
				}
				IL_009F:
				int num = i;
				i = num + 1;
				IL_00AF:
				if (i >= items.Length)
				{
					items = null;
				}
				else
				{
					IThreadPoolWorkItem threadPoolWorkItem = items[i];
					if (threadPoolWorkItem != null)
					{
						this.<>2__current = threadPoolWorkItem;
						this.<>1__state = 1;
						return true;
					}
					goto IL_009F;
				}
				IL_00C6:
				j++;
				IL_00D4:
				if (j >= array.Length)
				{
					array = null;
				}
				else
				{
					ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue = array[j];
					if (workStealingQueue != null && workStealingQueue.m_array != null)
					{
						items = workStealingQueue.m_array;
						i = 0;
						goto IL_00AF;
					}
					goto IL_00C6;
				}
				IL_00EE:
				if (globalQueueTail != null)
				{
					segment = globalQueueTail;
					goto IL_0186;
				}
				return false;
				IL_014C:
				num = j;
				j = num + 1;
				IL_015C:
				if (j >= items.Length)
				{
					items = null;
					segment = segment.Next;
				}
				else
				{
					IThreadPoolWorkItem threadPoolWorkItem2 = items[j];
					if (threadPoolWorkItem2 != null)
					{
						this.<>2__current = threadPoolWorkItem2;
						this.<>1__state = 2;
						return true;
					}
					goto IL_014C;
				}
				IL_0186:
				if (segment != null)
				{
					items = segment.nodes;
					j = 0;
					goto IL_015C;
				}
				segment = null;
				return false;
			}

			// Token: 0x170003CE RID: 974
			// (get) Token: 0x0600201F RID: 8223 RVA: 0x000760E2 File Offset: 0x000742E2
			IThreadPoolWorkItem IEnumerator<IThreadPoolWorkItem>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002020 RID: 8224 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003CF RID: 975
			// (get) Token: 0x06002021 RID: 8225 RVA: 0x000760E2 File Offset: 0x000742E2
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002022 RID: 8226 RVA: 0x000760EC File Offset: 0x000742EC
			[DebuggerHidden]
			IEnumerator<IThreadPoolWorkItem> IEnumerable<IThreadPoolWorkItem>.GetEnumerator()
			{
				ThreadPool.<EnumerateQueuedWorkItems>d__23 <EnumerateQueuedWorkItems>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<EnumerateQueuedWorkItems>d__ = this;
				}
				else
				{
					<EnumerateQueuedWorkItems>d__ = new ThreadPool.<EnumerateQueuedWorkItems>d__23(0);
				}
				<EnumerateQueuedWorkItems>d__.wsQueues = wsQueues;
				<EnumerateQueuedWorkItems>d__.globalQueueTail = globalQueueTail;
				return <EnumerateQueuedWorkItems>d__;
			}

			// Token: 0x06002023 RID: 8227 RVA: 0x0007613B File Offset: 0x0007433B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Threading.IThreadPoolWorkItem>.GetEnumerator();
			}

			// Token: 0x04001A19 RID: 6681
			private int <>1__state;

			// Token: 0x04001A1A RID: 6682
			private IThreadPoolWorkItem <>2__current;

			// Token: 0x04001A1B RID: 6683
			private int <>l__initialThreadId;

			// Token: 0x04001A1C RID: 6684
			private ThreadPoolWorkQueue.WorkStealingQueue[] wsQueues;

			// Token: 0x04001A1D RID: 6685
			public ThreadPoolWorkQueue.WorkStealingQueue[] <>3__wsQueues;

			// Token: 0x04001A1E RID: 6686
			private ThreadPoolWorkQueue.QueueSegment globalQueueTail;

			// Token: 0x04001A1F RID: 6687
			public ThreadPoolWorkQueue.QueueSegment <>3__globalQueueTail;

			// Token: 0x04001A20 RID: 6688
			private ThreadPoolWorkQueue.WorkStealingQueue[] <>7__wrap1;

			// Token: 0x04001A21 RID: 6689
			private int <>7__wrap2;

			// Token: 0x04001A22 RID: 6690
			private IThreadPoolWorkItem[] <items>5__4;

			// Token: 0x04001A23 RID: 6691
			private int <i>5__5;

			// Token: 0x04001A24 RID: 6692
			private ThreadPoolWorkQueue.QueueSegment <segment>5__6;
		}
	}
}
